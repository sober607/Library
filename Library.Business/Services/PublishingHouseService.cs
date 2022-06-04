using AutoMapper;
using System;
using System.Threading.Tasks;
using Library.Business.DTO.PublishingHouse;
using Library.Business.Model.ResultModel;
using Library.Business.Services.Interfaces;
using Library.DAL.UnitOfWork;
using Library.DAL.Entities;
using System.Collections.Generic;

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
                return ResultModel<PublishingHouseDto>.GetError(ErrorCode.ValidationError, "Empty publishing house model");
            }

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
                result = ResultModel<PublishingHouseDto>.GetError(ErrorCode.InternalServerError, $"Create of publishing house failed: {ex.Message}");
            }

            return result;
        }

        public async Task<ResultModel<bool>> DeletePublishingHouseByIdAsync(long publishingHouseId)
        {
            ResultModel<bool> result;

            if (publishingHouseId == default)
            {
                return ResultModel<bool>.GetError(ErrorCode.ValidationError, "Wrong publishing house ID");
            }

            try
            {
                await _unitOfWork.PublishingHouses.DeleteById(publishingHouseId);
                await _unitOfWork.SaveAsync();
                result = ResultModel<bool>.GetSuccess(true);
            }
            catch (Exception ex)
            {
                result = ResultModel<bool>.GetError(ErrorCode.InternalServerError, $"Delete of publishing house failed: {ex.Message}");
            }

            return result;
        }

        public async Task<ResultModel<PublishingHouseDto>> GetPublishingHouseByIdAsync(long publishingHouseId)
        {
            ResultModel<PublishingHouseDto> result;

            if (publishingHouseId == default)
            {
                return ResultModel<PublishingHouseDto>.GetError(ErrorCode.ValidationError, "Wrong publishing house ID");
            }

            var publishingHouse = await _unitOfWork.PublishingHouses.GetByIdAsync(publishingHouseId);

            if (publishingHouse == default)
            {
                return ResultModel<PublishingHouseDto>.GetError(ErrorCode.NotFound, "Can not find publishing house with given ID");
            }

            var mappedEntity = _mapper.Map<PublishingHouse, PublishingHouseDto>(publishingHouse);
            result = ResultModel<PublishingHouseDto>.GetSuccess(mappedEntity);

            return result;
        }

        public async Task<ResultModel<PublishingHouseDto>> UpdatePublishingHouseAsync(PublishingHouseDto publishingHouseDto)
        {
            ResultModel<PublishingHouseDto> result;

            if (publishingHouseDto == default)
            {
                return ResultModel<PublishingHouseDto>.GetError(ErrorCode.ValidationError, "Wrong publishing house model");
            }

            try
            {
                var mappedEntity = _mapper.Map<PublishingHouseDto, PublishingHouse>(publishingHouseDto);
                _unitOfWork.PublishingHouses.Update(mappedEntity);
                await _unitOfWork.SaveAsync();

                var mappedResult = _mapper.Map<PublishingHouse, PublishingHouseDto>(mappedEntity);
                result = ResultModel<PublishingHouseDto>.GetSuccess(mappedResult);
            }
            catch (Exception ex)
            {
                result = ResultModel<PublishingHouseDto>.GetError(ErrorCode.InternalServerError, $"Update of publishing house failed: {ex.Message}");
            }

            return result;
        }

        public async Task<ResultModel<bool>> DoesPublishingHouseExistAsync(long? publishingHouseId)
        {
            ResultModel<bool> result;

            if (publishingHouseId != default)
            {
                var doesNewPublishingHouseExist = await _unitOfWork.PublishingHouses.DoesExistByIdAsync((long)publishingHouseId);

                result = ResultModel<bool>.GetSuccess(doesNewPublishingHouseExist);
            }
            else
            {
                result = ResultModel<bool>.GetError(ErrorCode.ValidationError, "Wrong publishing house Id");
            }

            return result;
        }

        public async Task<ResultModel<IEnumerable<PublishingHouseDto>>> GetAllPublishingHouses()
        {
            var publishingHouses = await _unitOfWork.PublishingHouses.GetAll();
            var mappedPublishingHouses = _mapper.Map<IEnumerable<PublishingHouse>, IEnumerable<PublishingHouseDto>>(publishingHouses);

            return ResultModel<IEnumerable<PublishingHouseDto>>.GetSuccess(mappedPublishingHouses);
        }
    }
}
