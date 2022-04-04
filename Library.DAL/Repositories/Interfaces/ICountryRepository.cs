using Library.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.DAL.Repositories.Interfaces
{
    public interface ICountryRepository : IRepository<Country>
    {
        Task<IEnumerable<Country>> GetCountriesByNameFragement(string countryNameFragment);

        Task<Country> GetCountryByNameAsync(string countryName);
    }
}
