using AutoMapper;
using Calabonga.UnitOfWork;
using DataCoreModule.Application.Interfaces.Data;
using DataCoreModule.Application.Services;
using DataCoreModule.Core.Models.Entities;
using Moq;
using System.Linq.Expressions;

namespace DataCore.Tests.Application
{
    public class CityServiceTests
    {
        private readonly Mock<IUnitOfWorkAdapter> _unitOfWorkMock;
        private readonly Mock<IRepository<City>> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly CityService _cityService;

        public CityServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWorkAdapter>();
            _repositoryMock = new Mock<IRepository<City>>();
            _mapperMock = new Mock<IMapper>();

            _unitOfWorkMock.Setup(x => x.GetRepository<City>()).Returns(_repositoryMock.Object);
            _cityService = new CityService(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Create_Should_NotInsertCity_When_NameExists()
        {
            // Arrange
            var city = new City("TestCity");
            _repositoryMock
                .Setup(r => r.ExistsAsync(It.IsAny<Expression<Func<City, bool>>>(), CancellationToken.None))
                .ReturnsAsync(true);

            // Act
            var result = await _cityService.Create(city);

            // Assert
            _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<City>(), CancellationToken.None), Times.Never);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Never);
            Assert.Equal(city, result);
        }

        [Fact]
        public async Task UpsertMonitoringCityByName_Should_InsertCity_When_CityDoesNotExist()
        {
            // Arrange
            var cityName = "NewCity";
            _repositoryMock
                .Setup(r => r.ExistsAsync(It.IsAny<Expression<Func<City, bool>>>(), CancellationToken.None))
                .ReturnsAsync(true);

            // Act
            var result = await _cityService.UpsertMonitoringCityByName(cityName);

            // Assert
            _repositoryMock.Verify(r => r.InsertAsync(It.Is<City>(c => c.Name == cityName && c.IsMonitored), CancellationToken.None), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
            Assert.Equal(cityName, result.Name);
            Assert.True(result.IsMonitored);
        }
    }
}
