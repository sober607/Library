using Library.Business.DTO.Country;
using Library.Business.Model.ResultModel;
using Library.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : Controller
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [Route("Find")]
        [HttpGet]
        public async Task<IActionResult> Find(string countryName)
        {
            var countriesWithGivenName = await _countryService.CheckSimilarCountriesByNameAsync(countryName);

            return ResultModel<IList<CountryDto>>.ToActionResult(countriesWithGivenName);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long countryId)
        {
            var deletionResult = await _countryService.DeleteCountryById(countryId);

            return ResultModel<bool>.ToActionResult(deletionResult);
        }

        [HttpGet("{countryId}")]
        public async Task<IActionResult> Get(long countryId)
        {
            var countrySearchResult = await _countryService.GetCountryByIdAsync(countryId);

            return ResultModel<CountryDto>.ToActionResult(countrySearchResult);
        }

        [HttpGet("Name/{countryName}")]
        public async Task<IActionResult> Get(string countryName)
        {
            var country = await _countryService.GetCountryByNameAsync(countryName);

            return ResultModel<CountryDto>.ToActionResult(country);
        }

        [HttpGet("All")]
        public async Task<IActionResult> Get()
        {
            var countries = await _countryService.GetAllCountries();

            return ResultModel<IEnumerable<CountryDto>>.ToActionResult(countries);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CountryDto countryDto)
        {
            var createCountryResult = await _countryService.CreateCountryAsync(countryDto);

            return ResultModel<CountryDto>.ToActionResult(createCountryResult);
        }
    }
}
