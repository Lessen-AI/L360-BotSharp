using BotSharp.Abstraction.MultiTenancy;

namespace BotSharp.Plugin.MongoStorage.Collections;

public class ConversationDialogDocument : MongoBase, IMultiTenant
{
    public string ConversationId { get; set; } = default!;
    public string AgentId { get; set; } = default!;
    public string UserId { get; set; } = default!;
    public DateTime UpdatedTime { get; set; }
    public List<DialogMongoElement> Dialogs { get; set; } = [];

    public Guid? TenantId { get; set; }
}
