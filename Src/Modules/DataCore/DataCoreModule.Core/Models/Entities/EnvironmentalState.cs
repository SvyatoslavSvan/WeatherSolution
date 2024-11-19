using DataCoreModule.Core.Models.Entities.Base;

namespace DataCoreModule.Core.Models.Entities;

public sealed class EnvironmentalState : Entity
{
    private const double MinAllowedTemperatureValue = 56.7;
    private const double MaxAllowedTemperatureValue = -273.15;
    private const int MaxAirQualityMarkValue = 5;
    private const int MinAirQualityMarkValue = 1;

    private const int PerfectAqiValue = 20;
    private const int SatisfactoryAqiValue = 40;
    private const int ModerateAqiValue = 60;
    private const int PoorAqiValue = 100;

    private readonly float _temperature;
    private readonly double _airQualityMark;
    //ef constructor
#pragma warning disable CS8618 
    private EnvironmentalState() {  }
#pragma warning restore CS8618


    private EnvironmentalState(City city, float temperature, double airQualityMark, DateTime date)
    {
        City = city;
        Temperature = temperature;
        AirQualityMark = airQualityMark;
        Date = date;
    }

    private static readonly List<AirQualityThreshold> Thresholds =
    [
        new() { Threshold = PerfectAqiValue, Mark = 5 },
        new() { Threshold = SatisfactoryAqiValue, Mark = 4 },
        new() { Threshold = ModerateAqiValue, Mark = 3 },
        new() { Threshold = PoorAqiValue, Mark = 2 }
    ];

    public static EnvironmentalState CreateInstance(City city, float temperature, AirQuality quality, DateTime date) => new(city, temperature, MapToAirQualityMark(quality), date);

    public DateTime Date { get; init; }

    public City City { get; set; }

    public float Temperature
    {
        get => _temperature;
        init
        {
            if (value < MaxAllowedTemperatureValue || value > MinAllowedTemperatureValue)
                throw new ArgumentException($"{nameof(Temperature)} must be between {MinAllowedTemperatureValue} and {MaxAllowedTemperatureValue}", nameof(Temperature));
            _temperature = value;
        }
    }

    public double AirQualityMark
    {
        get => _airQualityMark;
        init
        {
            if (value is < MinAirQualityMarkValue or > MaxAirQualityMarkValue)
                throw new ArgumentException($"{nameof(AirQualityMark)} cannot be lower than {MinAirQualityMarkValue} or greater than {MaxAirQualityMarkValue}",
                    nameof(AirQualityMark));
            _airQualityMark = value;
        }
    }

    private static int ConvertAqiMarkToAirQualityMark(int value)
    {
        var result = Thresholds.FirstOrDefault(t => value <= t.Threshold);
        return result?.Mark ?? 1;
    }

    private static double MapToAirQualityMark(AirQuality quality) 
    {
        var properties = typeof(AirQuality).GetProperties();
        var marks = new List<int>(properties.Length);
        foreach (var property in properties)
        {
            if (property.PropertyType == typeof(int))
            {
                marks.Add(ConvertAqiMarkToAirQualityMark((int)property.GetValue(quality)!));
            }
        }
        return marks.Average();
    }

    private class AirQualityThreshold
    {
        public int Threshold { get; init; }
        public int Mark { get; init; }
    }
}