namespace Evently.Modules.Events.Presentation.Common.Mappings;

public class EventRequestProfile : Profile
{
    public EventRequestProfile()
    {
        CreateMap<CreateEvent.Request, CreateEventCommand>();
    }
}
