using AdPortal.Infrastructure.IoC.Modules;
using Autofac;
using Microsoft.Extensions.Configuration;

namespace AdPortal.Infrastructure.IoC
{
    public class ContainerModule : Autofac.Module
    {
        private readonly IConfiguration _configuration;
        public ContainerModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<RepositoryModule>();
            builder.RegisterModule<MongoModule>();
            builder.RegisterModule<ServiceModule>();
            builder.RegisterModule<CommandModule>();
            builder.RegisterModule(new SettingsModule(_configuration));
        }
    }
}