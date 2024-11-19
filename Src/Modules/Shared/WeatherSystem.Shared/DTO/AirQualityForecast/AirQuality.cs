#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace WeatherSystem.Shared.DTO.AirQualityForecast;

public class AirQuality
{

    public AirQuality()
    {
            
    }

    public AirQuality(DateTime time, int europeanAirQualityPm10, int europeanAirQualityPm25,
        int europeanAirQualityNitrogenDioxide, int europeanAirQualityOzone, int europeanAirQualitySulphurDioxide, string cityName)
    {
        EuropeanAirQualityPm10 = europeanAirQualityPm10;
        EuropeanAirQualityPm25 = europeanAirQualityPm25;
        EuropeanAirQualityNitrogenDioxide = europeanAirQualityNitrogenDioxide;
        EuropeanAirQualityOzone = europeanAirQualityOzone;
        Time = time;
        EuropeanAirQualitySulphurDioxide = europeanAirQualitySulphurDioxide;
        CityName = cityName;
    }

    public DateTime Time { get; set; } 
    public int EuropeanAirQualityPm25 { get; set; }
    public int EuropeanAirQualityPm10 { get; set; }
    public int EuropeanAirQualityNitrogenDioxide { get; set; }
    public int EuropeanAirQualityOzone { get; set; }
    public int EuropeanAirQualitySulphurDioxide { get; set; }
    public string CityName { get; set; }

}