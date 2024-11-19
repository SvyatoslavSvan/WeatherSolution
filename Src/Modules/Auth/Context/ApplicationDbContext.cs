using AuthorizationModule.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthorizationModule.Context;

public sealed class ApplicationDbContext(DbContextOptions opts) : IdentityDbContext<ApplicationUser>(opts)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseOpenIddict();
    }
}