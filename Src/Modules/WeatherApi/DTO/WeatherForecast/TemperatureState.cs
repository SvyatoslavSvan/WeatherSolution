namespace WeatherForecast.DTO.WeatherForecast
{
    public class TemperatureState(DateTime dateTime, float temperature, string cityName)
    {
        public DateTime Time { get; init; } = dateTime;

        public float Temperature { get; init; } = temperature;

        public string CityName { get; init; } = cityName;

    }
}
