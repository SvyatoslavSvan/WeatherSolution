using System.ComponentModel.DataAnnotations;

namespace DataCoreModule.Infrastructure.Presenters.BindingModels.City.Base;

public class WritableCityBindingModel
{
    [Required]
    public string Name { get; set; } = null!;
}