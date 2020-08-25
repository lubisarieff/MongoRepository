using MongoRepository.Core.Entities;
using System.Collections.Generic;

namespace MongoRepository.Repository
{
    public interface IMongoRepository<T> where T : IEntity
    {
        /*
            CREATE DATA FROM COLLECTION
        */

        //Store Data To Collection
        T Create(T entities);


        /*
            READ DATA FROM COLLECTION
        */

        //Get All Data
        IEnumerable<T> GetAll();
        //Get Data By Id
        T FindById(string id);

    }
}
