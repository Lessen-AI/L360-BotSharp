using BotSharp.Abstraction.MultiTenancy;

namespace BotSharp.Plugin.MongoStorage.Collections;

public class UserAgentDocument : MongoBase, IMultiTenant
{
    public string UserId { get; set; } = default!;
    public string AgentId { get; set; } = default!;
    public IEnumerable<string> Actions { get; set; } = [];
    public DateTime CreatedTime { get; set; }
    public DateTime UpdatedTime { get; set; }
    public string? TenantId { get; set; }
}
