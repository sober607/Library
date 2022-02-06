using AutoMapper;
using Library.Business.DTO.Country;
using Library.Business.Model.ResultModel;
using Library.Business.Services.Interfaces;
using Library.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Business.Services
{
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CountryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ResultModel<IList<CountryDto>>> CheckSimilarCountriesByNameAsync(string countryName)
        {
            ResultModel<IList<CountryDto>> countriesResultToReturn;

            if (String.IsNullOrWhiteSpace(countryName))
            {
                countriesResultToReturn = ResultModel<IList<CountryDto>>.GetError(ErrorCode.ValidationError, "Empty contry name");
            }
            else
            {
                var countries = await _unitOfWork.Countries.GetCountriesByNameFragement(countryName).ToListAsync();
                var countriesDto = _mapper.Map<IList<CountryDto>>(countries);

                countriesResultToReturn = ResultModel<IList<CountryDto>>.GetSuccess(countriesDto);
            }

            return countriesResultToReturn;
        }

        public Task<ResultModel<CountryDto>> CreateCountryAsync(CreateCountryDto countryDto)
        {
            throw new NotImplementedException();
        }

        public Task<ResultModel<bool>> DeleteCountryByIdAsync(long countryId)
        {
            throw new NotImplementedException();
        }

        public Task<ResultModel<CountryDto>> GetCountryByIdAsync(long countryId)
        {
            throw new NotImplementedException();
        }

        public Task<ResultModel<CountryDto>> GetCountryByNameAsync(string countryName)
        {
            throw new NotImplementedException();
        }

        public Task<ResultModel<CountryDto>> UpdateCountryAsync(long countryId, UpdateCountryDto countryDto)
        {
            throw new NotImplementedException();
        }
    }
}
