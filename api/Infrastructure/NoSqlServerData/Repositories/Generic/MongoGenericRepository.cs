using System;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using MediatorExample.Domain.Data.NoSqlServerRepositoryContract.Generic;
using MediatorExample.Domain.Data.NoSqlServerRepositoryContract.Generic.Entities;
using Infrastructure.NoSqlServerData.Context;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Infrastructure.NoSqlServerData.Repositories.Generic
{
    public class MongoGenericRepository<TDocument> : IMongoGenericRepository<TDocument>
        where TDocument : class, IIdentifiableMongoDocument, new()
    {
        protected readonly INoSqlContext _context;
        protected IMongoCollection<TDocument> _collection;

        protected MongoGenericRepository(INoSqlContext context)
        {
            _context = context;
        }

        private void ConfigCollection()
        {
            _collection = _context.GetCollection<TDocument>(typeof(TDocument).Name);
        }

        public virtual void Add(TDocument obj)
        {
            ConfigCollection();
            _context.AddCommand(() => _collection.InsertOneAsync(obj));
        }

        public virtual async Task<TDocument> GetById(Guid id)
        {
            ConfigCollection();
            var data = await _collection.FindAsync(Builders<TDocument>.Filter.Eq("_id", id));
            return data.SingleOrDefault();
        }

        public virtual async Task<IEnumerable<TDocument>> GetAll()
        {
            ConfigCollection();
            var all = await _collection.FindAsync(Builders<TDocument>.Filter.Empty);
            return all.ToList();
        }

        public virtual void Update(TDocument obj)
        {
            ConfigCollection();
            _context.AddCommand(() => _collection.ReplaceOneAsync(Builders<TDocument>.Filter.Eq("_id", obj.Id), obj));
        }

        public virtual void Remove(Guid id)
        {
            ConfigCollection();
            _context.AddCommand(() => _collection.DeleteOneAsync(Builders<TDocument>.Filter.Eq("_id", id)));
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

    }
}
