using System.Threading;

namespace BotSharp.Abstraction.MultiTenancy;

public interface ITenantConnectionProvider
{
    Task<string> GetConnectionStringAsync(string name, CancellationToken cancellationToken = default);
    Task<string> GetDefaultConnectionStringAsync(CancellationToken cancellationToken = default);
}