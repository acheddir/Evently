namespace Evently.Modules.Ticketing.Domain.Carts;

public sealed class Cart
{
    public Guid CustomerId { get; init; }
    public List<CartItem> Items { get; init; } = [];
    public static Cart CreateDefault(Guid customerId) => new() { CustomerId = customerId };
}
