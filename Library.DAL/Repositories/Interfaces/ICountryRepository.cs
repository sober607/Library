using Library.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.Repositories.Interfaces
{
    public interface ICountryRepository : IRepository<Country>
    {
        IQueryable<Country> GetCountriesByNameFragement(string countryNameFragment);
    }
}
