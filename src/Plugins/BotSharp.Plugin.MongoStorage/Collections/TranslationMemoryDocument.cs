using BotSharp.Abstraction.MultiTenancy;

namespace BotSharp.Plugin.MongoStorage.Collections;

public class TranslationMemoryDocument : MongoBase, IMultiTenant
{
    public string OriginalText { get; set; } = default!;
    public string HashText { get; set; } = default!;
    public List<TranslationMemoryMongoElement> Translations { get; set; } = [];
    public Guid? TenantId { get; set; }
}
