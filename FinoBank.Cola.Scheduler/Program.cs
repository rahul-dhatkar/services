using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Contesto.V2.Core.Infrastructure.ConfigurationService.Extensions;
using FinoBank.Cola.Manager.Helpers;
using FinoBank.Cola.Manager.Mappers;
using FinoBank.Cola.Repository.Uom;
using FinoBank.Cola.Repository.Uom.Interfaces;
using FinoBank.Cola.Scheduler.IOC;
using FinoBank.Cola.Scheduler.JobFactories;
using FinoBank.Cola.Scheduler.Jobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using System;
using System.Reflection;
using Topshelf;
using Topshelf.Autofac;

namespace FinoBank.Cola.Scheduler
{
    internal class Program
    {
        public static IConfigurationRoot Configuration;

        private static void Main(string[] args)
        {
            #region "ServiceConfiguration"

            var services = new ServiceCollection();
            var builder = new ConfigurationBuilder()
               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();

            // Add DbConfiguration reader
            services.AddDbConfigurationService(Configuration);
            // Caching Configuration
            services.AddMemoryCache();

            services.AddSingleton<IConfigurationSettingFromCacheHelper, ConfigurationSettingFromCacheHelper>();

            // AutoMapper Configuration
            services.AddAutoMapper(typeof(Program));
            var config = new MapperConfiguration(cfg => { cfg.AddProfile(new ModelsAutoMapper()); });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            services.AddSingleton<IUnitOfWork, UnitOfWork>(x => new UnitOfWork(Configuration.GetConnectionString("DefaultConnection")));

            var autofacBuilder = new ContainerBuilder();
            autofacBuilder.RegisterModule<SchedulerContainer>();
            autofacBuilder.Populate(services);
            autofacBuilder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).Where(x => typeof(IJob).IsAssignableFrom(x));
            var container = autofacBuilder.Build();

            // construct a scheduler factory
            ISchedulerFactory schedFact = new StdSchedulerFactory();
            IScheduler sched = schedFact.GetScheduler().Result;
            sched.JobFactory = new IocJobFactory(container);
            sched.Start();

            #endregion "ServiceConfiguration"

            IJobDetail checkForTransactionRequestExpirationJob = JobBuilder.Create<CheckForTransactionRequestExpiration>()
                .WithIdentity("CheckForTransactionRequestExpiration", "CheckForTransactionRequestExpirationGroup")
                .Build();

            ITrigger checkForTransactionRequestExpirationJobTrigger = TriggerBuilder.Create()
              .WithIdentity("CheckForTransactionRequestExpirationtTrigger", "CheckForTransactionRequestExpirationGroup")
              .StartNow()
              .WithSimpleSchedule(x => x
                  .WithIntervalInMinutes(1)
                  .RepeatForever())
              .Build();

            sched.ScheduleJob(checkForTransactionRequestExpirationJob, checkForTransactionRequestExpirationJobTrigger);

            IJobDetail checkForMerchantAcceptanceExpirationJob = JobBuilder.Create<CheckForMerchantAcceptanceExpiration>()
               .WithIdentity("CheckForMerchantAcceptanceExpiration", "CheckForMerchantAcceptanceExpirationGroup")
               .Build();

            ITrigger checkForMerchantAcceptanceExpirationJobTrigger = TriggerBuilder.Create()
              .WithIdentity("CheckForMerchantAcceptanceExpiration", "CheckForMerchantAcceptanceExpirationGroup")
              .StartNow()
              .WithSimpleSchedule(x => x
                  .WithIntervalInMinutes(1)
                  .RepeatForever())
              .Build();

            sched.ScheduleJob(checkForMerchantAcceptanceExpirationJob, checkForMerchantAcceptanceExpirationJobTrigger);

            HostFactory.Run(x =>
            {
                x.UseAutofacContainer(container);
                x.Service<Service>(s =>
                {
                    s.WhenStarted(service => service.OnStart());
                    s.WhenStopped(service => service.OnStop());
                    s.ConstructUsing(() => new Service());
                });

                x.SetServiceName("FinoBank Cola Windows Service");
                x.SetDisplayName("FinoBank Cola Windows Service");
                x.SetDescription("FinoBank Cola");
            });
        }
    }
}