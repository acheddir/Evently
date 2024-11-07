namespace Evently.Modules.Ticketing.Application.TicketTypes.UpdateTicketTypePrice;

internal sealed class UpdateTicketTypePriceCommandHandler(
    ITicketingUnitOfWork ticketingUnitOfWork)
    : ICommandHandler<UpdateTicketTypePriceCommand>
{
    public async Task<Result> Handle(UpdateTicketTypePriceCommand request, CancellationToken cancellationToken)
    {
        TicketType? ticketType = await ticketingUnitOfWork.TicketTypes.GetAsync(request.TicketTypeId, cancellationToken);

        if (ticketType is null)
        {
            return Result.Failure(TicketTypeErrors.NotFound(request.TicketTypeId));
        }

        ticketType.UpdatePrice(request.Price);

        await ticketingUnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
