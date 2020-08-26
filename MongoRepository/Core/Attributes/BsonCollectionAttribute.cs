using System;

namespace MongoRepository.Core.Attributes
{
    /// <summary>
    /// Attribute used to annotate Enities with to override mongo collection name. By default, when this attribute
    /// is not specified, the classname will be used.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class BsonCollectionAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the CollectionName class attribute with the desired name.
        /// </summary>
        /// <param name="collectionName">Name of the collection.</param>
        public BsonCollectionAttribute(string collectionName)
        {
           
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentException("Empty collection name is not allowed ", nameof(collectionName));
            CollectionName = collectionName;
        }

        // <summary>
        /// Gets the name of the collection.
        /// </summary>
        /// <collectionName>The name of the collection.</collectionName>
        public virtual string CollectionName { get; private set; }
    }
}
