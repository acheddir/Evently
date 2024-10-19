namespace Evently.Modules.Ticketing.Application.Carts.ClearCart;

public class ClearCartCommandHandler(ICustomerRepository customerRepository, CartService cartService) : ICommandHandler<ClearCartCommand>
{
    public async Task<Result> Handle(ClearCartCommand request, CancellationToken cancellationToken)
    {
        bool exists = await customerRepository.ExistAsync(request.CustomerId, cancellationToken);

        if (!exists)
        {
            return Result.Failure(CustomerErrors.NotFound(request.CustomerId));
        }

        await cartService.ClearAsync(request.CustomerId, cancellationToken);

        return Result.Success();
    }
}
