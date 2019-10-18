using Contesto.V2.Core.Common.Api.Base;
using Contesto.V2.Core.Common.Manager.Results;
using FinoBank.Cola.Manager.Helpers;
using FinoBank.Cola.Manager.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FinoBank.Cola.Api.Controllers
{
    /// <summary>
    /// Configuration Setting Controller
    /// </summary>
    /// <seealso cref="Contesto.V2.Core.Common.Api.Base.BaseApiController" />
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/v1/configurationSetting")]
    public class ConfigurationSettingController : BaseApiController
    {
        /// <summary>
        /// The configuration setting from cache helper
        /// </summary>
        private readonly IConfigurationSettingFromCacheHelper _configurationSettingFromCacheHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationSettingController"/> class.
        /// </summary>
        /// <param name="configurationSettingFromCacheHelper">The configuration setting from cache helper.</param>
        public ConfigurationSettingController(IConfigurationSettingFromCacheHelper configurationSettingFromCacheHelper)
        {
            _configurationSettingFromCacheHelper = configurationSettingFromCacheHelper;
        }

        /// <summary>
        /// Gets Method.
        /// </summary>
        /// <param name="environment">The environment.</param>
        /// <param name="configKey">The configuration key.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Get(string configKey)
        {
            var result = new OperationResult<CommandSuccessStringResultViewModel>
            {
                Data = new CommandSuccessStringResultViewModel() { ResponseValue = _configurationSettingFromCacheHelper.AppSettings(configKey) }
            };
            if(result.Data.ResponseValue!= null && result.Data.ResponseValue!="")
            {
                result.Success = true;
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}