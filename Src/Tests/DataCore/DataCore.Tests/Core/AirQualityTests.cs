using DataCoreModule.Core.Models;

namespace DataCore.Tests.Core
{
    public class AirQualityTests
    {
        [Fact]
        public void Constructor_Should_ThrowException_When_ParametersAreOutOfRange()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentException>(() =>
                new AirQuality(200, 50, 30, 40, 50)); // NitrogenDioxide вне допустимого диапазона

            Assert.Throws<ArgumentException>(() =>
                new AirQuality(20, 120, 30, 40, 50)); // Pm10 вне допустимого диапазона

            Assert.Throws<ArgumentException>(() =>
                new AirQuality(20, 50, -5, 40, 50)); // Pm25 вне допустимого диапазона
        }

        [Fact]
        public void Constructor_Should_SetValues_When_ParametersAreValid()
        {
            // Arrange & Act
            var airQuality = new AirQuality(20, 50, 30, 40, 50);

            // Assert
            Assert.Equal(20, airQuality.EuropeanAirQualityNitrogenDioxide);
            Assert.Equal(50, airQuality.EuropeanAirQualityPm10);
            Assert.Equal(30, airQuality.EuropeanAirQualityPm25);
            Assert.Equal(40, airQuality.EuropeanAirQualityOzone);
            Assert.Equal(50, airQuality.EuropeanAirQualitySulphurDioxide);
        }

        [Fact]
        public void Property_Setters_Should_ThrowException_When_ValuesAreOutOfRange()
        {
            // Arrange
            var airQuality = new AirQuality(20, 50, 30, 40, 50);

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                airQuality.EuropeanAirQualityPm25 = -10); 

            Assert.Throws<ArgumentException>(() =>
                airQuality.EuropeanAirQualityPm10 = 150); 
        }

        [Fact]
        public void Property_Setters_Should_SetValues_When_ValuesAreValid()
        {
            // Arrange
            var airQuality = new AirQuality(20, 50, 30, 40, 50);

            // Act
            airQuality.EuropeanAirQualityPm25 = 60;
            airQuality.EuropeanAirQualityPm10 = 70;

            // Assert
            Assert.Equal(60, airQuality.EuropeanAirQualityPm25);
            Assert.Equal(70, airQuality.EuropeanAirQualityPm10);
        }

        [Fact]
        public void ValidateParameter_Should_ThrowException_When_ValueIsOutOfRange()
        {
            // Arrange
            var airQuality = new AirQuality(20, 50, 30, 40, 50);

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                airQuality.EuropeanAirQualityNitrogenDioxide = -1); 

            Assert.Throws<ArgumentException>(() =>
                airQuality.EuropeanAirQualityOzone = 101); 
        }

        [Fact]
        public void ValidateParameter_Should_NotThrow_When_ValueIsValid()
        {
            // Arrange
            var airQuality = new AirQuality(20, 50, 30, 40, 50);

            // Act
            airQuality.EuropeanAirQualityNitrogenDioxide = 80;
            airQuality.EuropeanAirQualitySulphurDioxide = 90;

            // Assert
            Assert.Equal(80, airQuality.EuropeanAirQualityNitrogenDioxide);
            Assert.Equal(90, airQuality.EuropeanAirQualitySulphurDioxide);
        }
    }
}
