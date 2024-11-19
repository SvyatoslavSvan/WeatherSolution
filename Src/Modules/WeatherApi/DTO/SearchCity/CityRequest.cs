namespace WeatherForecast.DTO.SearchCity
{
    public class CityRequest
    {
        public required IList<Result> Results { get; set; }

        public IList<City> ToCityCollection()
        {
            var cities = new List<City>();
            foreach (var result in Results)
            {
                cities.Add(new City(result.Latitude, result.Longitude, result.Name));
            }
            return cities;
        }
    }
}
