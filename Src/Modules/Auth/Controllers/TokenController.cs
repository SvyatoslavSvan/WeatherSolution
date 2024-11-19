using System.Security.Claims;
using AuthorizationModule.Factories;
using AuthorizationModule.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;

namespace AuthorizationModule.Controllers;

[Route("connect")]
[ApiController]
public sealed class TokenController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationUserClaimsFactory claimsFactory) : ControllerBase
{
    [HttpPost("token")]
    public async Task<IActionResult> Exchange()
    {
        var request = HttpContext.GetOpenIddictServerRequest() ?? throw new NullReferenceException();

        if (!request.IsPasswordGrantType() || request.Username == null)
            return Problem("The specified grant type is not supported.");

        var user = await userManager.FindByNameAsync(request.Username);
        if (user is null || !await signInManager.CanSignInAsync(user) ||
            userManager.SupportsUserLockout 
            && await userManager.IsLockedOutAsync(user))
        {
            return Problem("Invalid Operation");
        }
        if (request.Password != null && !await userManager.CheckPasswordAsync(user,request.Password))
        {
            if (userManager.SupportsUserLockout)
            {
                await userManager.AccessFailedAsync(user);
            }
            return BadRequest("Username or password incorrect");
        }
        if (userManager.SupportsUserLockout)
        {
            await userManager.ResetAccessFailedCountAsync(user);
        }

        var principal = await claimsFactory.CreateAsync(user);
        return SignIn(principal, null, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
    }

}