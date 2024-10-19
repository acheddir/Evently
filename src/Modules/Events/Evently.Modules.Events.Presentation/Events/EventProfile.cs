namespace Evently.Modules.Events.Presentation.Events;

public class EventProfile : Profile
{
    public EventProfile()
    {
        CreateMap<CreateEvent.Request, CreateEventCommand>();
        CreateMap<RescheduleEvent.Request, RescheduleEventCommand>();
    }
}
