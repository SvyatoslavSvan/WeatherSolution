namespace WeatherSystem.Shared.DTO.WeatherForecast;

public class TemperatureState
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public TemperatureState()

    {
            
    }

    public TemperatureState(DateTime dateTime, float temperature, string cityName)
    {
        Time = dateTime;
        Temperature = temperature;
        CityName = cityName;
    }


    public DateTime Time { get; init; }

    public float Temperature { get; init; }

    public string CityName { get; init; }

}