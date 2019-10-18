using Contesto.V2.Core.Common.Api.Base;
using FinoBank.Cola.Manager.Helpers;
using FinoBank.Cola.Manager.Interfaces;
using FinoBank.Cola.Manager.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FinoBank.Cola.Api.Controllers
{
    /// <summary>
    /// Otp control
    /// </summary>
    /// <seealso cref="Contesto.V2.Core.Common.Api.Base.BaseApiController" />
    [Route("api/v1/OTP")]
    [ApiController]
    public class OTPController : BaseApiController
    {
        /// <summary>
        /// The query generate otp manager service
        /// </summary>
        private readonly IQueryOTPManagerService _queryGenerateOTPManagerService;

        /// <summary>
        /// The configuration setting from cache helper
        /// </summary>
        private readonly IConfigurationSettingFromCacheHelper _configurationSettingFromCacheHelper;

        /// <summary>
        /// The service URL
        /// </summary>
        private readonly string serviceUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="OTPController"/> class.
        /// </summary>
        /// <param name="queryGenerateOTPManagerService">The query generate otp manager service.</param>
        /// <param name="configurationSettingFromCacheHelper">The configuration setting from cache helper.</param>
        public OTPController(IQueryOTPManagerService queryGenerateOTPManagerService,
            IConfigurationSettingFromCacheHelper configurationSettingFromCacheHelper)
        {
            _queryGenerateOTPManagerService = queryGenerateOTPManagerService;
            _configurationSettingFromCacheHelper = configurationSettingFromCacheHelper;
            serviceUrl = _configurationSettingFromCacheHelper.AppSettings("INTEGRATION_OTP_SERVICEURL");
        }

        /// <summary>
        /// Posts the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost("GenerateOTP")]
        public async Task<ActionResult> Post([FromBody]GenerateOTPViewModel model)
        {
            var results = await _queryGenerateOTPManagerService.GenerateOTP(serviceUrl, model).ConfigureAwait(false);
            if (!results.Success)
                return BadRequest(results.ErrorMessages);
            if (results != null && results.Success)
            {
                return Ok(results);
            }
            return BadRequest(results.ErrorMessages);
        }

        /// <summary>
        /// Posts the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost("VerifyOTP")]
        public async Task<ActionResult> Post([FromBody]VerifyOTPViewModel model)
        {
            var results = await _queryGenerateOTPManagerService.VerifyOTP(serviceUrl, model).ConfigureAwait(false);
            if (!results.Success)
                return BadRequest(results.ErrorMessages);
            if (results != null && results.Success)
            {
                return Ok(results);
            }
            return BadRequest(results.ErrorMessages);
        }
    }
}