using System;
using System.Threading.Tasks;
using FinoBank.Cola.Manager.Helpers;
using FinoBank.Cola.Manager.Interfaces;
using Microsoft.Extensions.Logging;
using Quartz;

namespace FinoBank.Cola.WindowsService.Jobs
{
    [DisallowConcurrentExecution]
    internal class CheckForSMSLogs : IJob
    {
        /// <summary>
        /// The query application manager service
        /// </summary>
        private readonly ICommandSMSlogManagerService _commandSMSlogManagerService;
       
        /// <summary>
        /// The configuration setting from cache helper
        /// </summary>
        private readonly IConfigurationSettingFromCacheHelper _configurationSettingFromCacheHelper;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="CheckForMerchantAcceptanceExpiration" /> class.
        /// </summary>
        /// <param name="queryCheckForMerchantAcceptanceExpirationManagerService">The query check for merchant acceptance expiration manager service.</param>
        public CheckForSMSLogs(ICommandSMSlogManagerService commandSMSlogManagerService,
            IConfigurationSettingFromCacheHelper configurationSettingFromCacheHelper)
        {
            _commandSMSlogManagerService = commandSMSlogManagerService;
            _configurationSettingFromCacheHelper = configurationSettingFromCacheHelper;
        }
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Started Job CleanSMSlogs" + DateTime.Now.ToString());
            return Task.Run(() => CleanSMSlogs());
        }
        private async Task CleanSMSlogs()
        {
            try
            {
                var interval =  Convert.ToInt32(_configurationSettingFromCacheHelper.AppSettings("CLEAN_SMS_LOGS"));
                await _commandSMSlogManagerService.DeleteSMSlog(interval).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}