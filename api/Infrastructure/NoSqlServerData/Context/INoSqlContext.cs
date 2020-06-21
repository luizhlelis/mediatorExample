using System;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Infrastructure.NoSqlServerData.Context
{
    public interface INoSqlContext : IDisposable
    {
        void AddCommand(Func<Task> func);
        Task<int> SaveChanges();
        IMongoCollection<T> GetCollection<T>(string name);
    }
}
