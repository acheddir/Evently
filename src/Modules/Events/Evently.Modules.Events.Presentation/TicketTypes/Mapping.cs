namespace Evently.Modules.Events.Presentation.TicketTypes;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<CreateTicketType.Request, CreateTicketTypeCommand>();
        CreateMap<ChangeTicketTypePrice.Request, UpdateTicketTypePriceCommand>();
    }
}
