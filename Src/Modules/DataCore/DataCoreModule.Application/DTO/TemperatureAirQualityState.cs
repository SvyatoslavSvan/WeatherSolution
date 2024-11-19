namespace DataCoreModule.Application.DTO;

public class TemperatureAirQualityState
{
    public DateTime Time { get; set; }
    public int EuropeanAirQualityPm25 { get; set; }
    public int EuropeanAirQualityPm10 { get; set; }
    public int EuropeanAirQualityNitrogenDioxide { get; set; }
    public int EuropeanAirQualityOzone { get; set; }
    public int EuropeanAirQualitySulphurDioxide { get; set; }
    public float Temperature { get; set; }
}