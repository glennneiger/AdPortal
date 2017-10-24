using System.Reflection;
using AdPortal.Infrastructure.Mongo;
using AdPortal.Infrastructure.Repositories;
using Autofac;
using MongoDB.Driver;

namespace AdPortal.Infrastructure.IoC.Modules
{
    public class MongoModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register((c,p) =>
            {
                var settings = c.Resolve<MongoSettings>();
                return new MongoClient(settings.ConnectionString);
            }).SingleInstance();

            builder.Register((c,p) =>
            {
                var client = c.Resolve<MongoClient>();
                var settings = c.Resolve<MongoSettings>();
                var database = client.GetDatabase(settings.Database);
                return database;
            }).As<IMongoDatabase>();

            var assembly = typeof(MongoModule)
                .GetTypeInfo()
                .Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.IsAssignableTo<IMongoRepository>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            
        }
    }
}