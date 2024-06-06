namespace Evently.Modules.Events.Presentation.Common.Mappings;

public class EventsProfile : Profile
{
    public EventsProfile()
    {
        CreateMap<CreateEvent.Request, CreateEventCommand>();
        CreateMap<RescheduleEvent.Request, RescheduleEventCommand>();

        CreateMap<CreateTicketType.Request, CreateTicketTypeCommand>();
        CreateMap<ChangeTicketTypePrice.Request, UpdateTicketTypePriceCommand>();

        CreateMap<CreateCategory.Request, CreateCategoryCommand>();
        CreateMap<UpdateCategory.Request, UpdateCategoryCommand>();
    }
}
