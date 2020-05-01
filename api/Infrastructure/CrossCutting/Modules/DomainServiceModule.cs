using System;
using Autofac;
using MediatorExample.Domain.Services.Concrete;
using MediatorExample.Domain.Services.Contract;

namespace MediatorExample.Infrastructure.CrossCutting.Modules
{
    public class DomainServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CustomerDomainService>()
                .As<ICustomerDomainService>();
        }
    }
}
