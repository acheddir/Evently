namespace Evently.Modules.Events.Presentation.Categories;

internal static class UpdateCategory
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("categories/{id:guid}", async (Guid id, Request request, IMapper mapper, ISender sender) =>
            {
                UpdateCategoryCommand command = mapper.Map<UpdateCategoryCommand>(request);
                command.CategoryId = id;

                Result result = await sender.Send(command);

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            })
            .WithTags(Tags.Categories);
    }

    internal record struct Request(string Name);
}
