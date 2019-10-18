using FinoBank.Cola.Manager.Helpers;
using FinoBank.Cola.Manager.Interfaces;
using Quartz;
using System;
using System.Threading.Tasks;

namespace FinoBank.Cola.Scheduler.Jobs
{
    [DisallowConcurrentExecution]
    internal class CheckForMerchantAcceptanceExpiration : IJob
    {
        /// <summary>
        /// The query application manager service
        /// </summary>
        private readonly IQueryCheckForMerchantAcceptanceExpirationManagerService _queryCheckForMerchantAcceptanceExpirationManagerService;

        /// <summary>
        /// The configuration setting from cache helper
        /// </summary>
        private readonly IConfigurationSettingFromCacheHelper _configurationSettingFromCacheHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckForMerchantAcceptanceExpiration" /> class.
        /// </summary>
        /// <param name="queryCheckForMerchantAcceptanceExpirationManagerService">The query check for merchant acceptance expiration manager service.</param>
        public CheckForMerchantAcceptanceExpiration(IQueryCheckForMerchantAcceptanceExpirationManagerService queryCheckForMerchantAcceptanceExpirationManagerService,
            IConfigurationSettingFromCacheHelper configurationSettingFromCacheHelper)
        {
            _queryCheckForMerchantAcceptanceExpirationManagerService = queryCheckForMerchantAcceptanceExpirationManagerService;
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
            Console.WriteLine("Started Job MerchantAcceptanceExpiration" + DateTime.Now.ToString());
            return Task.Run(() => MerchantAcceptanceExpiration());
        }

        /// <summary>
        /// Merchants the acceptance expiration.
        /// </summary>
        /// <returns></returns>
        private async Task MerchantAcceptanceExpiration()
        {
            try
            {
                var interval = Convert.ToInt32(_configurationSettingFromCacheHelper.AppSettings("MERCHANT_ACCEPTANCE_EXPIRATION_SLA_MINUTES"));
                await _queryCheckForMerchantAcceptanceExpirationManagerService.CheckForMerchantAcceptanceExpiration(interval).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}