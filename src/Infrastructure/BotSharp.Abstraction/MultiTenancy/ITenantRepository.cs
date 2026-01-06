using BotSharp.Abstraction.MultiTenancy.Options;
using System.Threading;

namespace BotSharp.Abstraction.MultiTenancy;

public interface ITenantRepository
{
    Task<IReadOnlyList<TenantConfiguration>> GetTenantsAsync(CancellationToken cancellationToken = default);
}