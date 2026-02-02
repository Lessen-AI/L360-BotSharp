using BotSharp.Abstraction.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BotSharp.Plugin.MongoStorage.Repository;

public partial class MongoRepository : IBotSharpRepository
{
    private readonly MongoDbContext _dc;
    private readonly IServiceProvider _services;
    private readonly ILogger<MongoRepository> _logger;
    private readonly BotSharpOptions _botSharpOptions;
    private UpdateOptions _options;

    public MongoRepository(
        MongoDbContext dc,
        IServiceProvider services,
        ILogger<MongoRepository> logger,
        BotSharpOptions botSharpOptions)
    {
        _dc = dc;
        _services = services;
        _logger = logger;
        _botSharpOptions = botSharpOptions;
        _options = new UpdateOptions
        {
            IsUpsert = true,
        };
    }

    public IServiceProvider ServiceProvider => _services;

    // Retrieve current tenant id from header "__tenant" or claim "tenantid"
    private Guid? GetCurrentTenantId()
    {
        try
        {
            var accessor = _services.GetService(typeof(IHttpContextAccessor)) as IHttpContextAccessor;
            var context = accessor?.HttpContext;
            if (context != null)
            {
                // header has higher priority
                if (context.Request.Headers.TryGetValue("__tenant", out var headerVal))
                {
                    var header = headerVal.FirstOrDefault();
                    if (!string.IsNullOrWhiteSpace(header) && Guid.TryParse(header, out var hid))
                    {
                        return hid;
                    }
                }

                var claim = context.User?.FindFirst("tenantid")?.Value;
                if (!string.IsNullOrWhiteSpace(claim) && Guid.TryParse(claim, out var cid))
                {
                    return cid;
                }
            }
        }
        catch { }

        return null;
    }

    // Add tenant filter to an existing filter definition when tenant is present
    private FilterDefinition<T> WithTenant<T>(FilterDefinition<T> filter)
    {
        var tenantId = GetCurrentTenantId();
        if (!tenantId.HasValue) return filter;
        var builder = Builders<T>.Filter;
        var tenantFilter = builder.Eq("TenantId", tenantId.Value);
        return builder.And(filter, tenantFilter);
    }
}
