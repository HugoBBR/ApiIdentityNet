
using ApiChida.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ApiChida.Repository;

namespace Microsoft.Extensions.DependencyInjection;

public static class IdentityConf
{
    public static IServiceCollection AddIdentityConf(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<ApplicationUser, ApplicationRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApiChida.ApplicationDbContext>()
                   .AddDefaultTokenProviders();
        services.AddScoped<IClaimsManager, ClaimsManager>();


        return services;
    }
}

