using Library.Business.DTO.Country;
using Library.Business.Model.ResultModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Business.Services.Interfaces
{
    public interface ICountryService
    {
        Task<ResultModel<IList<CountryDto>>> CheckSimilarCountriesByNameAsync(string countryName);

        Task<ResultModel<CountryDto>> GetCountryByNameAsync(string countryName);

        Task<ResultModel<CountryDto>> GetCountryByIdAsync(long countryId);

        Task<ResultModel<CountryDto>> CreateCountryAsync(CountryDto countryDto);

        ResultModel<bool> DeleteCountryByIdAsync(long countryId);

        Task<ResultModel<CountryDto>> UpdateCountry(CountryDto countryDto);
    }
}
