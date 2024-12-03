using DataCoreModule.Core.Models;
using DataCoreModule.Core.Models.Entities;

namespace DataCore.Tests.Core
{
    public class EnvironmentalStateTests
    {
        [Fact]
        public void Temperature_Should_ThrowException_When_ValueIsOutOfRange()
        {
            // Arrange
            var city = new City("TestCity");
            var airQuality = new AirQuality(10, 20, 30, 40, 50);
            var date = DateTime.Now;

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                EnvironmentalState.CreateInstance(city, -300, airQuality, date)); // Температура ниже минимальной

            Assert.Throws<ArgumentException>(() =>
                EnvironmentalState.CreateInstance(city, 60, airQuality, date)); // Температура выше максимальной
        }

        [Fact]
        public void Temperature_Should_SetValue_When_ValueIsInRange()
        {
            // Arrange
            var city = new City("TestCity");
            var airQuality = new AirQuality(10, 20, 30, 40, 50);
            var date = DateTime.Now;

            // Act
            var state = EnvironmentalState.CreateInstance(city, 25, airQuality, date);

            // Assert
            Assert.Equal(25, state.Temperature);
        }

        [Fact]
        public void AirQualityMark_Should_ThrowException_When_ValueIsOutOfRange()
        {
            // Arrange
            var city = new City("TestCity");
            var date = DateTime.Now;

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                EnvironmentalState.CreateInstance(city, 25, new AirQuality(200, 20, 30, 40, 50), date)); // Некорректное качество воздуха
        }

        [Fact]
        public void AirQualityMark_Should_SetValue_When_ValueIsInRange()
        {
            // Arrange
            var city = new City("TestCity");
            var airQuality = new AirQuality(10, 20, 30, 40, 50);
            var date = DateTime.Now;

            // Act
            var state = EnvironmentalState.CreateInstance(city, 25, airQuality, date);

            // Assert
            Assert.InRange(state.AirQualityMark, 1, 5);
        }

        [Fact]
        public void CreateInstance_Should_CreateEnvironmentalState_WithValidInputs()
        {
            // Arrange
            var city = new City("TestCity");
            var airQuality = new AirQuality(10, 20, 30, 40, 50);
            var date = DateTime.Now;

            // Act
            var state = EnvironmentalState.CreateInstance(city, 25, airQuality, date);

            // Assert
            Assert.Equal(city, state.City);
            Assert.Equal(25, state.Temperature);
            Assert.Equal(date, state.Date);
            Assert.InRange(state.AirQualityMark, 1, 5);
        }

    }
}
