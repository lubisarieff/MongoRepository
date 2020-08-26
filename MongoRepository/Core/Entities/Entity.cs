using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoRepository.Core.Entities
{
    /// <summary>
    /// Abstract Entity for all the BusinessEntities.
    /// </summary>
    public abstract class Entity : IEntity
    {
        /// <summary>
        /// Gets or sets the id for this object (the primary record for an entity).
        /// </summary>
        /// <value>The id for this object (the primary record for an entity).</value
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public virtual string Id { get; set; }
    }
}
