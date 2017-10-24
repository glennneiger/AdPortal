using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdPortal.Core.Repositories;
using AdPortal.Infrastructure.IoC;
using AdPortal.Infrastructure.IoC.Modules;
using AdPortal.Infrastructure.Mapper;
using AdPortal.Infrastructure.Mongo;
using AdPortal.Infrastructure.Repositories;
using AdPortal.Infrastructure.Services;
using AdPortal.Infrastructure.Settings;
using AdPortal.MVC.Framework;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;


namespace AdPortal.MVC
{
    public class Startup
    {
        public IContainer ApplicationContainer {get; set;}
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddAuthentication("CookieAuth")
            .AddCookie("CookieAuth" , options =>
            {
            options.AccessDeniedPath = "/Account/Forbidden/";
            options.LoginPath = "/User/Login/";
            });
            
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, InMemoryUserRepository>();
            services.AddSingleton(AutoMapperConfig.Initialize());
            services.AddMvc();

           



            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule(new ContainerModule(Configuration));
            ApplicationContainer = builder.Build();
            return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime appLifetime)
        {

            app.UseAuthentication();
            MongoConfigurator.Initialize();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            
            app.UseStaticFiles();
            app.UseMiddleware(typeof(ExceptionHandlerMiddleware));
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            ;
            var generalSettings = app.ApplicationServices.GetService<GeneralSettings>();
            if(generalSettings.SeedData)
            {
                var datainitializer = app.ApplicationServices.GetService<IDataInitializer>();
                datainitializer.SeedAsync();

            }
            appLifetime.ApplicationStopped.Register(()=>ApplicationContainer.Dispose());
        }
    }
}
