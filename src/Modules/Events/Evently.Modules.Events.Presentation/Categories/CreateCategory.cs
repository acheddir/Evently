namespace Evently.Modules.Events.Presentation.Categories;

internal sealed class CreateCategory : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("categories", async (Request request, IMapper mapper, ISender sender) =>
            {
                CreateCategoryCommand command = mapper.Map<CreateCategoryCommand>(request);
                Result<Guid> result = await sender.Send(command);

                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .WithTags(Tags.Categories);
    }

    internal record struct Request(string Name);
}
