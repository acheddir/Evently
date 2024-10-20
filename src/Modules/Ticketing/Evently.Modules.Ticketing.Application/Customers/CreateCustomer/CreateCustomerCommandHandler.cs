namespace Evently.Modules.Ticketing.Application.Customers.CreateCustomer;

internal sealed class CreateCustomerCommandHandler(
    ITicketingUnitOfWork ticketingUnitOfWork)
    : ICommandHandler<CreateCustomerCommand>
{
    public async Task<Result> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = Customer.Create(request.CustomerId, request.Email, request.FirstName, request.LastName);

        ticketingUnitOfWork.Customers.Insert(customer);

        await ticketingUnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
