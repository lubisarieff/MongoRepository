using MongoDB.Bson.Serialization.Attributes;

namespace MongoRepository.Core.Entities
{
    public interface IEntity
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        string Id { get; set; }
    }
}
