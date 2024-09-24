using Calappoint.Application.Abstractions.Data;
using Calappoint.Application.Abstractions.Identity;
using Calappoint.Domain.Appointments;
using Calappoint.Domain.Availabilities;
using Calappoint.Domain.Clients;
using Calappoint.Domain.Users;
using Calappoint.Infrastructure.Appointments;
using Calappoint.Infrastructure.Authentication;
using Calappoint.Infrastructure.Availabilities;
using Calappoint.Infrastructure.Clients;
using Calappoint.Infrastructure.Database;
using Calappoint.Infrastructure.Identity;
using Calappoint.Infrastructure.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Calappoint.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<KeyCloakOptions>(config.GetSection("KeyCloak"));

        services.AddTransient<KeyCloakAuthDelegatingHandler>();

        services.AddHttpClient<KeyCloakClient>((sp, httpClient) =>
        {
            KeyCloakOptions keyCloakOptions = sp.GetRequiredService<IOptions<KeyCloakOptions>>().Value;
            httpClient.BaseAddress = new Uri(keyCloakOptions.AdminUrl);
        })
        .AddHttpMessageHandler<KeyCloakAuthDelegatingHandler>();

        services.AddTransient<IIdentityProviderService, IdentityProviderService>();

        services.AddDbContext<CalappointDbContext>(options =>
        {
            options.UseSqlServer(config.GetConnectionString("Database"));

        });

        services.AddAuthenticationInternal();

        services
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IClientRepository, ClientRepository>()
            .AddScoped<IAvailabilityRepository, AvailabilityRepository>()
            .AddScoped<IAppointmentRepository, AppointmentRepository>();

        services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<CalappointDbContext>());

        return services;
    }
}
