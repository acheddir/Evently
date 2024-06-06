namespace Evently.Modules.Events.Infrastructure;

public static class EventsModule
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        EventEndpoints.MapEndpoints(app);
        TicketTypeEndpoints.MapEndpoints(app);
        CategoryEndpoints.MapEndpoints(app);
    }

    public static IServiceCollection AddEventModule(this IServiceCollection services, IConfiguration configuration)
    {
        string dbConnectionString = configuration.GetConnectionString("Database");

        services.AddDbContext<EventsDbContext>(options =>
            options
                .UseNpgsql(
                    dbConnectionString,
                    npgsqlOptions =>
                        npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Events))
                .UseSnakeCaseNamingConvention());

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<EventsDbContext>());

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<ITicketTypeRepository, TicketTypeRepository>();

        return services;
    }
}
