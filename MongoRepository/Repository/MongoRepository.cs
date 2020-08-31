using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoRepository.Config;
using MongoRepository.Core.Attributes;
using MongoRepository.Core.Entities;
using MongoRepository.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MongoRepository.Repository
{
    public class MongoRepository<T, TKey> : IMongoRepository<T, TKey>
        where T : IEntity<TKey>
    {
        protected internal readonly IMongoCollection<T> _collection;

        /// <summary>
        /// Intialize a new instance of the MongoRepository class.
        /// </summary>
        /// <param name="options">options.Value.Conectionstring to use for connection to MongoDB</param>
        public MongoRepository(IMongoDbSettings options)
        {
            _collection = MongoDbUtil<TKey>.GetCollectionFromConnectionString<T>(options);
        }


        /// <summary>
        /// Initializes a new instance of the MongoRepository class.
        /// </summary>
        /// <param name="url">Url to use for connecting to MongoDB.</param>
        public MongoRepository(MongoUrl url)
        {
            _collection = MongoDbUtil<TKey>.GetCollectionFromUrl<T>(url);
        }
       
        /// <summary>
        /// Initializes a new instance of the MongoRepository class.
        /// </summary>
        // <param name="options">options.Value.Conectionstring to use for connection to MongoDB</param>
        /// <param name="collectionName">The name of the collection to use.</param>
        public MongoRepository(IMongoDbSettings options, string collectionName)
        {
            _collection = MongoDbUtil<TKey>.GetCollectionFromConnectionString<T>(options, collectionName);
        }

        /// <summary>
        /// Initializes a new instance of the MongoRepository class.
        /// </summary>
        /// <param name="url">Url to use for connecting to MongoDB.</param>
        /// <param name="collectionName">The name of the collection to use.</param>
        public MongoRepository(MongoUrl url, string collectionName)
        {
           _collection = MongoDbUtil<TKey>.GetCollectionFromUrl<T>(url, collectionName);
        }
        
        /// <summary>
        /// Gets the Mongo collection (to perform advanced operations).
        /// </summary>
        /// <remarks>
        /// One can argue that exposing this property (and with that, access to it's Database property for instance
        /// (which is a "parent")) is not the responsibility of this class. Use of this property is highly discouraged;
        /// for most purposes you can use the MongoRepositoryManager&lt;T&gt;
        /// </remarks>
        /// <value>The Mongo collection (to perform advanced operations).</value>
        public IMongoCollection<T> Collection 
        {
            get { return _collection; }
        }

        /// <summary>
        /// Returns the T by its given id.
        /// </summary>
        /// <param name="id">The Id of the entity to retrieve.</param>
        /// <returns>The Entity T.</returns>
        public virtual T GetById(TKey id)
        {
            var filter = Builders<T>.Filter.Eq(fil => fil.Id, id);
            return _collection.Find(filter).SingleOrDefault();
        }

        /// <summary>
        /// Return the T
        /// </summary>
        /// <returns>The IEnumerable<Entity> T</returns>
        public virtual IEnumerable<T> GetAll()
        {
            return _collection.Find(T => true).ToEnumerable();
        }

        /// <summary>
        /// Adds the new entity in the repository.
        /// </summary>
        /// <param name="entity">The entity T.</param>
        /// <returns>The added entity including its new ObjectId.</returns>
        public T Add(T entity)
        {
            _collection.InsertOne(entity);
            return entity;
        }

        /// <summary>
        /// Upserts an entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The updated entity.</returns>
        public T Update(T entity)
        {
            _collection.InsertOne(entity);
            return entity;
        }

        /// <summary>
        /// Deletes an entity from the repository by its id.
        /// </summary>
        /// <param name="id">The entity's id.</param>
        public void Delete(TKey id)
        {
            
        }
    }

    /// <summary>
    /// Deals with entities in MongoDb.
    /// </summary>
    /// <typeparam name="T">The type contained in the repository.</typeparam>
    /// <remarks>Entities are assumed to use strings for Id's.</remarks>
    public class MongoRepository<T> : MongoRepository<T, string>, IMongoRepository<T>
        where T : IEntity<string>
    {       
        /// <summary>
        /// Initializes a new instance of the MongoRepository class.
        /// </summary>
        /// <param name="url">Url to use for connecting to MongoDB.</param>
        /// <param name="collectionName">The name of the collection to use.</param>
        public MongoRepository(IMongoDbSettings options, string collectionName)
            : base(options, collectionName) { }

        /// <summary>
        /// Initializes a new instance of the MongoRepository class.
        /// </summary>
        /// <param name="connectionString">Connectionstring to use for connecting to MongoDB.</param>
        public MongoRepository(IMongoDbSettings options)
            : base(options) { }


        /// <summary>
        /// Initializes a new instance of the MongoRepository class.
        /// </summary>
        /// <param name="url">Url to use for connecting to MongoDB.</param>
        public MongoRepository(MongoUrl url)
         : base(url) { }

        /// <summary>
        /// Initializes a new instance of the MongoRepository class.
        /// </summary>
        /// <param name="url">Url to use for connecting to MongoDB.</param>
        /// <param name="collectionName">The name of the collection to use.</param>
        public MongoRepository(MongoUrl url, string collectionName)
            : base(url, collectionName) { }
    }
}
