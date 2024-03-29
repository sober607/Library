﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Business.DTO.Person;
using Library.Business.DTO.PublishingHouse;
using Library.Business.Model.ResultModel;

namespace Library.Business.Services.Interfaces
{
    public interface IPublishingHouseService
    {
        Task<ResultModel<PublishingHouseDto>> GetPublishingHouseByIdAsync(long publishingHouseId);

        Task<ResultModel<PublishingHouseDto>> CreatePublishingHouseAsync(PublishingHouseDto publishingHouseDto);

        Task<ResultModel<bool>> DeletePublishingHouseByIdAsync(long publishingHouseId);

        Task<ResultModel<PublishingHouseDto>> UpdatePublishingHouseAsync(PublishingHouseDto publishingHouseDto);

        Task<ResultModel<bool>> DoesPublishingHouseExistAsync(long? publishingHouseId);

        Task<ResultModel<IEnumerable<PublishingHouseDto>>> GetAllPublishingHouses();
    }
}
