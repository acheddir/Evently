namespace Evently.Modules.Ticketing.Application.Orders.GetOrder;

public sealed record OrderResponse(
    Guid Id,
    Guid CustomerId,
    OrderStatus Status,
    decimal TotalPrice,
    DateTime CreatedAtUtc)
{
    public List<OrderItemResponse> OrderItems { get; } = [];
}
