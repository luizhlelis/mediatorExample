using System.Threading;
using MongoDB.Driver;

namespace Infrastructure.NoSqlServerData.Context
{
    public class NoSqlContext
    {
        public IMongoClient Client { get; }
        public IMongoDatabase Database { get; }

        public NoSqlContext(MongoClient client)
        {
            Client = client;
            //Database = GetMongoDatabase(config.Database);
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return Database.GetCollection<T>(collectionName);
        }

        private IMongoDatabase GetMongoDatabase(string databaseName)
        {
            try
            {
                return Client.GetDatabase(databaseName);
            }
            catch
            {
                Thread.Sleep(1000);
                return GetMongoDatabase(databaseName);
            }
        }
    }
}