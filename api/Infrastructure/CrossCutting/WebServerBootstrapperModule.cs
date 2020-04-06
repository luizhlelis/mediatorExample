using System;
using Autofac;
using MediatorExample.Infrastructure.CrossCutting.Modules;

namespace MediatorExample.Infrastructure.CrossCutting
{
    public class WebServerBootstrapperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new SqlServerRepositoryModule());
            builder.RegisterModule(new DomainServiceModule());
        }
    }
}
