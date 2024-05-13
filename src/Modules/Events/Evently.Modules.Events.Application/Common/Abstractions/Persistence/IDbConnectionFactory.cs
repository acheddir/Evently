namespace Evently.Modules.Events.Application.Common.Abstractions.Persistence;

public interface IDbConnectionFactory
{
    ValueTask<DbConnection> OpenConnectionAsync();
}
