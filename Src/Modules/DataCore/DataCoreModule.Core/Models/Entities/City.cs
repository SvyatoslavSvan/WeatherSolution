using DataCoreModule.Core.Models.Entities.Base;

namespace DataCoreModule.Core.Models.Entities;

public class City : Entity
{
    private string _name = null!;
    private readonly IList<EnvironmentalState> _environmentalStates = null!;

    private City() { }

    public City(string name)
    {
        _environmentalStates = new List<EnvironmentalState>();
        Name = name;
        IsMonitored = false;
    }

    public City(string name, bool isMonitored)
    {
        _environmentalStates = new List<EnvironmentalState>();
        Name = name;
        IsMonitored = isMonitored;
    }

    public bool IsMonitored { get; set; }

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Name of city cannot be null or empty");
            _name = MakeFirstLetterUpperCase(value);
        }
    }

    public IEnumerable<EnvironmentalState> EnvironmentalEnvironmentalStates
    {
        get => _environmentalStates;
        init => _environmentalStates = value.ToList();
    }

    private static string MakeFirstLetterUpperCase(string value) => char.ToUpper(value[0]) + value[1..];
}