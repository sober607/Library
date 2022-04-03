using Library.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.DAL.Repositories.Interfaces
{
    public interface IBookAuthorRepository : IRepository<BookAuthor>
    {
        Task<IEnumerable<BookAuthor>> GetBookAuthorIdsByBookIdAsync(long bookId);
    }
}
