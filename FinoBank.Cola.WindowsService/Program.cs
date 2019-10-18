using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Contesto.V2.Core.Infrastructure.ConfigurationService.Extensions;
using FinoBank.Cola.Manager.Helpers;
using FinoBank.Cola.Manager.Mappers;
using FinoBank.Cola.Repository.Uom;
using FinoBank.Cola.Repository.Uom.Interfaces;
using FinoBank.Cola.WindowsService.IOC;
using FinoBank.Cola.WindowsService.JobFactories;
using FinoBank.Cola.WindowsService.Jobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;
using System;
using System.Linq;
using System.Reflection;
using Topshelf;
using Topshelf.Autofac;
using Contesto.V2.Core.Infrastructure.LoggerService.Extensions;

namespace FinoBank.Cola.WindowsService
{
    public class Program 
    {
      
        public static IConfigurationRoot Configuration;

        public static ILoggerFactory loggerFactory;

        public static void Main()
        {

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

            // logging
            autofacBuilder.RegisterType<LoggerFactory>()
                      .As<ILoggerFactory>()
                      .SingleInstance();
            autofacBuilder.RegisterGeneric(typeof(Logger<>))
                            .As(typeof(ILogger<>))
                            .SingleInstance();
            var container = autofacBuilder.Build();
            var loggerFactory = container.Resolve<ILoggerFactory>();
            

            //Write  LogLevel.Information from LogLevel.None to check logs 
            loggerFactory.AddContext(Contesto.V2.Core.Infrastructure.LoggerService.Dtos.LoggerTypeEnum.Database, LogLevel.Information, Configuration.GetConnectionString("DefaultConnection"));
            
            // construct a scheduler factory
            ISchedulerFactory schedFact = new StdSchedulerFactory();
            IScheduler sched = schedFact.GetScheduler().Result;
            sched.JobFactory = new IocJobFactory(container);
            sched.Start();

            //TransactionRequestExpirationJob
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

            //MerchantAcceptanceExpirationJob
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
          
            //CleanSMSlogs
            IJobDetail checkForSMSLogsJob = JobBuilder.Create<CheckForSMSLogs>()
              .WithIdentity("CheckForSMSLogs", "CheckForSMSLogsGroup")
              .Build();

            ITrigger checkForSMSLogsJobTrigger = TriggerBuilder.Create()
              .WithIdentity("CheckForSMSLogsTrigger", "CheckForSMSLogsGroup")
              .StartNow()
              .WithSimpleSchedule(x => x
                  .WithIntervalInMinutes(1)
                  .RepeatForever())
              .Build();

            sched.ScheduleJob(checkForSMSLogsJob, checkForSMSLogsJobTrigger);

            ////UserTokenHistory
            //IJobDetail checkForUserTokenHistory = JobBuilder.Create<CheckForUserTokenHistory>()
            //.WithIdentity("CheckForUserTokenHistory", "CheckForUserTokenHistoryGroup")
            //.Build();

            //ITrigger checkForUserTokenHistoryTrigger = TriggerBuilder.Create()
            //.WithIdentity("CheckForUserTokenHistoryTrigger", "CheckForUserTokenHistoryGroup")
            //.StartNow()
            //.WithSimpleSchedule(x => x
            //.WithIntervalInMinutes(1)
            //.RepeatForever())
            //.Build();

            //sched.ScheduleJob(checkForUserTokenHistory, checkForUserTokenHistoryTrigger);

            // logging
            ConfigureServices(services);
            var serviceProvider = services.AddLogging().BuildServiceProvider();
            Configure(serviceProvider.GetService<ILoggerFactory>());
            var logger = serviceProvider.GetService<ILogger<Program>>();
            logger.LogInformation("Window Service Started");

            var rc =  HostFactory.Run(x =>
            {
                x.UseAutofacContainer(container);
                x.Service<ColaService>(s =>
                {
                    s.WhenStarted(service => service.OnStart());
                    s.WhenStopped(service => service.OnStop());
                    s.ConstructUsing(() => new ColaService());
                });
            x.RunAsLocalSystem()
                   .DependsOnEventLog()
                   .StartAutomatically()
                   .EnableServiceRecovery(r => r.RestartService(1));

                x.SetServiceName("FinoBank-Cola-Windows-Service");
                x.SetDisplayName("FinoBank Cola Windows Service");
                x.SetDescription("FinoBank Cola");
            });
            
            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());   
            Environment.ExitCode = exitCode;
            logger.LogInformation("Window Service Stopped");
        }

        public static void Configure(ILoggerFactory loggerFactory)
        {
            BaseConfigure(loggerFactory);
        }

        public static void BaseConfigure(ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddContext(Contesto.V2.Core.Infrastructure.LoggerService.Dtos.LoggerTypeEnum.Database, LogLevel.Information, Configuration.GetConnectionString("DefaultConnection"));
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(configure => configure.AddConsole())
           .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Information)
           .AddTransient<Program>();
        }
    }
}