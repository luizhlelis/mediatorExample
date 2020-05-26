using System;
using Autofac;
using MediatorExample.Infrastructure.IoC.Modules;

namespace MediatorExample.Infrastructure.IoC
{
    public class WebServerBootstrapperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new SqlServerRepositoryModule());
            builder.RegisterModule(new NotificationHandlerModule());
        }
    }
}
