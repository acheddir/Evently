namespace Evently.Modules.Events.Infrastructure;

public static class EventsModule
{
    public static IServiceCollection AddEventModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpoints(Presentation.AssemblyReference.Assembly);
        services.AddEventsInfrastructure(configuration);

        return services;
    }

    private static void AddEventsInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string dbConnectionString = configuration.GetConnectionString("Database");

        services.AddDbContextFactory<EventsDbContext>((sp, options) =>
            options
                .UseNpgsql(
                    dbConnectionString,
                    npgsqlOptions =>
                        npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Events))
                .UseSnakeCaseNamingConvention()
                .AddInterceptors(sp.GetRequiredService<PublishDomainEventsInterceptor>()));

        services.AddScoped<IEventsUnitOfWork, EventsUnitOfWork>();

        services.AddScoped<IEventsApi, EventsApi>();
    }
}
