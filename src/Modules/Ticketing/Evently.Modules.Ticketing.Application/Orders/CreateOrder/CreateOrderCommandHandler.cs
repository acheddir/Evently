namespace Evently.Modules.Ticketing.Application.Orders.CreateOrder;

internal sealed class CreateOrderCommandHandler(
    IPaymentService paymentService,
    ICartService cartService,
    ITicketingUnitOfWork ticketingUnitOfWork) : ICommandHandler<CreateOrderCommand>
{
    public async Task<Result> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        await ticketingUnitOfWork.CreateTransactionAsync(cancellationToken);

        Customer? customer = await ticketingUnitOfWork.Customers.GetAsync(request.CustomerId, cancellationToken);

        if (customer is null)
        {
            return Result.Failure(CustomerErrors.NotFound(request.CustomerId));
        }

        var order = Order.Create(customer);

        Cart cart = await cartService.GetAsync(customer.Id, cancellationToken);

        if (!cart.Items.Any())
        {
            return Result.Failure(CartErrors.Empty);
        }

        foreach (CartItem cartItem in cart.Items)
        {
            // This acquires a pessimistic lock or throws an exception if already locked.
            TicketType? ticketType = await ticketingUnitOfWork.TicketTypes.GetWithLockAsync(
                cartItem.TicketTypeId,
                cancellationToken);

            if (ticketType is null)
            {
                return Result.Failure(TicketTypeErrors.NotFound(cartItem.TicketTypeId));
            }

            Result result = ticketType.UpdateQuantity(cartItem.Quantity);

            if (result.IsFailure)
            {
                return Result.Failure(result.Error);
            }

            order.AddItem(ticketType, cartItem.Quantity, cartItem.Price, ticketType.Currency);
        }

        ticketingUnitOfWork.Orders.Insert(order);

        // We're faking a payment gateway request here...
        PaymentResponse paymentResponse = await paymentService.ChargeAsync(order.TotalPrice, order.Currency);

        var payment = Payment.Create(
            order,
            paymentResponse.TransactionId,
            paymentResponse.Amount,
            paymentResponse.Currency);

        ticketingUnitOfWork.Payments.Insert(payment);

        await ticketingUnitOfWork.SaveChangesAsync(cancellationToken);

        await ticketingUnitOfWork.CommitAsync(cancellationToken);

        await cartService.ClearAsync(customer.Id, cancellationToken);

        return Result.Success();
    }
}
