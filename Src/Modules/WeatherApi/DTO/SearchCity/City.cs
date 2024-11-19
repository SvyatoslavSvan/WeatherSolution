namespace WeatherForecast.DTO.SearchCity
{
    public class City
    {
        public City(float latitude, float longitude, string name)
        {
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
        }

        public string Name { get; init; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }
    }
}
