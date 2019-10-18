using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Contesto.V2.Core.Common.Api.Base;
using FinoBank.Cola.Manager.Helpers;
using FinoBank.Cola.Manager.IOC;
using FinoBank.Cola.Manager.Mappers;
using FinoBank.Cola.Repository.Uom;
using FinoBank.Cola.Repository.Uom.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace FinoBank.Cola.Api
{
    /// <summary>
    /// StartupKitApplicationStartup module Startup
    /// </summary>
    /// <seealso cref="Startup" />
    public class FinoColaStartup : Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FinoColaStartup"/> class.
        /// </summary>
        /// <param name="env">The env.</param>
        public FinoColaStartup(IHostingEnvironment env) : base(env) { }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            BaseConfigureServices(services);

            services.AddSingleton<IConfigurationSettingFromCacheHelper, ConfigurationSettingFromCacheHelper>();
            //AutoMapper Configuration
            services.AddAutoMapper(typeof(Startup));
            var config = new MapperConfiguration(cfg => { cfg.AddProfile(new ModelsAutoMapper()); });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            //Autofac Configuration
            services.AddSingleton<IUnitOfWork, UnitOfWork>(x => new UnitOfWork(base.Configuration.GetConnectionString("DefaultConnection")));
            var builder = new ContainerBuilder();
            builder.RegisterModule<ManagerContainer>();
            builder.Populate(services);
            var container = builder.Build();
            return container.Resolve<IServiceProvider>();
        }

        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            BaseConfigure(app, env, loggerFactory);
        }
    }
}