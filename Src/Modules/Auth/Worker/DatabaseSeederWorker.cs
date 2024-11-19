using System.Security.Claims;
using AuthorizationModule.Models;
using Microsoft.AspNetCore.Identity;
using OpenIddict.Abstractions;

namespace AuthorizationModule.Worker;

public sealed class DatabaseSeederWorker(IServiceProvider serviceProvider) : IHostedService
{
    private static readonly List<OpenIddictApplicationDescriptor> Clients =
    [
        new()
        {
            ClientId = "9D0C424F-E64E-4C6D-8D75-E03F59A979F0",
            DisplayName = "PostMan",
            Permissions =
            {
                OpenIddictConstants.Permissions.Endpoints.Token,
                OpenIddictConstants.Permissions.GrantTypes.Password,
                OpenIddictConstants.Permissions.Scopes.Profile
            },
            ClientSecret = "9D0C424F-2323GFSDf64E-/////4C6D-8D75-E03F59A979F0"
        },
        new()
        {
            ClientId = "7DC04D7A-1D3E-448F-BCFA-5B0655F640DE",
            DisplayName = "ConsoleClient",
            Permissions =
            {
                OpenIddictConstants.Permissions.Endpoints.Token,
                OpenIddictConstants.Permissions.GrantTypes.Password,
                OpenIddictConstants.Permissions.Scopes.Profile
            },
            ClientSecret = "Bss2224FC94A2-Dfd3CF33-4C33/////321d16-8322-80ED6BFEDBB3"
        }
    ];

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        var appManager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        await CreateClients(cancellationToken, appManager, Clients);
        await CreateAdminUser(userManager);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    private static async Task CreateAdminUser(UserManager<ApplicationUser> userManager)
    {
        const string adminName = "Admin";
        if (await userManager.FindByNameAsync(adminName) is null)
        {
            var admin = new ApplicationUser(adminName, "AllAccess");
            await userManager.CreateAsync(admin, "{admin213}F");
        }
    }

    private static async Task CreateClients(CancellationToken cancellationToken,
        IOpenIddictApplicationManager appManager, List<OpenIddictApplicationDescriptor> clients)
    {
        foreach (var item in clients)
        {
            await CreateClient(cancellationToken, appManager, item);
        }
    }

    private static async Task CreateClient(CancellationToken cancellationToken, IOpenIddictApplicationManager appManager,
        OpenIddictApplicationDescriptor item)
    {
        var client = await appManager.FindByClientIdAsync(item.ClientId!, cancellationToken);
        if (client is null)
        {
            await appManager.CreateAsync(item, cancellationToken);
        }
    }
}