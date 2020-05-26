using System;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using MediatorExample.Domain.Data.NoSqlServerRepositoryContract.Generic;
using MediatorExample.Domain.Data.NoSqlServerRepositoryContract.Generic.Entities;
using Infrastructure.NoSqlServerData.Context;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.NoSqlServerData.Repositories.Generic
{
    public class MongoGenericRepository<TDocument> : IMongoGenericRepository<TDocument>
        where TDocument : class, IIdentifiableMongoDocument, new()
    {
        private readonly IMongoCollection<TDocument> _collection;
        public Func<NoSqlContext> ContextCreator { get; private set; }

        public MongoGenericRepository(IConfiguration configuration)
        {
            //ContextCreator = () =>
            //{
            //};
        }
    }
}
