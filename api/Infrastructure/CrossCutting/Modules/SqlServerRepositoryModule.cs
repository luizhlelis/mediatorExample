using System;
using Autofac;
using MediatorExample.Domain.Data.SqlServerRepositoryContract.Generic;
using MediatorExample.Domain.Services;
using NexaDb.Infra.Data.Repositories.Generic;

namespace MediatorExample.Infrastructure.CrossCutting.Modules
{
    public class SqlServerRepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(EntityRepository<>))
                .As(typeof(IEntityRepository<>));
        }
    }
}
