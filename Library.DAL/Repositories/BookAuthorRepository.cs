using Library.DAL.Entities;
using Library.DAL.UnitOfWork;

namespace Library.DAL.Repositories
{
    public class BookAuthorRepository : EfCoreRepository<BookAuthor, ApplicationContext>
    {
        public BookAuthorRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
