using System.Reflection;
using AdPortal.Infrastructure.Services;
using Autofac;
using Microsoft.Extensions.Configuration;

namespace AdPortal.Infrastructure.IoC.Modules
{
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(ServiceModule)
                .GetTypeInfo()
                .Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.IsAssignableTo<IService>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<Encrypter>()
                .As<IEncrypter>()
                .SingleInstance();

            builder.RegisterType<LoginManager>()
                .As<ILoginManager>()
                .SingleInstance();

            builder.RegisterType<DataInitializer>()
                .As<IDataInitializer>()
                .SingleInstance();
        
    }
    }
}