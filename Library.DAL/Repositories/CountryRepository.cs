using Library.DAL.Entities;
using Library.DAL.Repositories.Interfaces;
using Library.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Library.DAL.Repositories
{
    public class CountryRepository : EfCoreRepository<Country, ApplicationContext>, ICountryRepository
    {
        public CountryRepository(ApplicationContext context) : base(context)
        {
        }

        public IQueryable<Country> GetCountriesByNameFragement(string countryNameFragment)
        {
             return _context.Countries.Where(x => x.Name.Contains(countryNameFragment)).AsQueryable();
        }
    }
}
