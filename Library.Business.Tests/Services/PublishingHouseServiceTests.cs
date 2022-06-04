using AutoMapper;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using Library.Business.DTO.PublishingHouse;
using Library.Business.Model.ResultModel;
using Library.Business.Services;
using Library.DAL.Entities;
using Library.DAL.UnitOfWork;

namespace Library.Business.Tests.Services
{
    public class PublishingHouseServiceTests
    {
        private MockRepository mockRepository;

        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;

        public PublishingHouseServiceTests()
        {
            mockRepository = new MockRepository(MockBehavior.Strict);

            _mockUnitOfWork = this.mockRepository.Create<IUnitOfWork>();
            _mockMapper = this.mockRepository.Create<IMapper>();
        }

        private PublishingHouseService CreateService()
        {
            return new PublishingHouseService(
                this._mockUnitOfWork.Object,
                this._mockMapper.Object);
        }

        [Fact]
        public async Task CreatePublishingHouseAsync_PublishingHouseNullEntityPassed_ReturnsError()
        {
            // Arrange
            var service = this.CreateService();
            PublishingHouseDto publishingHouseDto = null;
            var expectedErrorCode = ErrorCode.ValidationError;

            // Act
            var result = await service.CreatePublishingHouseAsync(
                publishingHouseDto);

            // Assert
            Assert.NotNull(result.Error);
            Assert.Equal(expectedErrorCode, result.Error.Code); // A bit integration test. Due to unability to Mock ResultModel static class.
        }

        [Fact]
        public async Task CreatePublishingHouseAsync_CorrectPublishingHouseEntityPassed_EntitySaved()
        {
            // Arrange
            var service = this.CreateService();
            PublishingHouseDto publishingHouseDtoToCreate = new PublishingHouseDto()
            {
                Name = "Marvel"
            };

            PublishingHouse publishingHouseAfterMapping = new PublishingHouse()
            {
                Name = "Marvel"
            };

            PublishingHouseDto publishingHouseCreated = new PublishingHouseDto()
            {
                Id = 1,
                Name = "Marvel"
            };

            _mockMapper.Setup(data => data.Map<PublishingHouseDto, PublishingHouse>(It.IsAny<PublishingHouseDto>())).Returns(publishingHouseAfterMapping);
            _mockUnitOfWork.Setup(data => data.PublishingHouses.CreateAsync(It.IsAny<PublishingHouse>())).Returns(Task.FromResult(default(object)));
            _mockUnitOfWork.Setup(data => data.SaveAsync()).Returns(Task.FromResult(default(object)));
            _mockMapper.Setup(data => data.Map<PublishingHouse, PublishingHouseDto>(It.IsAny<PublishingHouse>())).Returns(publishingHouseCreated);

            // Act
            var result = await service.CreatePublishingHouseAsync(
                publishingHouseDtoToCreate);

            // Assert
            Assert.NotNull(result.Data);
            Assert.Null(result.Error);
        }

        [Fact]
        public async Task CreatePublishingHouseAsync_DatabaseErrorThrown_ErrorReturned()
        {
            // Arrange
            var service = this.CreateService();
            PublishingHouseDto publishingHouseDtoToCreate = new PublishingHouseDto()
            {
                Name = "Marvel"
            };

            PublishingHouse publishingHouseAfterMapping = new PublishingHouse()
            {
                Name = "Marvel"
            };

            var expectedErrorCode = ErrorCode.InternalServerError;

            _mockMapper.Setup(data => data.Map<PublishingHouseDto, PublishingHouse>(It.IsAny<PublishingHouseDto>())).Returns(publishingHouseAfterMapping);
            _mockUnitOfWork.Setup(data => data.PublishingHouses.CreateAsync(It.IsAny<PublishingHouse>())).Returns(Task.FromResult(default(object)));
            _mockUnitOfWork.Setup(data => data.SaveAsync()).ThrowsAsync(new Exception());

            // Act
            var result = await service.CreatePublishingHouseAsync(
                publishingHouseDtoToCreate);

            // Assert
            Assert.Null(result.Data);
            Assert.NotNull(result.Error);
            Assert.Equal(expectedErrorCode, result.Error.Code);
        }

