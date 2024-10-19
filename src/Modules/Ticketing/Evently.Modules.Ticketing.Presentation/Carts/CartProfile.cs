namespace Evently.Modules.Ticketing.Presentation.Carts;

public class CartProfile : Profile
{
    public CartProfile()
    {
        CreateMap<AddToCart.Request, AddItemToCartCommand>();
    }
}
