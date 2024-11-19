// ReSharper disable InconsistentNaming

namespace WeatherSystem.Shared.DTO.AirQualityForecast;

public class Hourly
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Hourly()

    {
            
    }

    public Hourly(List<DateTime> time, List<int> european_aqi_pm10, List<int> european_aqi_pm2_5, List<int> european_aqi_nitrogen_dioxide, List<int> european_aqi_ozone, List<int> european_aqi_sulphur_dioxide)
    {
        this.time = time;
        this.european_aqi_pm10 = european_aqi_pm10;
        this.european_aqi_pm2_5 = european_aqi_pm2_5;
        this.european_aqi_nitrogen_dioxide = european_aqi_nitrogen_dioxide;
        this.european_aqi_ozone = european_aqi_ozone;
        this.european_aqi_sulphur_dioxide = european_aqi_sulphur_dioxide;
    }

    public List<DateTime> time { get; set; }
    public List<int> european_aqi_pm10 { get; set; }
    public List<int> european_aqi_pm2_5 { get; set; }
    public List<int> european_aqi_nitrogen_dioxide { get; set; }
    public List<int> european_aqi_ozone { get; set; }
    public List<int> european_aqi_sulphur_dioxide { get; set; }

    public List<AirQuality> ToAirQualityCollection(string cityName)
    {
        var airQualities = new List<AirQuality>();
        for (int i = 0; i < time.Count; i++)
        {
            airQualities.Add(CreateAirQuality(cityName, i));
        }
        return airQualities;
    }

    public AirQuality CreateAirQuality(string cityName, int index) => new(time[index], european_aqi_pm10[index], european_aqi_pm2_5[index],
        european_aqi_nitrogen_dioxide[index], european_aqi_ozone[index], european_aqi_sulphur_dioxide[index], cityName);
}