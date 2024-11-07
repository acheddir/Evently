namespace Evently.Modules.Ticketing.Application.Tickets.CreateTicketBatch;

internal sealed class CreateTicketBatchCommandHandler(
    ITicketingUnitOfWork ticketingUnitOfWork)
    : ICommandHandler<CreateTicketBatchCommand>
{
    public async Task<Result> Handle(CreateTicketBatchCommand request, CancellationToken cancellationToken)
    {
        Order? order = await ticketingUnitOfWork.Orders.GetAsync(request.OrderId, cancellationToken);

        if (order is null)
        {
            return Result.Failure(OrderErrors.NotFound(request.OrderId));
        }

        Result result = order.IssueTickets();

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        List<Ticket> tickets = [];

        foreach (OrderItem orderItem in order.OrderItems)
        {
            TicketType? ticketType = await ticketingUnitOfWork.TicketTypes.GetAsync(orderItem.TicketTypeId, cancellationToken);

            if (ticketType is null)
            {
                return Result.Failure(TicketTypeErrors.NotFound(orderItem.TicketTypeId));
            }

            for (int i = 0; i < orderItem.Quantity; i++)
            {
                var ticket = Ticket.Create(order, ticketType);

                tickets.Add(ticket);
            }
        }

        ticketingUnitOfWork.Tickets.InsertRange(tickets);

        await ticketingUnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
