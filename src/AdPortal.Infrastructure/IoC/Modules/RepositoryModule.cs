using System.Reflection;
using AdPortal.Core.Repositories;
using Autofac;
using Microsoft.Extensions.Configuration;

namespace AdPortal.Infrastructure.IoC.Modules
{
    public class RepositoryModule : Autofac.Module
    {
      
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(RepositoryModule)
                .GetTypeInfo()
                .Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.IsAssignableTo<IRepository>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            
        }
    }
}