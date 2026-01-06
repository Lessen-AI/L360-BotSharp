using BotSharp.Abstraction.MultiTenancy;
using Microsoft.Extensions.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace BotSharp.Plugin.MultiTenancy.MultiTenancy;

public class TenantConnectionProvider : ITenantConnectionProvider
{
    private readonly IConnectionStringResolver _resolver;
    private readonly IConfiguration _configuration;

    public TenantConnectionProvider(IConnectionStringResolver resolver, IConfiguration configuration)
    {
        _resolver = resolver;
        _configuration = configuration;
    }

    public async Task<string> GetConnectionStringAsync(string name, CancellationToken cancellationToken = default)
    {
        // Prefer app-level connection strings
        var fallback = _configuration.GetConnectionString(name);
        if (!string.IsNullOrWhiteSpace(fallback)) return fallback;

        var cs = await _resolver.GetConnectionStringAsync(name, cancellationToken);
        return cs ?? string.Empty;
    }

    public async Task<string> GetDefaultConnectionStringAsync(CancellationToken cancellationToken = default)
    {
        return await GetConnectionStringAsync("Default", cancellationToken);
    }
}