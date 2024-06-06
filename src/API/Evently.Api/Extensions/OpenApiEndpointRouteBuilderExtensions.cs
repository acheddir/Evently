namespace Evently.Api.Extensions;

internal static class OpenApiEndpointRouteBuilderExtensions
{
    internal static IEndpointConventionBuilder MapScalarApiReference(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapGet("api/v1", () =>
            {
                string title = $"Scalar API Reference -- v1";
                return Results.Content(
                    $$"""
                      <!doctype html>
                      <html>
                      <head>
                          <title>{{title}}</title>
                          <meta charset="utf-8" />
                          <meta name="viewport" content="width=device-width, initial-scale=1" />
                      </head>
                      <body>
                          <script id="api-reference" data-url="/swagger/v1/swagger.json"></script>
                          <script src="https://cdn.jsdelivr.net/npm/@scalar/api-reference"></script>
                      </body>
                      </html>
                      """, "text/html");
            })
            .ExcludeFromDescription();
    }
}
