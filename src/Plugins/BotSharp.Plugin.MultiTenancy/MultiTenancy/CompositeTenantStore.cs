using BotSharp.Abstraction.MultiTenancy;
using BotSharp.Abstraction.MultiTenancy.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BotSharp.Plugin.MultiTenancy.MultiTenancy;

public class CompositeTenantStore : ITenantStore
{
    private readonly IOptionsMonitor<TenantStoreOptions> _options;
    private readonly IEnumerable<ITenantStore> _stores;

    public CompositeTenantStore(IOptionsMonitor<TenantStoreOptions> options, IEnumerable<ITenantStore> stores)
    {
        _options = options;
        _stores = stores;
    }

    public async Task<IReadOnlyList<TenantConfiguration>> GetTenantsAsync(CancellationToken cancellationToken = default)
    {
        // If configuration has tenants, prefer it.
        var configured = _options.CurrentValue.Tenants;
        if (configured is { Length: > 0 })
        {
            return configured;
        }

        // Otherwise, try other stores in order.
        foreach (var s in _stores)
        {
            if (s is ConfigTenantStore) continue;
            var tenants = await s.GetTenantsAsync(cancellationToken);
            if (tenants.Count > 0) return tenants;
        }

        return Array.Empty<TenantConfiguration>();
    }
}