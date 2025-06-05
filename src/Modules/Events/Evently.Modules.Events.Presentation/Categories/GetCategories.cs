using Evently.Modules.Events.Application.Categories.GetCategories;

namespace Evently.Modules.Events.Presentation.Categories;

internal sealed class GetCategories : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("categories", async (ISender sender, ICacheService cacheService) =>
            {
                IReadOnlyCollection<CategoryResponse> categoryResponse =
                    await cacheService.GetAsync<IReadOnlyCollection<CategoryResponse>>("categories");

                if (categoryResponse is not null)
                {
                    return Results.Ok(categoryResponse);
                }

                Result<IReadOnlyCollection<CategoryResponse>> result = await sender.Send(new GetCategoriesQuery());

                if (result.IsSuccess)
                {
                    await cacheService.SetAsync("categories", result.Value).ConfigureAwait(false);
                }

                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .RequireAuthorization()
            .WithTags(Tags.Categories);
    }
}
