using BotSharp.Abstraction.MultiTenancy;

namespace BotSharp.Plugin.MongoStorage.Collections;

public class ConversationStateDocument : MongoBase, IMultiTenant
{
    public string ConversationId { get; set; } = default!;
    public string AgentId { get; set; } = default!;
    public string UserId { get; set; } = default!;
    public DateTime UpdatedTime { get; set; }
    public List<StateMongoElement> States { get; set; } = [];
    public List<BreakpointMongoElement> Breakpoints { get; set; } = [];
    public string? TenantId { get; set; }
}
