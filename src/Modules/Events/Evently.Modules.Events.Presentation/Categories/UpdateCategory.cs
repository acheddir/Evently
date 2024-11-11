namespace Evently.Modules.Events.Presentation.Categories;

internal sealed class UpdateCategory : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPut("categories/{id:guid}", async (Guid id, Request request, IMapper mapper, ISender sender) =>
            {
                UpdateCategoryCommand command = mapper.Map<UpdateCategoryCommand>(request);
                command.CategoryId = id;

                Result result = await sender.Send(command);

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            })
            .RequireAuthorization()
            .WithTags(Tags.Categories);
    }

    internal record struct Request(string Name);
}
