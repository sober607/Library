using Library.DAL.Entities;
using Library.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.DAL.Repositories
{
    public class PersonRepository : EfCoreRepository<Person, ApplicationContext>
    {
        public PersonRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
