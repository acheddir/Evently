namespace Evently.Modules.Ticketing.Domain.Orders;

public enum OrderStatus
{
    Pending = 0,
    Paid,
    Refunded,
    Cancelled
}
