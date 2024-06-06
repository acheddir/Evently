namespace Evently.Modules.Events.Application.Events.GetEvents;

internal sealed class GetEventsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetEventsQuery, EventsResponse>
{
    public async Task<Result<EventsResponse>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        var parameters = new GetEventsParameters(
            (int)EventStatus.Published,
            request.CategoryId,
            request.StartDate?.Date,
            request.EndDate?.Date,
            request.PageSize,
            (request.Page - 1) * request.PageSize);

        IReadOnlyCollection<EventResponse> events = await GetEventsAsync(connection, parameters);
        int totalCount = await CountEventsAsync(connection, parameters);

        return new EventsResponse(request.Page, request.PageSize, totalCount, events);
    }

    private static async Task<int> CountEventsAsync(DbConnection connection, GetEventsParameters parameters)
    {
        const string sql =
            """
            SELECT COUNT(*)
            FROM events.events
            WHERE
               (@Status IS NULL OR status = @Status)
               AND (@CategoryId IS NULL OR category_id = @CategoryId)
               AND (@StartDate::timestamp IS NULL OR starts_at_utc >= @StartDate::timestamp)
               AND (@EndDate::timestamp IS NULL OR ends_at_utc >= @EndDate::timestamp)
            """;

        int totalCount = await connection.ExecuteScalarAsync<int>(sql, parameters);

        return totalCount; 
    }

    private static async Task<IReadOnlyCollection<EventResponse>> GetEventsAsync(
        DbConnection connection,
        GetEventsParameters parameters)
    {
        const string sql =
            $"""
             SELECT
                 id AS {nameof(EventResponse.Id)},
                 category_id AS {nameof(EventResponse.CategoryId)},
                 title AS {nameof(EventResponse.Title)},
                 description AS {nameof(EventResponse.Description)},
                 location AS {nameof(EventResponse.Location)},
                 starts_at_utc AS {nameof(EventResponse.StartsAtUtc)},
                 ends_at_utc AS {nameof(EventResponse.EndsAtUtc)}
             FROM
                 events.events
             WHERE
                 (@Status IS NULL OR status = @Status)
                 AND (@CategoryId IS NULL OR category_id = @CategoryId)
                 AND (@StartDate::timestamp IS NULL OR starts_at_utc >= @StartDate::timestamp)
                 AND (@EndDate::timestamp IS NULL OR ends_at_utc <= @EndDate::timestamp)
             ORDER BY starts_at_utc
             OFFSET @Skip
             LIMIT @Take
             """;

        var events = (await connection.QueryAsync<EventResponse>(sql, parameters)).ToList();

        return events;
    }

    private record struct GetEventsParameters(
        int? Status,
        Guid? CategoryId,
        DateTime? StartDate,
        DateTime? EndDate,
        int Take,
        int Skip);
}
