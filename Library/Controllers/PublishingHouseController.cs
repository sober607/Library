using Library.Business.DTO.PublishingHouse;
using Library.Business.Model.ResultModel;
using Library.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishingHouseController : ControllerBase
    {
        private IPublishingHouseService _publishingHouseService;

        public PublishingHouseController(IPublishingHouseService publishingHouseService)
        {
            _publishingHouseService = publishingHouseService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(PublishingHouseDto publishingHouseDto)
        {
            var createResult = await _publishingHouseService.CreatePublishingHouseAsync(publishingHouseDto);

            return ResultModel<PublishingHouseDto>.ToActionResult(createResult);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long publishingHouseId)
        {
            var deleteResult = await _publishingHouseService.DeletePublishingHouseByIdAsync(publishingHouseId);

            return ResultModel<bool>.ToActionResult(deleteResult);
        }

        [HttpGet]
        public async Task<IActionResult> Get(long publishingHouseId)
        {
            var publishingHouse = await _publishingHouseService.GetPublishingHouseByIdAsync(publishingHouseId);

            return ResultModel<PublishingHouseDto>.ToActionResult(publishingHouse);
        }

        [HttpPut]
        public async Task<IActionResult> Update(PublishingHouseDto publishingHouseDto)
        {
            var updatedPublishingHouseResult = await _publishingHouseService.UpdatePublishingHouseAsync(publishingHouseDto);

            return ResultModel<PublishingHouseDto>.ToActionResult(updatedPublishingHouseResult);
        }

        [HttpGet("CheckExistance/{publishingHouseId}")]
        public async Task<IActionResult> CheckExistance(long publishingHouseId)
        {
            var updatedPublishingHouseResult = await _publishingHouseService.DoesPublishingHouseExistAsync(publishingHouseId);

            return ResultModel<bool>.ToActionResult(updatedPublishingHouseResult);
        }
    }
}
