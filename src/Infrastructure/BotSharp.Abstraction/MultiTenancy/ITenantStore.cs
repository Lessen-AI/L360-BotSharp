using BotSharp.Abstraction.MultiTenancy.Options;
using System.Threading;
using System.Threading.Tasks;

namespace BotSharp.Abstraction.MultiTenancy;

public interface ITenantStore
{
    Task<IReadOnlyList<TenantConfiguration>> GetTenantsAsync(CancellationToken cancellationToken = default);
}
