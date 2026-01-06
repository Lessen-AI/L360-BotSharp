using BotSharp.Abstraction.MultiTenancy;
using BotSharp.Abstraction.MultiTenancy.Options;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BotSharp.Plugin.MultiTenancy.MultiTenancy;

public class ConfigTenantStore : ITenantStore
{
    private readonly TenantStoreOptions _options;

    public ConfigTenantStore(IOptionsMonitor<TenantStoreOptions> options)
    {
        _options = options.CurrentValue;
    }

    public async Task<IReadOnlyList<TenantConfiguration>> GetTenantsAsync(CancellationToken cancellationToken = default)
    {
        return await Task.FromResult<IReadOnlyList<TenantConfiguration>>(_options.Tenants);
    }
}