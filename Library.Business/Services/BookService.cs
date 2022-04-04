using Library.Business.DTO.Book;
using Library.Business.Model.ResultModel;
using Library.Business.Services.Interfaces;
using Library.DAL.UnitOfWork;
using Library.DAL.Entities;
using AutoMapper;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace Library.Business.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPublishingHouseService _publishingHouseService;
        private const string WRONG_BOOK_ID = "Wrong book ID";
        private const string BOOK_NOT_EXIST = "Book with given ID do not exist";
        private const string WRONG_BOOK_MODEL = "Wrong book model";
        private const string EMPTY_BOOK_MODEL = "Empty book model";

        public BookService(IUnitOfWork unitOfWork, IMapper mapper, IPublishingHouseService publishingHouseService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _publishingHouseService = publishingHouseService;
        }

        public async Task<ResultModel<BookDto>> CreateBookAsync(CreateBookDto createBookDto)
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

                await CreateBookAuthors(createBookDto, mappedToBookEntity);

                await _unitOfWork.SaveAsync();

                var mappedFromCreateBookDtoToBookDto = _mapper.Map<CreateBookDto, BookDto>(createBookDto);

                var bookId = mappedToBookEntity.Id;
                mappedFromCreateBookDtoToBookDto.Id = bookId;
                result = ResultModel<BookDto>.GetSuccess(mappedFromCreateBookDtoToBookDto);
            }
            catch(Exception ex)
            {
                result = ResultModel<BookDto>.GetError(ErrorCode.InternalServerError, $"Creation of book is failed: {ex.Message}");
            }

            return result;
        }

        public async Task<ResultModel<bool>> DeleteBookByIdAsync(long bookId)
        {
            ResultModel<bool> result;

            if (bookId == default)
            {
                return ResultModel<bool>.GetError(ErrorCode.ValidationError, WRONG_BOOK_ID);
            }

            try
            {
                var doesBookExist = await _unitOfWork.Books.DoesExistByIdAsync(bookId);

                if (!doesBookExist)
                {
                    return ResultModel<bool>.GetError(ErrorCode.NotFound, BOOK_NOT_EXIST);
                }

                await _unitOfWork.Books.DeleteById(bookId);
                await _unitOfWork.SaveAsync();

                result = ResultModel<bool>.GetSuccess(true);
            }
            catch (Exception ex)
            {
                result = ResultModel<bool>.GetError(ErrorCode.InternalServerError, $"Deletion of book has failed: {ex.Message}");
            }

            return result;
        }

        public async Task<ResultModel<BookDto>> GetBookByIdAsync(long bookId)
        {
            ResultModel<BookDto> result;

            if (bookId == default)
            {
                return ResultModel<BookDto>.GetError(ErrorCode.ValidationError, WRONG_BOOK_ID);
            }

            var book = await _unitOfWork.Books.GetByIdAsync(bookId);

            if (book == default)
            {
                return ResultModel<BookDto>.GetError(ErrorCode.NotFound, $"{BOOK_NOT_EXIST}. ID: {bookId}");
            }

            var bookAuthors = await _unitOfWork.BookAuthors.GetBookAuthorIdsByBookIdAsync(bookId);

            book.BookAuthors = bookAuthors.ToList();

            var mappedEntity = _mapper.Map<Book, BookDto>(book);
            result = ResultModel<BookDto>.GetSuccess(mappedEntity);

            return result;
        }

        public async Task<ResultModel<BookDto>> UpdateBook(BookDto bookDto)
        {
            ResultModel<BookDto> result;

            if (bookDto == default && bookDto.Id == default)
            {
                return ResultModel<BookDto>.GetError(ErrorCode.ValidationError, WRONG_BOOK_MODEL);
            }

            var existingBook = await _unitOfWork.Books.GetByIdAsync(bookDto.Id);

            if (existingBook == default)
            {
                return ResultModel<BookDto>.GetError(ErrorCode.NotFound, $"{BOOK_NOT_EXIST}. ID: {bookDto.Id}");
            }

            var existingBookAuthors = await _unitOfWork.BookAuthors.GetBookAuthorIdsByBookIdAsync(existingBook.Id);

            var newBookAuthors = bookDto.AuthorIds.Select(personId => new BookAuthor
            {
                BookId = existingBook.Id,
                Book = existingBook,
                AuthorId = personId
            }).ToList();

            existingBook.Circulations = bookDto.Circulations ?? existingBook.Circulations;
            existingBook.PublishingDate = bookDto.PublishingDate ?? existingBook.PublishingDate;
            existingBook.PublishingHouseId = bookDto.PublishingHouseId ?? existingBook.PublishingHouseId;
            existingBook.Title = bookDto.Title ?? existingBook.Title;

            _unitOfWork.Books.Update(existingBook);

            _unitOfWork.BookAuthors.UpdateManyToMany(existingBookAuthors, newBookAuthors);

            await _unitOfWork.SaveAsync();

            result = ResultModel<BookDto>.GetSuccess(bookDto);

            return result;
        }

        private async Task<ResultModel<BookDto>> ValidateNewBookDto(CreateBookDto createBookDto) // Need to use Yield return to collect all errors in next edition
        {
            if (createBookDto == default)
            {
                return ResultModel<BookDto>.GetError(ErrorCode.ValidationError, EMPTY_BOOK_MODEL);
            }

            var doesPublishingHouseExist = await _publishingHouseService.DoesPublishingHouseExistAsync(createBookDto.PublishingHouseId);

            if (doesPublishingHouseExist.Error != default)
            {
                return ResultModel<BookDto>.GetError(doesPublishingHouseExist.Error.Code, doesPublishingHouseExist.Error.Message);
            }

            if (createBookDto.PublishingHouseId != default && !doesPublishingHouseExist.Data)
            {
                return ResultModel<BookDto>.GetError(ErrorCode.NotFound, "Given publishing house does not exist");
            }

            if (createBookDto.PersonsIdsToBeAuthors != default && createBookDto.PersonsIdsToBeAuthors.Any())
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

        private async Task CreateBookAuthors(CreateBookDto createBookDto, Book book)
        {
            if (createBookDto.PersonsIdsToBeAuthors != default && createBookDto.PersonsIdsToBeAuthors.Any())
            {
                foreach (var personId in createBookDto.PersonsIdsToBeAuthors)
                {
                    var person = await _unitOfWork.Persons.GetByIdAsync(personId);

                    var bookAuthor = new BookAuthor()
                    {
                        Book = book,
                        AuthorId = personId,
                        Author = person
                    };

                    await _unitOfWork.BookAuthors.CreateAsync(bookAuthor);
                }
            }
        }

        private void AttachBookAuthorsToBook(Book book, IEnumerable<long> authorsList)
        {
            foreach(var authorId in authorsList)
            {
                var bookAuthor = new BookAuthor();
                bookAuthor.AuthorId = authorId;
            }
        }
    }
}
