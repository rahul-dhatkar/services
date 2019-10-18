///-------------------------------------------------------------------------------------------
///** Copyright © 2018, National Arbitration and Mediation                                  **
///** All rights reserved.                                                                  **
///**                                                                                       **
///** Redistribution, re-engineering or use of this code - in source                        **
///** or binary forms with or without modifications, are not                                **
///** permitted without prior written consent from appropriate person                       **
///** in National Arbitration and Mediation                                                 **
///**                                                                                       **
///**                                                                                       **
///** Author    : Fulcrum World Wide                                                        **
///** Created   : 20-06-18                                                                  **
///** Purpose   : Set IOC values                                                            **
///**                                                                                       **
///**                                                                                       **
///**                                                                                       **
///** Change Log:                                                                           **
///** ==================================                                                    **
///** Name          Date         Purpose                                                    **
///** Nam Team      20-06-18     Created                                                    **
///**                                                                                       **
///-------------------------------------------------------------------------------------------

using Autofac;
using AutoMapper;
using Contesto.V2.Core.Common.Manager.Ioc;
using FinoBank.Cola.Manager.Interfaces;
using FinoBank.Cola.Manager.Queries;
using FinoBank.Cola.Repository.Uom.Interfaces;
using System.Reflection;

namespace FinoBank.Cola.Scheduler.IOC
{
    /// <summary>
    /// dependency injection for Master manager
    /// </summary>
    /// <seealso cref="CaseManagement.Core.Common.Manager.Base.BaseManagerAutofacContainer" />
    public class SchedulerContainer : BaseManagerAutofacContainer
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
                   new QueryCheckForMerchantAcceptanceExpirationManagerService(c.Resolve<IMapper>(),
                   c.Resolve<IUnitOfWork>()
                   )).As<IQueryCheckForMerchantAcceptanceExpirationManagerService>();

            builder.Register(
               c =>
                   new QueryCheckForTransactionRequestExpirationManagerService(c.Resolve<IMapper>(),
                   c.Resolve<IUnitOfWork>()
                   )).As<IQueryCheckForTransactionRequestExpirationManagerService>();

            base.Load(builder);
        }
    }
}