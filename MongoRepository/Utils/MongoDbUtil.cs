using MongoDB.Driver;
using MongoRepository.Config;
using MongoRepository.Core.Attributes;
using MongoRepository.Core.Entities;
using System;

namespace MongoRepository.Utils
{
    /// <summary>
    /// The default key MongoRepository for appsettings.json
    /// </summary>
    /// <typeparam name="U"></typeparam>
    internal static class MongoDbUtil<U>
    {
        /// <summary>
        /// Creates and returns a MongoDatabase from specified mongodbsettings
        /// </summary>
        /// <param name="options">The options to use get connectionstring and get database from.</param>
        /// <returns>Returns a MongoDatabase from the specified mongodbsettings</returns>
        /// <summary>
        private static MongoDatabaseBase GetDatabaseFromMongoDbSettings(IMongoDbSettings options)
        {
            return (MongoDatabaseBase)new MongoClient(options.ConnectionString).GetDatabase(options.DatabaseName);
        }

        /// <summary>
        /// Creates and returns a MongoDatabase from the specified url.
        /// </summary>
        /// <param name="url">The url to use to get the database from.</param>
        /// <returns>Returns a MongoDatabase from the specified url.</returns>
        private static MongoDatabaseBase GetDatabaseFromUrl(MongoUrl url)
        {
            return (MongoDatabaseBase)new MongoClient(url).GetDatabase(url.DatabaseName);
        }

        /// <summary>
        /// Creates and return a MongoCollection from the specified type and connectionstring
        /// </summary>
        /// <typeparam name="T">The type to get the collection</typeparam>
        /// <param name="connectionString">The connectionstring to use to get the collection from.</param>
        /// <returns>Returns a MongoCollection from the specified type and connectionstring.</returns>
        public static IMongoCollection<T> GetCollectionFromConnectionString<T>(IMongoDbSettings options)
            where T : IEntity<U>
        {
            return GetCollectionFromConnectionString<T>(options, GetCollectionName<T>());
        }

        /// <summary>
        /// Creates and returns a MongoCollection from specified type and connectionString
        /// </summary>
        /// <typeparam name="T">The type to get the collection of.</typeparam>
        /// <param name="options">The options to use get connectionstring and get database from.</param>
        /// <param name="collectionName">The name of the collection use.</param>
        /// <returns>Returns a MongoCollection form the specified type and connectionstring</returns>
        public static IMongoCollection<T> GetCollectionFromConnectionString<T>(IMongoDbSettings options, string collectionName)
            where T : IEntity<U>
        {
            return GetDatabaseFromMongoDbSettings(options)
                .GetCollection<T>(collectionName);
        }

        /// <summary>
        /// Creates and returns a MongoCollection from the specified type and url.
        /// </summary>
        /// <typeparam name="T">The type to get the collection of.</typeparam>
        /// <param name="url">The url to use to get the collection from.</param>
        /// <returns>Returns a MongoCollection from the specified type and url.</returns>
        public static IMongoCollection<T> GetCollectionFromUrl<T>(MongoUrl url)
            where T : IEntity<U>
        {
            return GetCollectionFromUrl<T>(url, GetCollectionName<T>());
        }

        /// <summary>
        /// Creates and returns a MongoCollection from the specified type and url.
        /// </summary>
        /// <typeparam name="T">The type to get the collection of.</typeparam>
        /// <param name="url">The url to use to get the collection from.</param>
        /// <param name="collectionName">The name of the collection to use.</param>
        /// <returns>Returns a MongoCollection from the specified type and url.</returns>
        public static IMongoCollection<T> GetCollectionFromUrl<T>(MongoUrl url, string collectionName)
            where T : IEntity<U>
        {
            return GetDatabaseFromUrl(url)
                .GetCollection<T>(collectionName);
        }

        /// <summary>
        /// Determines the CollectionName for T and assures it is not empty
        /// </summary>
        /// <typeparam name="T">The type to determinae the CollectionName for</typeparam>
        /// <returns>Returns the CollectionName For T</returns>
        private static string GetCollectionName<T>() where T : IEntity<U>
        {
            string collectionName;
            if (typeof(T).BaseType.Equals(typeof(object)))
            {
                collectionName = GetCollectionNameFromInterface<T>();
            }
            else
            {
                collectionName = GetCollectionNameFromType(typeof(T));
            }

            if (string.IsNullOrEmpty(collectionName))
            {
                throw new ArgumentException("Collection name cannot be empty for this entity");
            }

            return collectionName;
        }

        /// <summary>
        /// Determines the collectionname from the specified type
        /// </summary>
        /// <typeparam name="T">The type to get the collectionname from.</typeparam>
        /// <returns>Returns the collectionname from the specified type.</returns>
        private static string GetCollectionNameFromInterface<T>()
        {
            string collectionName;
            
            // Check to se if the object (inherited from Entity) has a CollectionName attribute
            var att = Attribute.GetCustomAttribute(typeof(T), typeof(CollectionName));
            if (att != null)
            {
                collectionName = ((CollectionName)att).Name;
            }
            else
            {
                collectionName = typeof(T).Name;
            }
            return collectionName;
        }

        /// <summary>
        /// Determines the collectionname from the specified type
        /// </summary>
        /// <param name="entityType">The type of the entity to get the collectionname from.</param>
        /// <returns>Return the collectionname from the specified type.</returns>
        private static string GetCollectionNameFromType(Type entityType)
        {
            string collectionName;

            //Check to see if the object (Inherited from Entity) has a CollectionName attribute
            var att = Attribute.GetCustomAttribute(entityType, typeof(CollectionName));
            if (att != null)
            {
                //It does! Return the value the specified by the CollectionName attribute
                collectionName = ((CollectionName)att).Name;
            }
            else
            {
                if (typeof(Entity).IsAssignableFrom(entityType))
                {
                    // No attribute found, get the basetype
                    while (!entityType.BaseType.Equals(typeof(Entity)))
                    {
                        entityType = entityType.BaseType;
                    }
                }
                collectionName = entityType.Name;
            }
            return collectionName;
        }
    }
}
