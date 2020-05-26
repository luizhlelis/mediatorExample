﻿using System;
using Autofac;
using MediatorExample.Domain.Data.SqlServerRepositoryContract.Generic;
using MediatorExample.Infra.SqlServerData.Repositories.Generic;

namespace MediatorExample.Infrastructure.IoC.Modules
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
