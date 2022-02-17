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
                countriesResultToReturn = ResultModel<IList<CountryDto>>.GetError(ErrorCode.ValidationError, "Empty contry name");
            }
            else
            {
                var countries = await _unitOfWork.Countries.GetCountriesByNameFragement(countryName);
                var countriesDto = _mapper.Map<IList<CountryDto>>(countries);

                countriesResultToReturn = ResultModel<IList<CountryDto>>.GetSuccess(countriesDto);
            }

            return countriesResultToReturn;
        }

        public async Task<ResultModel<CountryDto>> CreateCountryAsync(CountryDto countryDto)
        {
            ResultModel<CountryDto> result;

            if (String.IsNullOrWhiteSpace(countryDto.Name))
            {
                result = ResultModel<CountryDto>.GetError(ErrorCode.ValidationError, "Empty contry name");
            }
            else
            {
                try
                {
                    await _unitOfWork.Countries.CreateAsync(_mapper.Map<CountryDto, Country>(countryDto));
                    await _unitOfWork.SaveAsync();

                    result = ResultModel<CountryDto>.GetSuccess(countryDto);
                }
                catch (Exception ex)
                {
                    _unitOfWork.Dispose();
                    result = ResultModel<CountryDto>.GetError(ErrorCode.InternalServerError, $"Database error: {ex.Message}");
                }
            }

            return result;
        }

        public ResultModel<bool> DeleteCountryByIdAsync(long countryId)
        {
            ResultModel<bool> result;

            if (countryId == default)
            {
                result = ResultModel<bool>.GetError(ErrorCode.ValidationError, "Wrong country ID");
            }
            else
            {
                try
                {
                    _unitOfWork.Countries.DeleteById(countryId);
                    result = ResultModel<bool>.GetSuccess(true);
                }
                catch (Exception ex)
                {
                    result = ResultModel<bool>.GetError(ErrorCode.InternalServerError, $"Database error: {ex.Message}");
                }
            }

            return result;
        }

        public async Task<ResultModel<CountryDto>> GetCountryByIdAsync(long countryId)
        {
            ResultModel<CountryDto> result;

            if (countryId == default)
            {
                result = ResultModel<CountryDto>.GetError(ErrorCode.ValidationError, "Wrong country ID");
            }
            else
            {
                var country = await _unitOfWork.Countries.GetByIdAsync(countryId);

                if (country != default)
                {
                    result = ResultModel<CountryDto>.GetSuccess(_mapper.Map<Country, CountryDto>(country));
                }
                else
                {
                    result = ResultModel<CountryDto>.GetError(ErrorCode.NotFound, $"Country with ID: {countryId} not found");
                }
            }

            return result;
        }

        public async Task<ResultModel<CountryDto>> GetCountryByNameAsync(string countryName)
        {
            ResultModel<CountryDto> result;

            if (String.IsNullOrWhiteSpace(countryName))
            {
                result = ResultModel<CountryDto>.GetError(ErrorCode.ValidationError, "Country name is empty");
            }
            else
            {
                var country = await _unitOfWork.Countries.GetCountryByNameAsync(countryName); 

                if (country != default)
                {
                    result = ResultModel<CountryDto>.GetSuccess(_mapper.Map<Country, CountryDto>(country));
                }
                else
                {
                    result = ResultModel<CountryDto>.GetError(ErrorCode.NotFound, $"Country with name: {countryName} not found");
                }
            }

            return result;
        }

        public async Task<ResultModel<CountryDto>> UpdateCountry(CountryDto countryDto)
        {
            ResultModel<CountryDto> result;

            if (countryDto == default)
            {
                result = ResultModel<CountryDto>.GetError(ErrorCode.ValidationError, "Given country model validation error");
            }
            else
            {
                try
                {
                    var doesCountryExist = await _unitOfWork.Countries.DoesExistByIdAsync(countryDto.Id);

                    if (doesCountryExist)
                    {
                        _unitOfWork.Countries.Update(_mapper.Map<CountryDto, Country>(countryDto));

                        result = ResultModel<CountryDto>.GetSuccess(countryDto);
                    }
                    else
                    {
                        result = ResultModel<CountryDto>.GetError(ErrorCode.NotFound, $"Country with ID: {countryDto.Id} not found");
                    }
                }
                catch (Exception ex)
                {
                    _unitOfWork.Dispose();
                    result = ResultModel<CountryDto>.GetError(ErrorCode.InternalServerError, $"Database error: {ex.Message}");
                }
            }

            return result;
        }
    }
}
