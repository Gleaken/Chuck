using MongoDB.Bson.Serialization.Attributes;

namespace Chuck.Infrastructure.Models;

public class QuoteDao
{
    [BsonId]
    public string Id { get; set; }
    public string Quote { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}