        [Fact]
        public async Task DeletePublishingHouseByIdAsync_DefaultEntityIdPassed_ErrorReturned()
        {
            // Arrange
            var service = this.CreateService();
            long publishingHouseIdToPass = default;
            var expectedErrorCode = ErrorCode.ValidationError;

            // Act
            var result = await service.DeletePublishingHouseByIdAsync(publishingHouseIdToPass);

            // Assert
            Assert.NotNull(result.Error);
            Assert.Equal(expectedErrorCode, result.Error.Code); 
        }

        [Fact]
        public async Task DeletePublishingHouseByIdAsync_ExistingEntityIdPassed_SuccessReturned()
        {
            // Arrange
            var service = this.CreateService();
            long publishingHouseIdToPass = 37;
            _mockUnitOfWork.Setup(data => data.PublishingHouses.DeleteById(It.IsAny<long>())).Returns(Task.FromResult(default(object)));
            _mockUnitOfWork.Setup(data => data.SaveAsync()).Returns(Task.FromResult(default(object)));

            // Act
            var result = await service.DeletePublishingHouseByIdAsync(publishingHouseIdToPass);

            // Assert
            Assert.True(result.Data);
            Assert.Null(result.Error);
        }

        [Fact]
        public async Task DeletePublishingHouseByIdAsync_DatabaseExceptionThrown_ErrorReturned()
        {
            // Arrange
            var service = this.CreateService();
            long publishingHouseIdToPass = 37;
            _mockUnitOfWork.Setup(data => data.PublishingHouses.DeleteById(It.IsAny<long>())).Returns(Task.FromResult(default(object)));
            _mockUnitOfWork.Setup(data => data.SaveAsync()).ThrowsAsync(new Exception());
            var expectedErrorCode = ErrorCode.InternalServerError;

            // Act
            var result = await service.DeletePublishingHouseByIdAsync(publishingHouseIdToPass);

            // Assert
            Assert.Equal(expectedErrorCode, result.Error.Code);
            Assert.False(result.Data);
        }

        [Fact]
        public async Task UpdatePublishingHouseAsync_PublishingHouseNullEntityPassed_ReturnsError()
        {
            // Arrange
            var service = this.CreateService();
            PublishingHouseDto publishingHouseDto = null;
            var expectedErrorCode = ErrorCode.ValidationError;

            // Act
            var result = await service.UpdatePublishingHouseAsync(
                publishingHouseDto);

            // Assert
            Assert.NotNull(result.Error);
            Assert.Equal(expectedErrorCode, result.Error.Code);
        }

        [Fact]
        public async Task UpdatePublishingHouseAsync_ExistingPublishingHousePassed_SuccessReturned()
        {
            // Arrange
            var service = this.CreateService();
            PublishingHouseDto publishingHouseToUpdate = new PublishingHouseDto()
            {
                Id = 1,
                Name = "Marvel2"
            };

            PublishingHouse publishingHouseAfterMapping = new PublishingHouse()
            {
                Id = 1,
                Name = "Marvel2"
            };

            PublishingHouseDto publishingHouseUpdated = new PublishingHouseDto()
            {
                Id = 1,
                Name = "Marvel"
            };

            _mockMapper.Setup(data => data.Map<PublishingHouseDto, PublishingHouse>(It.IsAny<PublishingHouseDto>())).Returns(publishingHouseAfterMapping);
            _mockUnitOfWork.Setup(data => data.PublishingHouses.Update(It.IsAny<PublishingHouse>()));
            _mockUnitOfWork.Setup(data => data.SaveAsync()).Returns(Task.FromResult(default(object)));
            _mockMapper.Setup(data => data.Map<PublishingHouse, PublishingHouseDto>(It.IsAny<PublishingHouse>())).Returns(publishingHouseUpdated);

            // Act
            var result = await service.UpdatePublishingHouseAsync(
                publishingHouseToUpdate);

            // Assert
            Assert.NotNull(result.Data);
            Assert.Null(result.Error);
        }
    }
}
