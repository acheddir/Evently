namespace Evently.Common.Presentation.Endpoints;

public static class EndpointExtensions
{
    public static void AddEndpoints(this IServiceCollection services, params Assembly[] assemblies)
    {
        ServiceDescriptor[] serviceDescriptors = assemblies
            .SelectMany(a => a.GetTypes())
            .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                           type.IsAssignableTo(typeof(IEndpoint)))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
            .ToArray();

        services.TryAddEnumerable(serviceDescriptors);
    }
    
    public static void MapEndpoints(this WebApplication app)
    {
        app.MapEndpoints(null);
    }

    private static void MapEndpoints(this WebApplication app,
        RouteGroupBuilder? routeGroupBuilder)
    {
        IEnumerable<IEndpoint> endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();

        IEndpointRouteBuilder builder = routeGroupBuilder is null ? app : routeGroupBuilder;

        foreach (IEndpoint endpoint in endpoints)
        {
            endpoint.MapEndpoint(builder);
        }
    }
}
