namespace Evently.Modules.Events.Presentation.Events;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<CreateEvent.Request, CreateEventCommand>();
        CreateMap<RescheduleEvent.Request, RescheduleEventCommand>();
    }
}
