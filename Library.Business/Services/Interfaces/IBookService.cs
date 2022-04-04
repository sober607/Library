using Library.Business.DTO.Book;
using Library.Business.Model.ResultModel;
using System.Threading.Tasks;

namespace Library.Business.Services.Interfaces
{
    public interface IBookService
    {
        Task<ResultModel<BookDto>> GetBookByIdAsync(long bookId);

        Task<ResultModel<BookDto>> CreateBookAsync(CreateBookDto bookDto);

        Task<ResultModel<bool>> DeleteBookByIdAsync(long bookId);

        Task<ResultModel<BookDto>> UpdateBook(BookDto bookDto);
    }
}
