using Contesto.V2.Core.Common.Api.Base;
using Contesto.V2.Core.Common.Manager.Helpers;
using Contesto.V2.Core.Common.Utility.Models;
using FinoBank.Cola.Manager.Helpers;
using FinoBank.Cola.Manager.Interfaces;
using FinoBank.Cola.Manager.ViewModels;
using FinoBank.Cola.Manager.ViewModelValidators;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinoBank.Cola.Web.Api.Controllers
{
    /// <summary>
    /// Search
    /// </summary>
    /// <seealso cref="Contesto.V2.Core.Common.Api.Base.BaseApiController" />
    /// <seealso cref="BaseApiController" />
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/v1/search")]
    public class SearchController : BaseApiController
    {
        /// <summary>
        /// The query merchant search manager service
        /// </summary>
        private readonly IQueryMerchantSearchManagerService _queryMerchantSearchManagerService;

        /// <summary>
        /// The configuration setting from cache helper
        /// </summary>
        private readonly IConfigurationSettingFromCacheHelper _configurationSettingFromCacheHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchController" /> class.
        /// </summary>
        /// <param name="queryMerchantSearchManagerService">The query merchant search manager service.</param>
        /// <param name="configurationSettingFromCacheHelper">The configuration setting from cache helper.</param>
        public SearchController(IQueryMerchantSearchManagerService queryMerchantSearchManagerService,
            IConfigurationSettingFromCacheHelper configurationSettingFromCacheHelper)
        {
            _queryMerchantSearchManagerService = queryMerchantSearchManagerService;
            _configurationSettingFromCacheHelper = configurationSettingFromCacheHelper;
        }

        /// <summary>
        /// Searches the merchants.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpGet("merchant")]
        public async Task<ActionResult> Search([FromQuery]MerchantSearchRequestViewModel model)
        {
            var validationResults = GetValidationResult(model, new MerchantSearchRequestViewModelValidation());
            if (!validationResults.IsValid)
                return BadRequest(BuildValidationErrorMessage(validationResults));

            model.PageSize = Convert.ToInt32(_configurationSettingFromCacheHelper.AppSettings("MERCHANT_SEARCH_PAGESIZE"));

            var result = await _queryMerchantSearchManagerService.GetMerchantSearchDataWithPagingAsync(model).ConfigureAwait(false);

            if (result.Success)
            {
                if (result.Data.Result.Any())
                    return Ok(result);
                else
                {
                    return Ok("No Data Found");
                }
            }
            return BadRequest(result.ErrorMessages);
        }
    }
}