using Library.DAL.Entities;
using Library.DAL.UnitOfWork;

namespace Library.DAL.Repositories
{
    public class PersonRepository : EfCoreRepository<Person, ApplicationContext>
    {
        public PersonRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
