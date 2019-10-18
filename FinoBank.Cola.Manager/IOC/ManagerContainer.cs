///-------------------------------------------------------------------------------------------
///** Copyright © 2018, Fulcrum Digital                                  **
///** All rights reserved.                                                                  **
///**                                                                                       **
///** Redistribution, re-engineering or use of this code - in source                        **
///** or binary forms with or without modifications, are not                                **
///** permitted without prior written consent from appropriate person                       **
///** in Fulcrum Digital                                                 **
///**                                                                                       **
///**                                                                                       **
///** Author    : Fulcrum World Wide                                                        **
///** Created   : 07-02-18                                                                  **
///** Purpose   : Set IOC values                                                            **
///**                                                                                       **
///**                                                                                       **
///**                                                                                       **
///** Change Log:                                                                           **
///** ==================================                                                    **
///** Name          Date         Purpose                                                    **
///** Nam Team      07-02-18     Created                                                    **
///**                                                                                       **
///-------------------------------------------------------------------------------------------

using Autofac;
using AutoMapper;
using Contesto.V2.Core.Common.Manager.Ioc;
using FinoBank.Cola.Manager.Commands;
using FinoBank.Cola.Manager.Helpers;
using FinoBank.Cola.Manager.Interfaces;
using FinoBank.Cola.Manager.Queries;
using FinoBank.Cola.Repository.Uom.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Reflection;

namespace FinoBank.Cola.Manager.IOC
{
    /// <summary>
    /// dependency injection for Master manager
    /// </summary>
    /// <seealso cref="Contesto.V2.Core.Common.Manager.Ioc.BaseManagerAutofacContainer" />
    /// <seealso cref="BaseManagerAutofacContainer" />
    public class ManagerContainer : BaseManagerAutofacContainer
    {
        /// <summary>
        /// Override to add registrations to the container.
        /// </summary>
        /// <param name="builder">The builder through which components can be
        /// registered.</param>
        /// <remarks>
        /// Note that the ContainerBuilder parameter is unique to this module.
        /// </remarks>
        protected override void Load(ContainerBuilder builder)
        {
            var dataAccess = Assembly.GetEntryAssembly();
            builder.RegisterAssemblyTypes(dataAccess).Where(t => t.Name.EndsWith("ManagerService")).AsImplementedInterfaces();

            builder.Register(
               c =>
                   new QuerySampleManagerService(c.Resolve<IMapper>(),
                   c.Resolve<IMemoryCache>(),
                   c.Resolve<IUnitOfWork>())).As<IQuerySampleManagerService>();

            builder.Register(
               c =>
                   new QueryCustomerSummaryManagerService(c.Resolve<IMapper>(),
                   c.Resolve<IMemoryCache>(),
                   c.Resolve<IUnitOfWork>())).As<IQueryCustomerSummaryManagerService>();

            builder.Register(
              c =>
                  new QueryUserTokenHistoryManagerService(c.Resolve<IMapper>(),
                  c.Resolve<IUnitOfWork>())).As<IQueryUserTokenHistoryManagerService>();


            builder.Register(
           c =>
               new QueryAcceptTransactionRequestManagerService(c.Resolve<IMapper>(),
               c.Resolve<IUnitOfWork>())).As<IQueryAcceptTransactionRequestManagerService>();

            
            builder.Register(
           c =>
               new QueryMerchantBulkUploadManagerService(c.Resolve<IMapper>(),
               c.Resolve<IUnitOfWork>())).As<IQueryMerchantBulkUploadManagerService>();

            builder.Register(
            c =>
                new QueryMerchantSearchManagerService(c.Resolve<IMapper>(),
                c.Resolve<IMemoryCache>(),
                c.Resolve<IUnitOfWork>())).As<IQueryMerchantSearchManagerService>();

            builder.Register(
         c =>
             new QueryMasterDataManagerService(c.Resolve<IMapper>(),
             c.Resolve<IMemoryCache>(),
             c.Resolve<IUnitOfWork>())).As<IQueryMasterDataManagerService>();

            builder.Register(
           c =>
               new QueryTransactionSummaryManagerService(c.Resolve<IMapper>(),
               c.Resolve<IMemoryCache>(),
               c.Resolve<IUnitOfWork>())).As<IQueryTransactionSummaryManagerService>();

            builder.Register(
            c =>
                new QueryMerchantSummaryManagerService(c.Resolve<IMapper>(),
                c.Resolve<IMemoryCache>(),
                c.Resolve<IUnitOfWork>())).As<IQueryMerchantSummaryManagerService>();

           builder.Register(
           c =>
               new QueryMerchantSummaryDataManagerService(c.Resolve<IMapper>(),
               c.Resolve<IMemoryCache>(),
               c.Resolve<IUnitOfWork>())).As<IQueryMerchantSummaryDataManagerService>();

            builder.Register(
            c =>
                new QueryOTPManagerService(c.Resolve<IMapper>(),
                c.Resolve<IMemoryCache>(),
                c.Resolve<IUnitOfWork>(),
                c.Resolve<IConfigurationSettingFromCacheHelper>()
                )).As<IQueryOTPManagerService>();
      
            builder.Register(
               c =>
                   new CommandSampleManagerService(c.Resolve<IMapper>(),
                   c.Resolve<IUnitOfWork>())).As<ICommandSampleManagerService>();

            builder.Register(
              c =>
                  new CommandTransactionRequestsManagerService(c.Resolve<IMapper>(),
                  c.Resolve<IUnitOfWork>())).As<ICommandTransactionRequestsManagerService>();

            builder.Register(
              c =>
                  new CommandTransactionFeedbacksManagerService(c.Resolve<IMapper>(),
                  c.Resolve<IUnitOfWork>())).As<ICommandTransactionFeedbacksManagerService>();

            builder.Register(
                          c =>
                              new CommandUpdateTransactionRequestsManagerService(c.Resolve<IMapper>(),
                              c.Resolve<IUnitOfWork>())).As<ICommandUpdateTransactionRequestsManagerService>();
            builder.Register(
                          c =>
                              new CommandUpdateTransactionRequestsManagerService(c.Resolve<IMapper>(),
                              c.Resolve<IUnitOfWork>())).As<ICommandUpdateTransactionRequestsManagerService>();
            builder.Register(
                        c =>
                            new CommandMerchantManagerService(c.Resolve<IMapper>(),
                            c.Resolve<IUnitOfWork>())).As<ICommandCreateMerchantsManagerService>();
            builder.Register(
                       c =>
                           new CommandMerchantSetupManagerService(c.Resolve<IMapper>(),
                           c.Resolve<IUnitOfWork>())).As<ICommandMerchantSetupManagerService>();
            builder.Register(
                       c =>
                           new CommandCustomerManagerService(c.Resolve<IMapper>(),
                           c.Resolve<IUnitOfWork>())).As<ICommandCustomerManagerService>();

            builder.Register(
                       c =>
                           new CommandActivityManagerService(c.Resolve<IMapper>(),
                           c.Resolve<IUnitOfWork>())).As<ICommandActivityManagerService>();
            builder.Register(
                      c =>
                          new CommandSMSlogManagerService(c.Resolve<IMapper>(),
                          c.Resolve<IUnitOfWork>())).As<ICommandSMSlogManagerService>();

            base.Load(builder);
        }
    }
}