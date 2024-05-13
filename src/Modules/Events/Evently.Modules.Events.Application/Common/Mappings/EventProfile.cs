namespace Evently.Modules.Events.Application.Common.Mappings;

public class EventProfile : Profile
{
    public EventProfile()
    {
        CreateMap<Event, EventResponse>();
    }
}
