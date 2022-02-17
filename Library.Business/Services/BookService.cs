using Library.Business.DTO.Book;
using Library.Business.Model.ResultModel;
using Library.Business.Services.Interfaces;
using Library.DAL.UnitOfWork;
using Library.DAL.Entities;
using AutoMapper;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace Library.Business.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResultModel<BookDto>> CreateBookAsync(CreateBookDto createBookDto) // TODO: To separate to small private methods /REFACTOR
        {
            ResultModel<BookDto> result;

            var createBookDtoValidationResult = await ValidateNewBookDto(createBookDto);

            if (createBookDtoValidationResult.Error != default)
            {
                return createBookDtoValidationResult;
            }

            try
            {
                var mappedToBookEntity = _mapper.Map<CreateBookDto, Book>(createBookDto);

                await _unitOfWork.Books.CreateAsync(mappedToBookEntity);

                var bookId = mappedToBookEntity.Id;

                if (createBookDto.PersonsIdsToBeAuthors != default && createBookDto.PersonsIdsToBeAuthors.Count > 0)
                {
                    foreach (var personId in createBookDto.PersonsIdsToBeAuthors)
                    {
                        var person = await _unitOfWork.Persons.GetByIdAsync(personId);

                        var bookAuthor = new BookAuthor()
                        {
                            BookId = bookId,
                            Book = mappedToBookEntity,
                            AuthorId = personId,
                            Author = person
                        };

                        await _unitOfWork.BookAuthors.CreateAsync(bookAuthor);
                    }
                }

                await _unitOfWork.SaveAsync();

                var mappedFromCreateBookDtoToBookDto = _mapper.Map<CreateBookDto, BookDto>(createBookDto);
                result = ResultModel<BookDto>.GetSuccess(mappedFromCreateBookDtoToBookDto);
            }
            catch(Exception ex)
            {
                result = ResultModel<BookDto>.GetError(ErrorCode.InternalServerError, $"Database error: {ex.Message}");
            }

            return result;
        }

        public async Task<ResultModel<bool>> DeleteBookByIdAsync(long bookId)
        {
            ResultModel<bool> result;

            if (bookId == default)
            {
                result = ResultModel<bool>.GetError(ErrorCode.ValidationError, "Wrong book ID");
            }
            else
            {
                try
                {
                    var doesBookExist = await _unitOfWork.Books.DoesExistByIdAsync(bookId);

                    if (doesBookExist)
                    {
                        await _unitOfWork.Books.DeleteById(bookId);
                        result = ResultModel<bool>.GetSuccess(true);
                    }
                    else
                    {
                        result = ResultModel<bool>.GetError(ErrorCode.NotFound, "Book with given ID do not exist");
                    }
                }
                catch (Exception ex)
                {
                    result = ResultModel<bool>.GetError(ErrorCode.InternalServerError, $"Database error: {ex.Message}");
                }
            }

            return result;
        }

        public async Task<ResultModel<BookDto>> GetBookByIdAsync(long bookId)
        {
            ResultModel<BookDto> result;

            if (bookId == default)
            {
                result = ResultModel<BookDto>.GetError(ErrorCode.ValidationError, "Wrong book ID");
            }
            else
            {
                var book = await _unitOfWork.Books.GetByIdAsync(bookId);

                if (book != default)
                {
                    var mappedEntity = _mapper.Map<Book, BookDto>(book);
                    result = ResultModel<BookDto>.GetSuccess(mappedEntity);
                }
                else
                {
                    result = ResultModel<BookDto>.GetError(ErrorCode.NotFound, $"Book with ID: {bookId} not found");
                }
            }

            return result;
        }

        public async Task<ResultModel<BookDto>> UpdateBook(BookDto bookDto)
        {
            ResultModel<BookDto> result;

            if (bookDto == default && bookDto.Id == default)
            {
                result = ResultModel<BookDto>.GetError(ErrorCode.ValidationError, "Book model is wrong");
            }
            else
            {
                var existingBook = await _unitOfWork.Books.GetByIdAsync(bookDto.Id);

                if (existingBook != default)
                {
                    var newBookAuthors = bookDto.AuthorIds.Select(personId => new BookAuthor
                    {
                        BookId = existingBook.Id,
                        Book = existingBook,
                        AuthorId = personId
                    }).ToList();

                    var oldAuthors = existingBook.BookAuthors.Where(x => x.BookId == existingBook.Id);

                    _unitOfWork.BookAuthors.UpdateManyToMany(oldAuthors, newBookAuthors);

                    var newMappedBook = _mapper.Map<BookDto, Book>(bookDto);

                    newMappedBook.BookAuthors = newBookAuthors;

                    _unitOfWork.Books.Update(newMappedBook);

                    await _unitOfWork.SaveAsync();

                    result = ResultModel<BookDto>.GetSuccess(bookDto);
                }
                else
                {
                    result = ResultModel<BookDto>.GetError(ErrorCode.NotFound, $"Book with ID: {bookDto.Id} not found");
                } 
            }

            return result;
        }

        private async Task<bool> DoesPublishingHouseExist(long? publishingHouseId)
        {
            var result = false;

            if (publishingHouseId != default)
            {
                var doesNewPublishingHouseExist = await _unitOfWork.PublishingHouses.DoesExistByIdAsync((long)publishingHouseId);

                result = doesNewPublishingHouseExist;
            }

            return result;
        }

        private async Task<ResultModel<BookDto>> ValidateNewBookDto(CreateBookDto createBookDto) // Need to use Yield return to collect all errors in next edition
        {
            if (createBookDto == default)
            {
                return ResultModel<BookDto>.GetError(ErrorCode.ValidationError, "Empty book model");
            }

            if (createBookDto.PublishingHouseId != default && !await DoesPublishingHouseExist(createBookDto.PublishingHouseId))
            {
                return ResultModel<BookDto>.GetError(ErrorCode.NotFound, "Given publishing house does not exist");
            }

            if (createBookDto.PersonsIdsToBeAuthors != default && createBookDto.PersonsIdsToBeAuthors.Count != 0)
            {
                foreach (var personId in createBookDto.PersonsIdsToBeAuthors) // Horrible check. Have to find way to send IEnumerable to EF and to receive List of non-existing persons
                {
                    if (personId == default || !await _unitOfWork.Persons.DoesExistByIdAsync(personId))
                    {
                        return ResultModel<BookDto>.GetError(ErrorCode.NotFound, $"Person with ID {personId} does not exist");
                    }
                }
            }

            return ResultModel<BookDto>.GetSuccess(_mapper.Map<CreateBookDto, BookDto>(createBookDto));
        }
    }
}
