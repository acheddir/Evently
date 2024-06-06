namespace Evently.Common.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string dbConnectionString,
        Assembly[] assemblies)
    {
        NpgsqlDataSource npgsqlDataSource = new NpgsqlDataSourceBuilder(dbConnectionString).Build();
        services.TryAddSingleton(npgsqlDataSource);

        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();

        services.TryAddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddAutoMapper(assemblies);

        return services;
    }
}
