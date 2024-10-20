namespace Evently.Common.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        Action<IRegistrationConfigurator>[] moduleConfigureConsumers,
        string dbConnectionString,
        string redisConnectionString,
        Assembly[] assemblies)
    {
        NpgsqlDataSource npgsqlDataSource = new NpgsqlDataSourceBuilder(dbConnectionString).Build();
        services.TryAddSingleton(npgsqlDataSource);

        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();

        services.TryAddSingleton<PublishDomainEventsInterceptor>();
        services.TryAddSingleton<IDateTimeProvider, DateTimeProvider>();

        try
        {
            IConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(redisConnectionString);
            services.TryAddSingleton(connectionMultiplexer);

            services.AddStackExchangeRedisCache(options =>
            {
                options.ConnectionMultiplexerFactory = () => Task.FromResult(connectionMultiplexer);
            });
        }
        catch
        {
            services.AddDistributedMemoryCache();
        }

        services.TryAddSingleton<ICacheService, CacheService>();
        services.TryAddSingleton<IEventBus, EventBus>();

        services.AddMassTransit(configure =>
        {
            // Configure consumers
            foreach (
                Action<IRegistrationConfigurator> configureConsumer in moduleConfigureConsumers
            )
            {
                configureConsumer(configure);
            }

            configure.SetKebabCaseEndpointNameFormatter();
            configure.UsingInMemory(
                (context, cfg) =>
                {
                    cfg.ConfigureEndpoints(context);
                }
            );
        });

        services.AddAutoMapper(assemblies);

        return services;
    }
}
