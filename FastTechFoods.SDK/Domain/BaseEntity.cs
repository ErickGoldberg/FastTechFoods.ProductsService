using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FastTechFoods.SDK.Domain
{
    public class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; } = Guid.NewGuid();
        public DateTime CreatedAt { get; } = DateTime.Now;
        public DateTime UpdatedAt { get; protected set; } = DateTime.Now;
    }
}