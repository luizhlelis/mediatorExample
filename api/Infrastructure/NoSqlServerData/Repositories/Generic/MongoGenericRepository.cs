using System;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using MediatorExample.Domain.Data.NoSqlServerRepositoryContract.Generic;
using MediatorExample.Domain.Data.NoSqlServerRepositoryContract.Generic.Entities;

namespace Infrastructure.NoSqlServerData.Repositories.Generic
{
    public class MongoGenericRepository<TDocument> : IMongoGenericRepository<TDocument>
        where TDocument : class, IIdentifiableMongoDocument, new()
    {
        private readonly IMongoCollection<TDocument> _books;

        public MongoGenericRepository()
        {
            MongoClient client = new MongoClient(settings.ConnectionString);
            IMongoDatabase database = client.GetDatabase(settings.DatabaseName);

            _books = database.GetCollection<TEntity>(settings.BooksCollectionName);
        }
    }
}
