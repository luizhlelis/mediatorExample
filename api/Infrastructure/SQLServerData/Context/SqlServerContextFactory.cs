using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MediatorExample.Infrastructure.SqlServerData.Context
{
    public class SqlServerContextFactory : IDesignTimeDbContextFactory<SqlServerContext>
    {
        public SqlServerContext CreateDbContext(string[] args)
        {
            string appSettingsPath = Path.Combine(Directory.GetCurrentDirectory(), "..//Application//");

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(appSettingsPath)
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<SqlServerContext>();
            builder.UseSqlServer(
                configuration
                    .GetSection("ConnectionSettings:SqlServer:ConnectionString")
                    .Value
            );

            return new SqlServerContext(builder.Options);
        }
    }
}