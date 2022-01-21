using Library.Business.DTO.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Business.Services.Interfaces
{
    public interface ICountryService
    {
        Task<Result<IList<CountryDto>>> CheckSimilarCountriesByNameAsync(string countryName);

        Task<Result<CountryDto>> GetCountryByNameAsync(string countryName);

        Task<Result<CountryDto>> GetCountryByIdAsync(long countryId);

        Task<Result<CountryDto>> CreateCountryAsync(CreateCountryDto countryDto);

        Task<Result<bool>> DeleteCountryByIdAsync(long countryId);

        Task<Result<CountryDto>> UpdateCountryAsync(long countryId, UpdateCountryDto countryDto);
    }
}
