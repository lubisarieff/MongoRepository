using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoRepository.Config;
using MongoRepository.Core.Attributes;
using MongoRepository.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MongoRepository.Repository
{
    public class MongoRepository<T> : IMongoRepository<T>
        where T : IEntity
    {
        private readonly IMongoCollection<T> _collection;

        public MongoRepository(IOptions<MongoDbSettings> settings)
        {
            try
            {
                var database = new MongoClient(settings.Value.ConnectionString).GetDatabase(settings.Value.DatabaseName);
                _collection = database.GetCollection<T>(GetCollectionName(typeof(T)));
            }
            catch (Exception ex)
            {
                throw new Exception("Can not access to MongoDb server.", ex);
            }
        }

        //Store Data To Collection
        private protected string GetCollectionName(Type documentType)
        {
            return ((CollectionName)documentType.GetCustomAttributes(
                 typeof(CollectionName),
                 true)
             .FirstOrDefault())?.CollectionName;
        }

        //Get Collection Name
        public virtual T Create(T entities)
        {
            try
            {
                _collection.InsertOne(entities);
                return entities;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Get All Data From Collection
        public virtual IEnumerable<T> GetAll()
        {
            try
            {
                return _collection.Find(T => true).ToEnumerable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Get Data By Id From Collection
        public virtual T FindById(string id)
        {
           try
            {
                var filter = Builders<T>.Filter.Eq(filt => filt.Id, id);
                return _collection.Find(filter).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
