using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoRepository.Core.Entities
{
    /// <summary>
    /// Generic Entity interface.
    /// </summary>
    public interface IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        string Id { get; set; }
    }
}
