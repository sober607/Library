using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Business.DTO.Country;
using Library.Business.Model.ResultModel;
using Library.Business.Services.Interfaces;
using Library.DAL.UnitOfWork;
using Library.DAL.Entities;
using AutoMapper;

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
                return ResultModel<IList<CountryDto>>.GetError(ErrorCode.ValidationError, "Empty contry name");
            }

            var countries = await _unitOfWork.Countries.GetCountriesByNameFragement(countryName);
            var countriesDto = _mapper.Map<IList<CountryDto>>(countries);

            countriesResultToReturn = ResultModel<IList<CountryDto>>.GetSuccess(countriesDto);

            return countriesResultToReturn;
        }

        public async Task<ResultModel<CountryDto>> CreateCountryAsync(CountryDto countryDto)
        {
            ResultModel<CountryDto> result;

            if (String.IsNullOrWhiteSpace(countryDto.Name))
            {
                return ResultModel<CountryDto>.GetError(ErrorCode.ValidationError, "Empty contry name");
            }

            try
            {
                await _unitOfWork.Countries.CreateAsync(_mapper.Map<CountryDto, Country>(countryDto));
                await _unitOfWork.SaveAsync();

                result = ResultModel<CountryDto>.GetSuccess(countryDto);
            }
            catch (Exception ex)
            {
                _unitOfWork.Dispose();
                return ResultModel<CountryDto>.GetError(ErrorCode.InternalServerError, $"Creation of country is failed: {ex.Message}");
            }

            return result;
        }

        public async Task<ResultModel<bool>> DeleteCountryById(long countryId)
        {
            ResultModel<bool> result;

            if (countryId == default)
            {
                return ResultModel<bool>.GetError(ErrorCode.ValidationError, "Wrong country ID");
            }
            
            try
            {
                await _unitOfWork.Countries.DeleteById(countryId);
                await _unitOfWork.SaveAsync();

                result = ResultModel<bool>.GetSuccess(true);
            }
            catch (Exception ex)
            {
                result = ResultModel<bool>.GetError(ErrorCode.InternalServerError, $"Deletion of country is failed: {ex.Message}");
            }
            
            return result;
        }

        public async Task<ResultModel<CountryDto>> GetCountryByIdAsync(long countryId)
        {
            ResultModel<CountryDto> result;

            if (countryId == default)
            {
                return ResultModel<CountryDto>.GetError(ErrorCode.ValidationError, "Wrong country ID");
            }
            
            var country = await _unitOfWork.Countries.GetByIdAsync(countryId);

            if (country == default)
            {
                return ResultModel<CountryDto>.GetError(ErrorCode.NotFound, $"Country with ID: {countryId} not found");
                
            }

            result = ResultModel<CountryDto>.GetSuccess(_mapper.Map<Country, CountryDto>(country));

            return result;
        }

        public async Task<ResultModel<CountryDto>> GetCountryByNameAsync(string countryName)
        {
            ResultModel<CountryDto> result;

            if (String.IsNullOrWhiteSpace(countryName))
            {
                return ResultModel<CountryDto>.GetError(ErrorCode.ValidationError, "Country name is empty");
            }

            var country = await _unitOfWork.Countries.GetCountryByNameAsync(countryName); 

            if (country == default)
            {
                return ResultModel<CountryDto>.GetError(ErrorCode.NotFound, $"Country with name: {countryName} not found");
            }

            result = ResultModel<CountryDto>.GetSuccess(_mapper.Map<Country, CountryDto>(country));

            return result;
        }

        public async Task<ResultModel<CountryDto>> UpdateCountry(CountryDto countryDto)
        {
            ResultModel<CountryDto> result;

            if (countryDto == default)
            {
                return ResultModel<CountryDto>.GetError(ErrorCode.ValidationError, "Given country model validation error");
            }

            try
            {
                var doesCountryExist = await _unitOfWork.Countries.DoesExistByIdAsync(countryDto.Id);

                if (!doesCountryExist)
                {
                    return ResultModel<CountryDto>.GetError(ErrorCode.NotFound, $"Country with ID: {countryDto.Id} not found");
                }
                _unitOfWork.Countries.Update(_mapper.Map<CountryDto, Country>(countryDto));

                result = ResultModel<CountryDto>.GetSuccess(countryDto);
            }
            catch (Exception ex)
            {
                _unitOfWork.Dispose();
                return ResultModel<CountryDto>.GetError(ErrorCode.InternalServerError, $"Update of country is failed: {ex.Message}");
            }

            return result;
        }

        public async Task<ResultModel<IEnumerable<CountryDto>>> GetAllCountries()
        {
            var countries = await _unitOfWork.Countries.GetAll();
            var mappedCountries = _mapper.Map<IEnumerable<Country>, IEnumerable<CountryDto>>(countries);

            return ResultModel<IEnumerable<CountryDto>>.GetSuccess(mappedCountries);
        }
    }
}
