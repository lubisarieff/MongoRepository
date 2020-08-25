namespace MongoRepository.Core.Entities
{
    public abstract class Entity : IEntity
    {
        public string Id { get; set; }
    }
}
