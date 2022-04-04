using Library.DAL.Entities;
using Library.DAL.UnitOfWork;

namespace Library.DAL.Repositories
{
    public class PublishingHouseRepository : EfCoreRepository<PublishingHouse, ApplicationContext>
    {
        public PublishingHouseRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
