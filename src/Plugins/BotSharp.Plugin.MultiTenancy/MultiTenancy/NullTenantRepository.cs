using BotSharp.Abstraction.MultiTenancy;
using BotSharp.Abstraction.MultiTenancy.Options;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BotSharp.Plugin.MultiTenancy.MultiTenancy;

public class NullTenantRepository : ITenantRepository
{
    public async Task<IReadOnlyList<TenantConfiguration>> GetTenantsAsync(CancellationToken cancellationToken = default)
    {
        return await Task.FromResult<IReadOnlyList<TenantConfiguration>>(Array.Empty<TenantConfiguration>());
    }
}