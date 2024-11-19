using System.ComponentModel.DataAnnotations;

namespace AuthorizationModule.ViewModels;

public class RegisterViewModel
{
    [Required]
    public string Password { get; set; } = null!;

    [Required]
    public string Name { get; set; } = null!;
}