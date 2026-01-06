using BotSharp.Abstraction.MultiTenancy;
using BotSharp.Abstraction.MultiTenancy.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace BotSharp.Plugin.MultiTenancy.MultiTenancy;

public class DbTenantStore : ITenantStore
{
    private const string CacheKey = "tenant_store:tenants";

    private readonly ITenantRepository _repo;
    private readonly IMemoryCache _cache;
    private readonly ILogger<DbTenantStore> _logger;

    public DbTenantStore(ITenantRepository repo, IMemoryCache cache, ILogger<DbTenantStore> logger)
    {
        _repo = repo;
        _cache = cache;
        _logger = logger;
    }

    public List<TenantConfiguration> GetTenants()
    {
        if (_cache.TryGetValue(CacheKey, out List<TenantConfiguration> cached))
        {
            return cached;
        }

        try
        {
            var tenants = _repo.GetTenants();
            _cache.Set(CacheKey, tenants, TimeSpan.FromMinutes(5));
            return tenants;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "DbTenantStore: failed to load tenant configurations from database.");
            return new List<TenantConfiguration>();
        }
    }
}