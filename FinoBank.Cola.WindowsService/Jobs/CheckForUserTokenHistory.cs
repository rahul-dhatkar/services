﻿using FinoBank.Cola.Manager.Helpers;
using FinoBank.Cola.Manager.Interfaces;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;

namespace FinoBank.Cola.WindowsService.Jobs
{
    /// <summary>
    /// Check For Transaction Request Expiration
    /// </summary>
    /// <seealso cref="Quartz.IJob" />
    [DisallowConcurrentExecution]
    internal class CheckForUserTokenHistory : IJob
    {
        /// <summary>
        /// The query check for transaction request expiration manager service
        /// </summary>
        public readonly IQueryUserTokenHistoryManagerService _queryUserTokenHistoryManagerService;

        /// <summary>
        /// The configuration setting from cache helper
        /// </summary>
        private readonly IConfigurationSettingFromCacheHelper _configurationSettingFromCacheHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckForUserTokenHistory" /> class.
        /// </summary>
        /// <param name="queryCheckForTransactionRequestExpirationManagerService">The query check for transaction request expiration manager service.</param>
        public CheckForUserTokenHistory(IQueryUserTokenHistoryManagerService queryUserTokenHistoryManagerService,
            IConfigurationSettingFromCacheHelper configurationSettingFromCacheHelper)
        {
            _queryUserTokenHistoryManagerService = queryUserTokenHistoryManagerService;
            _configurationSettingFromCacheHelper = configurationSettingFromCacheHelper;
        }

        /// <summary>
        /// Called by the <see cref="T:Quartz.IScheduler" /> when a <see cref="T:Quartz.ITrigger" />
        /// fires that is associated with the <see cref="T:Quartz.IJob" />.
        /// </summary>
        /// <param name="context">The execution context.</param>
        /// <returns></returns>
        /// <remarks>
        /// The implementation may wish to set a  result object on the
        /// JobExecutionContext before this method exits.  The result itself
        /// is meaningless to Quartz, but may be informative to
        /// <see cref="T:Quartz.IJobListener" />s or
        /// <see cref="T:Quartz.ITriggerListener" />s that are watching the job's
        /// execution.
        /// </remarks>
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Started Job UserTokenHistory" + DateTime.Now.ToString());
            return Task.Run(() => GetUserTokenHistory());
        }

        /// <summary>
        /// Merchants the acceptance expiration.
        /// </summary>
        /// <returns></returns>
        private async Task GetUserTokenHistory()
        {
            try
            {
                var interval =  Convert.ToInt32(_configurationSettingFromCacheHelper.AppSettings("USER_TOKEN_HISTORY_SLA_MINUTES"));
                await _queryUserTokenHistoryManagerService.CheckForUserTokenHistory(interval).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
