namespace Evently.Modules.Ticketing.Domain.Carts;

public static class CartErrors
{
    public static readonly Error Empty = Error.Problem("Carts.Empty", "The cart is empty");
}

