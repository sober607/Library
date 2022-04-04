using Library.DAL.Entities;
using Library.DAL.Repositories.Interfaces;
using Library.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.DAL.Repositories
{
    public class BookAuthorRepository : EfCoreRepository<BookAuthor, ApplicationContext>, IBookAuthorRepository
    {
        public BookAuthorRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<IEnumerable<BookAuthor>> GetBookAuthorIdsByBookIdAsync(long bookId)
        {
            return await _context.BookAuthor.Where(author => author.BookId == bookId).ToListAsync();
        }
    }
}
