using System.Security.Claims;
using AuthorizationModule.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using OpenIddict.Abstractions;

namespace AuthorizationModule.Factories
{
    public sealed class ApplicationUserClaimsFactory(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IOptions<IdentityOptions> options)
        : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>(userManager, roleManager, options)
    {
        public override async Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            var claims = await base.CreateAsync(user);
            AddSubjectClaim(claims,user);
            AddProfileClaims(claims, user);
            return claims;
        }

        private static void AddProfileClaims(ClaimsPrincipal principal, ApplicationUser user)
        {
            if (principal.Identity is not ClaimsIdentity identity) 
                return;
            AddClaimIfNotEmpty(identity, OpenIddictConstants.Claims.Name, user.UserName);
            AddClaimIfNotEmpty(identity, OpenIddictConstants.Claims.Email, user.Email);
            AddClaimIfNotEmpty(identity, nameof(ApplicationUser.Permission), user.Permission);
        }

        private static void AddClaimIfNotEmpty(ClaimsIdentity identity, string claimType, string? value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                identity.AddClaim(new Claim(claimType, value)
                    .SetDestinations(OpenIddictConstants.Destinations.IdentityToken,
                        OpenIddictConstants.Destinations.AccessToken));
            }
        }

        private static void AddSubjectClaim(ClaimsPrincipal principal, IdentityUser user)
        {
            if (principal.Claims.Any(c => c.Type == OpenIddictConstants.Claims.Subject)) 
                return;
            var identity = (ClaimsIdentity)principal.Identity!;
            identity.AddClaim(new Claim(OpenIddictConstants.Claims.Subject, user.Id));
        }
    }
}
