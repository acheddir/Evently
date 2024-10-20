namespace Evently.Modules.Events.Application.TicketTypes.UpdateTicketTypePrice;

internal sealed class UpdateTicketTypePriceCommandHandler(
    IEventsUnitOfWork eventsUnitOfWork)
    : ICommandHandler<UpdateTicketTypePriceCommand>
{
    public async Task<Result> Handle(UpdateTicketTypePriceCommand request, CancellationToken cancellationToken)
    {
        TicketType? ticketType = await eventsUnitOfWork.TicketTypes.GetAsync(request.TicketTypeId, cancellationToken);

        if (ticketType is null)
        {
            return Result.Failure(TicketTypeErrors.NotFound(request.TicketTypeId));
        }

        ticketType.UpdatePrice(request.Price);

        await eventsUnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
