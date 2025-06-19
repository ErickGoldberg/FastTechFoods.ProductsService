using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FastTechFoods.SDK.Domain
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; } = DateTime.Now;
        public DateTime UpdatedAt { get; protected set; } = DateTime.Now;
    }
}