namespace Evently.Modules.Ticketing.Application.Carts.GetCart;

public class GetCartQueryHandler(ICartService cartService) : IQueryHandler<GetCartQuery, Cart>
{
    public async Task<Result<Cart>> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        return await cartService.GetAsync(request.CustomerId, cancellationToken);
    }
}
