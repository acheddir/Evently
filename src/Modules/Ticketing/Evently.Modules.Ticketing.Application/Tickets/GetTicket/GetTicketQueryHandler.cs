namespace Evently.Modules.Ticketing.Application.Tickets.GetTicket;

public class GetTicketQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetTicketQuery, TicketResponse>
{
    public async Task<Result<TicketResponse>> Handle(GetTicketQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                id AS {nameof(TicketResponse.Id)},
                customer_id AS {nameof(TicketResponse.CustomerId)},
                order_id AS {nameof(TicketResponse.OrderId)},
                event_id AS {nameof(TicketResponse.EventId)},
                ticket_type_id AS {nameof(TicketResponse.TicketTypeId)},
                code AS {nameof(TicketResponse.Code)},
                created_at_utc AS {nameof(TicketResponse.CreatedAtUtc)},
             FROM ticketing.tickets
             WHERE id = @TicketId
             """;
        
        TicketResponse? ticket = await connection.QueryFirstOrDefaultAsync<TicketResponse>(sql, request);
        
        return ticket ?? Result.Failure<TicketResponse>(TicketErrors.NotFound(request.TicketId));
    }
}
