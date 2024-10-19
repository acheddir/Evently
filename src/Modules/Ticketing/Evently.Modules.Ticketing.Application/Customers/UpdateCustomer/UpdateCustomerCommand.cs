namespace Evently.Modules.Ticketing.Application.Customers.UpdateCustomer;

public record UpdateCustomerCommand(Guid CustomerId, string FirstName, string LastName) : ICommand;
