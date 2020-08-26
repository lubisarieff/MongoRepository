using System;

namespace MongoRepository.Core.Attributes
{
     /*
      * Attribute used to annotate Enities with to override mongo collection name. By default, when this attribute
      * is not specified, the classname will be used.
     */
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class BsonCollectionAttribute : Attribute
    {
        // Gets the name of the collection.
        public string CollectionName { get; }

        //Initializes a new instance of the CollectionName class attribute with the desired name.
        public BsonCollectionAttribute(string collectionName)
        {
           
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentException("Empty collection name is not allowed ", nameof(collectionName));
            CollectionName = collectionName;
        }
    }
}
