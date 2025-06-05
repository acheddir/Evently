namespace Evently.Modules.Ticketing.Application.Carts.AddItemToCart;

internal sealed class AddItemToCartCommandHandler(
    ICartService cartService,
    ITicketingUnitOfWork ticketingUnitOfWork,
    IEventsApi eventsApi) : ICommandHandler<AddItemToCartCommand>
{
    public async Task<Result> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
    {
        Customer? customer = await ticketingUnitOfWork.Customers.GetAsync(request.CustomerId, cancellationToken);

        if (customer is null)
        {
            return Result.Failure(CustomerErrors.NotFound(request.CustomerId));
        }

        TicketTypeResponse? ticketType = await eventsApi.GetTicketTypesAsync(request.TicketTypeId, cancellationToken);

        if (ticketType is null)
        {
            return Result.Failure(CustomerErrors.NotFound(request.CustomerId));
        }

        var cartItem = new CartItem
        {
            TicketTypeId = ticketType.Id,
            Price = ticketType.Price,
            Currency = ticketType.Currency,
            Quantity = request.Quantity
        };

        await cartService.AddItemAsync(customer.Id, cartItem, cancellationToken).ConfigureAwait(false);

        return Result.Success();
    }
}
