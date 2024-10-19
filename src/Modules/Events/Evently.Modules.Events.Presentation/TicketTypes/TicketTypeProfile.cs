namespace Evently.Modules.Events.Presentation.TicketTypes;

public class TicketTypeProfile : Profile
{
    public TicketTypeProfile()
    {
        CreateMap<CreateTicketType.Request, CreateTicketTypeCommand>();
        CreateMap<ChangeTicketTypePrice.Request, UpdateTicketTypePriceCommand>();
    }
}
