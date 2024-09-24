namespace Calappoint.API.Extensions;

internal static class SwaggerExtensions
{
    internal static IServiceCollection AddSwaggerDoc(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "Calappoint API",
                Version = "v1"
            });

            options.CustomSchemaIds(t => t.FullName?.Replace("+", "."));

        });
        return services;
    }
}
