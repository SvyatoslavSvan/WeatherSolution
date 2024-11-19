using AuthorizationModule.Models;
using AuthorizationModule.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationModule.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class AccountController(UserManager<ApplicationUser> userManager) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        var user = new ApplicationUser(userName: model.Name);
        var result = await userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors.Select(x => x.Description));
        }
        return Ok();
    }
}