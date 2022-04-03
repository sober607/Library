using AutoMapper;
using Library.Business.DTO.Person;
using Library.Business.Model.ResultModel;
using Library.Business.Services.Interfaces;
using Library.DAL.Entities;
using Library.DAL.UnitOfWork;
using System;
using System.Threading.Tasks;

namespace Library.Business.Services
{
    public class PersonService : IPersonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PersonService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResultModel<PersonDto>> CreatePersonAsync(PersonDto personDto)
        {
            ResultModel<PersonDto> result;

            if (personDto == default)
            {
                return ResultModel<PersonDto>.GetError(ErrorCode.ValidationError, "Empty person model");
            }

            try
            {
                var person = _mapper.Map<PersonDto, Person>(personDto);
                await _unitOfWork.Persons.CreateAsync(person);
                await _unitOfWork.SaveAsync();

                var mappedPersonDto = _mapper.Map<Person, PersonDto>(person);

                result = ResultModel<PersonDto>.GetSuccess(mappedPersonDto);
            }
            catch (Exception ex)
            {
                result = ResultModel<PersonDto>.GetError(ErrorCode.InternalServerError, $"Create of person failed: {ex.Message}");
            }

            return result;
        }

        public async Task<ResultModel<bool>> DeletePersonByIdAsync(long personId)
        {
            ResultModel<bool> result;

            if (personId == default)
            {
                result = ResultModel<bool>.GetError(ErrorCode.ValidationError, "Wrong person ID");
            }

            try
            {
                var personExists = await _unitOfWork.Persons.DoesExistByIdAsync(personId);
                    
                if (!personExists)
                {
                    return ResultModel<bool>.GetError(ErrorCode.NotFound, $"Person with ID: {personId} not found");
                }

                await _unitOfWork.Persons.DeleteById(personId);
                await _unitOfWork.SaveAsync();

                result = ResultModel<bool>.GetSuccess(true);
            }
            catch (Exception ex)
            {
                result = ResultModel<bool>.GetError(ErrorCode.InternalServerError, $"Delete of person failed: {ex.Message}");
            }

            return result;
        }

        public async Task<ResultModel<PersonDto>> GetPersonByIdAsync(long personId)
        {
            ResultModel<PersonDto> result;

            if (personId == default)
            {
                return ResultModel<PersonDto>.GetError(ErrorCode.ValidationError, "Wrong person ID");
            }
            
            var person = await _unitOfWork.Persons.GetByIdAsync(personId);
            var mappedEntity = _mapper.Map<Person, PersonDto>(person);
            result = ResultModel<PersonDto>.GetSuccess(mappedEntity);

            return result;
        }

        public async Task<ResultModel<PersonDto>> UpdatePerson(PersonDto personDto)
        {
            ResultModel<PersonDto> result;

            if (personDto == default)
            {
                return ResultModel<PersonDto>.GetError(ErrorCode.ValidationError, "Wrong person model");
            }

            try
            {
                var personExists = await _unitOfWork.Persons.DoesExistByIdAsync(personDto.Id);

                if (!personExists)
                {
                    return ResultModel<PersonDto>.GetError(ErrorCode.NotFound, $"Person with ID: {personDto.Id} not found");
                }

                var mappedEntity = _mapper.Map<PersonDto, Person>(personDto);
                _unitOfWork.Persons.Update(mappedEntity);
                await _unitOfWork.SaveAsync();

                var mappedResult = _mapper.Map<Person, PersonDto>(mappedEntity);
                result = ResultModel<PersonDto>.GetSuccess(mappedResult);
            }
            catch (Exception ex)
            {
                result = ResultModel<PersonDto>.GetError(ErrorCode.InternalServerError, $"Update of person failed: {ex.Message}");
            }

            return result; 
        }
    }
}
