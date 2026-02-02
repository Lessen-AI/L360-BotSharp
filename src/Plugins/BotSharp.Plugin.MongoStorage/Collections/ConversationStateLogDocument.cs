using BotSharp.Abstraction.MultiTenancy;

namespace BotSharp.Plugin.MongoStorage.Collections;

public class ConversationStateLogDocument : MongoBase, IMultiTenant
{
    public string ConversationId { get; set; } = default!;
    public string AgentId { get; set; } = default!;
    public string MessageId { get; set; } = default!;
    public Dictionary<string, string> States { get; set; } = [];
    public DateTime CreatedTime { get; set; }
    public Guid? TenantId { get; set; }
}
