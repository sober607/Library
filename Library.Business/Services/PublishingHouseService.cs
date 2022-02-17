using AutoMapper;
using System;
using System.Threading.Tasks;
using Library.Business.DTO.PublishingHouse;
using Library.Business.Model.ResultModel;
using Library.Business.Services.Interfaces;
using Library.DAL.UnitOfWork;
using Library.DAL.Entities;

namespace Library.Business.Services
{
    public class PublishingHouseService : IPublishingHouseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PublishingHouseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResultModel<PublishingHouseDto>> CreatePublishingHouseAsync(PublishingHouseDto publishingHouseDto)
        {
            ResultModel<PublishingHouseDto> result;

            if (publishingHouseDto == default)
            {
                result = ResultModel<PublishingHouseDto>.GetError(ErrorCode.ValidationError, "Empty publishing house model");
            }
            else
            {
                try
                {
                    var publishingHouse = _mapper.Map<PublishingHouseDto, PublishingHouse>(publishingHouseDto);
                    await _unitOfWork.PublishingHouses.CreateAsync(publishingHouse);
                    await _unitOfWork.SaveAsync();

                    var mappedResult = _mapper.Map<PublishingHouse, PublishingHouseDto>(publishingHouse);
                    result = ResultModel<PublishingHouseDto>.GetSuccess(mappedResult);
                }
                catch (Exception ex)
                {
                    result = ResultModel<PublishingHouseDto>.GetError(ErrorCode.InternalServerError, $"Database error: {ex.Message}");
                }
            }

            return result;
        }

        public async Task<ResultModel<bool>> DeletePublishingHouseByIdAsync(long publishingHouseId)
        {
            ResultModel<bool> result;

            if (publishingHouseId == default)
            {
                result = ResultModel<bool>.GetError(ErrorCode.ValidationError, "Wrong publishing house ID");
            }
            else
            {
                try
                {
                    await _unitOfWork.PublishingHouses.DeleteById(publishingHouseId);
                    result = ResultModel<bool>.GetSuccess(true);
                }
                catch (Exception ex)
                {
                    result = ResultModel<bool>.GetError(ErrorCode.InternalServerError, $"Database error: {ex.Message}");
                }
            }

            return result;
        }

        public async Task<ResultModel<PublishingHouseDto>> GetPublishingHouseByIdAsync(long publishingHouseId)
        {
            ResultModel<PublishingHouseDto> result;

            if (publishingHouseId == default)
            {
                result = ResultModel<PublishingHouseDto>.GetError(ErrorCode.ValidationError, "Wrong publishing house ID");
            }
            else
            {
                var publishingHouse = await _unitOfWork.PublishingHouses.GetByIdAsync(publishingHouseId);
                var mappedEntity = _mapper.Map<PublishingHouse, PublishingHouseDto>(publishingHouse);
                result = ResultModel<PublishingHouseDto>.GetSuccess(mappedEntity);
            }

            return result;
        }

        public ResultModel<PublishingHouseDto> UpdatePublishingHouse(PublishingHouseDto publishingHouseDto)
        {
            ResultModel<PublishingHouseDto> result;

            if (publishingHouseDto == default)
            {
                result = ResultModel<PublishingHouseDto>.GetError(ErrorCode.ValidationError, "Wrong publishing house model");
            }
            else
            {
                try
                {
                    var mappedEntity = _mapper.Map<PublishingHouseDto, PublishingHouse>(publishingHouseDto);
                    _unitOfWork.PublishingHouses.Update(mappedEntity);

                    var mappedResult = _mapper.Map<PublishingHouse, PublishingHouseDto>(mappedEntity);
                    result = ResultModel<PublishingHouseDto>.GetSuccess(mappedResult);
                }
                catch (Exception ex)
                {
                    result = ResultModel<PublishingHouseDto>.GetError(ErrorCode.InternalServerError, $"Database error: {ex.Message}");
                }
            }

            return result;
        }
    }
}
