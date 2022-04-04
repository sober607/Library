using Library.DAL.Entities;
using Library.DAL.UnitOfWork;

namespace Library.DAL.Repositories
{
    public class BookRepository : EfCoreRepository<Book, ApplicationContext>
    {
        public BookRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
