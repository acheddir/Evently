using Evently.Modules.Events.Application.Categories.GetCategory;

namespace Evently.Modules.Events.Presentation.Categories;

internal sealed class GetCategory : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("categories/{id:guid}", async (Guid id, ISender sender) =>
            {
                Result<CategoryResponse> result = await sender.Send(new GetCategoryQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .RequireAuthorization()
            .WithTags(Tags.Categories);
    }
}
