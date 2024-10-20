namespace Evently.Modules.Ticketing.Infrastructure;

public static class TicketingModule
{
    public static IServiceCollection AddTicketingModule(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTicketingInfrastructure(configuration);

        services.AddEndpoints(Presentation.AssemblyReference.Assembly);

        return services;
    }

    public static void ConfigureConsumers(IRegistrationConfigurator registrationConfigurator)
    {
        registrationConfigurator.AddConsumer<UserRegisteredIntegrationEventConsumer>();
        registrationConfigurator.AddConsumer<UserProfileUpdatedIntegrationEventConsumer>();
    }

    private static void AddTicketingInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string dbConnectionString = configuration.GetConnectionString("Database");

        services.AddDbContextFactory<TicketingDbContext>((sp, options) =>
            options
                .UseNpgsql(
                    dbConnectionString,
                    npgsqlOptions => npgsqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Ticketing))
                .UseSnakeCaseNamingConvention()
                .AddInterceptors(sp.GetRequiredService<PublishDomainEventsInterceptor>()));

        services.AddSingleton<CartService>();

        services.AddScoped<ITicketingUnitOfWork, TicketingUnitOfWork>();
    }
}
