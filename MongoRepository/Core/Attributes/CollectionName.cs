using System;

namespace MongoRepository.Core.Attributes
{
    /// <summary>
    /// Attribute used to annotate Enities with to override mongo collection name. By default, when this attribute
    /// is not specified, the classname will be used.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class CollectionName : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the CollectionName class attribute with the desired name.
        /// </summary>
        /// <param name="collectionName">Name of the collection.</param>
        public CollectionName(string value)
        {
           
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Empty collection name is not allowed ", nameof(value));
            this.Name = value;  
        }

        // <summary>
        /// Gets the name of the collection.
        /// </summary>
        /// <collectionName>The name of the collection.</collectionName>
        public virtual string Name { get; private set; }
    }
}
