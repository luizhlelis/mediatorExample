using Infrastructure.NoSqlServerData.Repositories.Generic;
using MediatorExample.Domain.Data.NoSqlServerRepositoryContract.Generic;
using MediatorExample.Domain.Data.SqlServerRepositoryContract.Generic;
using MediatorExample.Domain.NotificationHandlers;
using MediatorExample.Infra.SqlServerData.Repositories.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace MediatorExample.Infrastructure.IoC
{
    public static class IocWebAppContainer
    {   
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IEntityRepository<>), typeof(EntityRepository<>))
                .AddScoped(typeof(IMongoGenericRepository<>), typeof(MongoGenericRepository<>));
        }
    }
}
