namespace Evently.Modules.Events.Application.Events.CancelEvent;

public class CancelEventCommandValidator : AbstractValidator<CancelEventCommand>
{
    public CancelEventCommandValidator()
    {
        RuleFor(c => c.EventId).NotEmpty();
    }
}
