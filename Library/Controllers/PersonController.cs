using Library.Business.DTO.Person;
using Library.Business.Model.ResultModel;
using Library.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(PersonDto personDto)
        {
            var creationResult = await _personService.CreatePersonAsync(personDto);

            return ResultModel<PersonDto>.ToActionResult(creationResult);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long personId)
        {
            var deleteResult = await _personService.DeletePersonByIdAsync(personId);

            return ResultModel<bool>.ToActionResult(deleteResult);
        }

        [HttpGet]
        public async Task<IActionResult> Get(long personId)
        {
            var person = await _personService.GetPersonByIdAsync(personId);

            return ResultModel<PersonDto>.ToActionResult(person);
        }

        [HttpPut]
        public async Task<IActionResult> Update(PersonDto personDto)
        {
            var updatePersonrResult = await _personService.UpdatePerson(personDto);

            return ResultModel<PersonDto>.ToActionResult(updatePersonrResult);
        }
    }
}
