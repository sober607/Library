using Library.DAL.Entities;
using Library.DAL.Repositories.Interfaces;
using Library.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.DAL.Repositories
{
    public class CountryRepository : EfCoreRepository<Country, ApplicationContext>, ICountryRepository
    {
        public CountryRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Country>> GetCountriesByNameFragement(string countryNameFragment)
        {
             return await _context.Countries.Where(x => x.Name.Contains(countryNameFragment)).ToListAsync();
        }

        public async Task<Country> GetCountryByNameAsync(string countryName)
        {
            return await _context.Countries.FirstOrDefaultAsync(x => x.Name.Contains(countryName));
        }
    }
}
