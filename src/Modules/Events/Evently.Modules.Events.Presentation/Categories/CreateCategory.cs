namespace Evently.Modules.Events.Presentation.Categories;

internal static class CreateCategory
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("categories", async (Request request, IMapper mapper, ISender sender) =>
            {
                CreateCategoryCommand command = mapper.Map<CreateCategoryCommand>(request);
                Result<Guid> result = await sender.Send(command);

                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .WithTags(Tags.Categories);
    }

    internal record struct Request(string Name);
}
