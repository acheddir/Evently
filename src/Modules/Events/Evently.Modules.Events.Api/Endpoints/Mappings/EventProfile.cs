namespace Evently.Modules.Events.Api.Endpoints.Mappings;

public class EventProfile : Profile
{
    public EventProfile()
    {
        CreateMap<CreateEvent.Request, Event>();
    }
}
