using System.Threading;

namespace BotSharp.Abstraction.MultiTenancy;

public interface IConnectionStringResolver
{
    Task<string?> GetConnectionStringAsync(string connectionStringName, CancellationToken cancellationToken = default);

    Task<string?> GetConnectionStringAsync<TContext>(CancellationToken cancellationToken = default);
}