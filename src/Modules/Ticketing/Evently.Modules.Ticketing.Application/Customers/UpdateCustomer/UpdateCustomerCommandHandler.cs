namespace Evently.Modules.Ticketing.Application.Customers.UpdateCustomer;

internal sealed class UpdateCustomerCommandHandler(
    ITicketingUnitOfWork ticketingUnitOfWork) : ICommandHandler<UpdateCustomerCommand>
{
    public async Task<Result> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        Customer? customer = await ticketingUnitOfWork.Customers.GetAsync(request.CustomerId, cancellationToken);

        if (customer is null)
        {
            return Result.Failure(CustomerErrors.NotFound(request.CustomerId));
        }

        customer.Update(request.FirstName, request.LastName);

        await ticketingUnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
