﻿using Autofac;
using Quartz;
using Quartz.Spi;
using System;

namespace FinoBank.Cola.Scheduler.JobFactories
{
    /// <summary>
    /// IocJobFactory
    /// </summary>
    /// <seealso cref="Quartz.Spi.IJobFactory" />
    internal class IocJobFactory : IJobFactory
    {
        /// <summary>
        /// The factory
        /// </summary>
        private readonly IContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="IocJobFactory" /> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public IocJobFactory(IContainer container)
        {
            _container = container;
        }

        /// <summary>
        /// Called by the scheduler at the time of the trigger firing, in order to
        /// produce a <see cref="T:Quartz.IJob" /> instance on which to call Execute.
        /// </summary>
        /// <param name="bundle">The TriggerFiredBundle from which the <see cref="T:Quartz.IJobDetail" />
        /// and other info relating to the trigger firing can be obtained.</param>
        /// <param name="scheduler">a handle to the scheduler that is about to execute the job</param>
        /// <returns>
        /// the newly instantiated Job
        /// </returns>
        /// <remarks>
        /// It should be extremely rare for this method to throw an exception -
        /// basically only the case where there is no way at all to instantiate
        /// and prepare the Job for execution.  When the exception is thrown, the
        /// Scheduler will move all triggers associated with the Job into the
        /// <see cref="F:Quartz.TriggerState.Error" /> state, which will require human
        /// intervention (e.g. an application restart after fixing whatever
        /// configuration problem led to the issue with instantiating the Job).
        /// </remarks>
        /// <throws>  SchedulerException if there is a problem instantiating the Job. </throws>
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return (IJob)_container.Resolve(bundle.JobDetail.JobType);
        }

        /// <summary>
        /// Allows the job factory to destroy/cleanup the job if needed.
        /// </summary>
        /// <param name="job"></param>
        public void ReturnJob(IJob job)
        {
            var disposable = job as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
    }
}