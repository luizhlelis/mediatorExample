using System;
using Autofac;
using MediatorExample.Domain.NotificationHandlers;
using MediatorExample.Infra.SqlServerData.Repositories.Generic;

namespace MediatorExample.Infrastructure.IoC.Modules
{
    public class NotificationHandlerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(NoSqlHandler))
                .As(typeof(INoSqlHandler));
        }
    }
}
