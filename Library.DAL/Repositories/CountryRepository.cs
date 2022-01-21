using Library.DAL.Entities;
using Library.DAL.UnitOfWork;

namespace Library.DAL.Repositories
{
    public class CountryRepository : EfCoreRepository<Country, ApplicationContext>
    {
        public CountryRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
