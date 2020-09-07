namespace MongoRepository.Config
{
    public interface IMongoDbSettings
    {
        /// <summary>
        /// Get and Set DatabaseName MongoDb
        /// ex : set DatabaseName MongoDb => book
        /// </summary>
        string DatabaseName { get; set; }

        /// <summary>
        /// Get and Set Connection For MongoDb
        /// ex : Set Connection Url MongoDb => localhost::27017
        /// </summary>
        string ConnectionString { get; set; }
    }
}
