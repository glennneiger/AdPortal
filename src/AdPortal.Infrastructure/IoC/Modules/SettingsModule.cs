using System.Reflection;
using AdPortal.Infrastructure.Extensions;
using AdPortal.Infrastructure.Mongo;
using AdPortal.Infrastructure.Settings;
using Autofac;
using Microsoft.Extensions.Configuration;

namespace AdPortal.Infrastructure.IoC.Modules
{
    public class SettingsModule : Autofac.Module
    {

        private readonly IConfiguration _configuration;
        public SettingsModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_configuration.GetSettings<GeneralSettings>())
                    .SingleInstance();
            builder.RegisterInstance(_configuration.GetSettings<CookiesAuthSettings>())
                    .SingleInstance();
            builder.RegisterInstance(_configuration.GetSettings<MongoSettings>())
                    .SingleInstance();

        }
    }
}