using Library.Business.DTO.Book;
using Library.Business.Model.ResultModel;
using Library.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookDto createBookDto)
        {
            var createResult = await _bookService.CreateBookAsync(createBookDto);

            return ResultModel<BookDto>.ToActionResult(createResult);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long bookId)
        {
            var deleteResult = await _bookService.DeleteBookByIdAsync(bookId);

            return ResultModel<bool>.ToActionResult(deleteResult);
        }

        [HttpGet]
        public async Task<IActionResult> Get(long bookId)
        {
            var book = await _bookService.GetBookByIdAsync(bookId);

            return ResultModel<BookDto>.ToActionResult(book);
        }

        [HttpPut]
        public async Task<IActionResult> Update(BookDto bookDto)
        {
            var bookUpdateResult = await _bookService.UpdateBook(bookDto);

            return ResultModel<BookDto>.ToActionResult(bookUpdateResult);
        }
    }
}
