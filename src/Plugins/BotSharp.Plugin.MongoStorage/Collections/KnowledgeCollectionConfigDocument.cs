using BotSharp.Abstraction.MultiTenancy;

namespace BotSharp.Plugin.MongoStorage.Collections;

public class KnowledgeCollectionConfigDocument : MongoBase, IMultiTenant
{
    public string Name { get; set; } = default!;
    public string Type { get; set; } = default!;
    public KnowledgeVectorStoreConfigMongoModel VectorStore { get; set; } = new();
    public KnowledgeEmbeddingConfigMongoModel TextEmbedding { get; set; } = new();
    public Guid? TenantId { get; set; }
}
