namespace DataCoreModule.Core.Models;

public class AirQuality
{

    private const int MinAqiValue = 0;
    private const int MaxAqiValue = 100;

    private int _europeanAirQualityPm25;
    private int _europeanAirQualityPm10;
    private int _europeanAirQualityNitrogenDioxide;
    private int _europeanAirQualityOzone;
    private int _europeanAirQualitySulphurDioxide;

    public AirQuality(int europeanAirQualityNitrogenDioxide, int europeanAirQualityPm10, int europeanAirQualityPm25,
        int europeanAirQualityOzone, int europeanAirQualitySulphurDioxide)
    {
        EuropeanAirQualityNitrogenDioxide = europeanAirQualityNitrogenDioxide;
        EuropeanAirQualityPm10 = europeanAirQualityPm10;
        EuropeanAirQualityPm25 = europeanAirQualityPm25;
        EuropeanAirQualityOzone = europeanAirQualityOzone;
        EuropeanAirQualitySulphurDioxide = europeanAirQualitySulphurDioxide;
    }

    public int EuropeanAirQualityPm25
    {
        get => _europeanAirQualityPm25;
        set => _europeanAirQualityPm25 = ValidateParameter(value, nameof(EuropeanAirQualityPm25));
    }

    public int EuropeanAirQualityPm10
    {
        get => _europeanAirQualityPm10;
        set => _europeanAirQualityPm10 = ValidateParameter(value, nameof(EuropeanAirQualityPm10));
    }

    public int EuropeanAirQualityNitrogenDioxide
    {
        get => _europeanAirQualityNitrogenDioxide;
        set => _europeanAirQualityNitrogenDioxide =
            ValidateParameter(value, nameof(EuropeanAirQualityNitrogenDioxide));
    }

    public int EuropeanAirQualityOzone
    {
        get => _europeanAirQualityOzone;
        set => _europeanAirQualityOzone = ValidateParameter(value, nameof(EuropeanAirQualityOzone));
    }

    public int EuropeanAirQualitySulphurDioxide
    {
        get => _europeanAirQualitySulphurDioxide;
        set => _europeanAirQualitySulphurDioxide =
            ValidateParameter(value, nameof(EuropeanAirQualitySulphurDioxide));
    }


    private int ValidateParameter(int value, string parameterName)
    {
        if (value is < MinAqiValue or > MaxAqiValue)
            throw new ArgumentException($"{parameterName} must be between {MinAqiValue} and {MaxAqiValue}.", parameterName);
        return value;
    }
}