using Library.Business.DTO.Person;
using Library.Business.Model.ResultModel;
using System.Threading.Tasks;

namespace Library.Business.Services.Interfaces
{
    public interface IPersonService
    {
        Task<ResultModel<PersonDto>> GetPersonByIdAsync(long personId);

        Task<ResultModel<PersonDto>> CreatePersonAsync(PersonDto personDto);

        Task<ResultModel<bool>> DeletePersonByIdAsync(long personId);

        Task<ResultModel<PersonDto>> UpdatePerson(PersonDto personDto);
    }
}
