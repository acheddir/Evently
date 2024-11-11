namespace Evently.Modules.Users.Domain.Users;

public sealed class Permission(string code)
{
    public static readonly Permission ReadUser = new("user:read");
    public static readonly Permission WriteUser = new("user:write");
    public static readonly Permission ReadEvent = new("event:read");
    public static readonly Permission WriteEvent = new("event:write");
    public static readonly Permission ReadTicketType = new("ticket-type:read");
    public static readonly Permission WriteTicketType = new("ticket-type:write");
    public static readonly Permission ReadCategory = new("category:read");
    public static readonly Permission WriteCategory = new("category:write");
    public static readonly Permission ReadCart = new("cart:read");
    public static readonly Permission UpdateCart = new("cart:update");
    public static readonly Permission ReadOrder = new("order:read");
    public static readonly Permission CreateOrder = new("order:create");
    public static readonly Permission ReadTicket = new("ticket:read");
    public static readonly Permission CheckInTicket = new("ticket:check-in");
    public static readonly Permission ReadEventStatistics = new("event-statistics:read");
    
    public string Code { get; } = code;
}
