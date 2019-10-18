using Contesto.V2.Core.Common.Api.Base;
using FinoBank.Cola.Manager.Helpers;
using FinoBank.Cola.Manager.Interfaces;
using FinoBank.Cola.Manager.ViewModels;
using FinoBank.Cola.Manager.ViewModelValidators;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FinoBank.Cola.Web.Api.Controllers
{
    /// <summary>
    /// Merchant Controller
    /// </summary>
    /// <seealso cref="Contesto.V2.Core.Common.Api.Base.BaseApiController" />
    [Route("api/v1/merchant")]
    public class MerchantController : BaseApiController
    {
        /// <summary>
        /// The command create merchant manager service
        /// </summary>
        private readonly ICommandCreateMerchantsManagerService _commandCreateMerchantsManagerService;

        /// <summary>
        /// The query create merchant bulk upload manager service
        /// </summary>
        private readonly IQueryMerchantBulkUploadManagerService _queryMerchantBulkUploadService;

        /// <summary>
        /// The command merchant setup manager service
        /// </summary>
        private readonly ICommandMerchantSetupManagerService _commandMerchantSetupManagerService;

        /// <summary>
        /// The query merchant summary manager service
        /// </summary>
        private readonly IQueryMerchantSummaryManagerService _queryMerchantSummaryManagerService;

        /// <summary>
        /// The query merchant summary data manager service
        /// </summary>
        private readonly IQueryMerchantSummaryDataManagerService _queryMerchantSummaryDataManagerService;

        /// <summary>
        /// The command activity manager service
        /// </summary>
        private readonly ICommandActivityManagerService _commandActivityManagerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchController" /> class.
        /// </summary>
        /// <param name="commandCreateMerchantsManagerService">The command create merchants manager service.</param>
        /// <param name="commandMerchantSetupManagerService">The command merchant setup manager service.</param>
        /// <param name="queryMerchantSummaryManagerService">The query merchant summary manager service.</param>
        public MerchantController(ICommandCreateMerchantsManagerService commandCreateMerchantsManagerService,
            ICommandMerchantSetupManagerService commandMerchantSetupManagerService,
            IQueryMerchantSummaryManagerService queryMerchantSummaryManagerService,
            IQueryMerchantSummaryDataManagerService queryMerchantSummaryDataManagerService,
            IQueryMerchantBulkUploadManagerService queryMerchantBulkUploadService,
            ICommandActivityManagerService commandActivityManagerService)
        {
            _commandCreateMerchantsManagerService = commandCreateMerchantsManagerService;
            _commandMerchantSetupManagerService = commandMerchantSetupManagerService;
            _commandActivityManagerService = commandActivityManagerService;
            _queryMerchantSummaryManagerService = queryMerchantSummaryManagerService;
            _queryMerchantSummaryDataManagerService = queryMerchantSummaryDataManagerService;
            _queryMerchantBulkUploadService = queryMerchantBulkUploadService;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="searchText">The search text.</param>
        /// <returns></returns>
        [HttpGet("getAll")]
        public async Task<ActionResult> GetAll()
        {
            var result = await _queryMerchantSummaryManagerService.GetAllMerchantData(string.Empty).ConfigureAwait(false);
            if (result != null && result.Success)
                return Ok(result);
            else
                return NotFound();
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("get/{refCode}")]
        public async Task<ActionResult> GetMerchantDataById(string refCode)
        {
            var result = await _queryMerchantSummaryDataManagerService.GetMerchantDataById(refCode).ConfigureAwait(false);
            if (result != null && result.Success)
                return Ok(result);
            else
                return NotFound();
        }

        /// <summary>
        /// Posts the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] MerchantViewModel model)
        {
            var validationResults = GetValidationResult(model, new MerchantViewModelValidation());
            if (!validationResults.IsValid)
                return BadRequest(BuildValidationErrorMessage(validationResults));

            var merchant = await _queryMerchantSummaryManagerService.GetMerchantDetailsByRefCode(model.RefCode).ConfigureAwait(false);

            if (merchant.Data != null)
            {
                return BadRequest("Merchant with " + model.RefCode + "already exist.");
            }
            var results = await _commandCreateMerchantsManagerService.CreateMerchants(model).ConfigureAwait(false);
            if (!results.Success)
                return BadRequest(results.ErrorMessages);
            if (results != null && results.Success)
            {
                await _commandActivityManagerService.ActivityLogger(
                   Manager.Helpers.UtilityHelper.BuildActivityLogger<MerchantViewModel, MerchantViewModel>(ActionConstHelper.CREATE_MERCHANT, merchant.Data, model, model.ModifiedBy));
                return Ok(results);
            }
            return BadRequest(results.ErrorMessages);
        }

        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPut("{refCode}")]
        public async Task<ActionResult> Put(string refCode, [FromBody] MerchantViewModel model)
        {
            var validationResults = GetValidationResult(model, new MerchantViewModelValidation());
            if (!validationResults.IsValid)
                return BadRequest(BuildValidationErrorMessage(validationResults));

            var merchant = await _queryMerchantSummaryManagerService.GetMerchantDetailsByRefCode(refCode).ConfigureAwait(false);

            if (merchant.Data == null)
            {
                return BadRequest("Merchant with " + refCode + " not exist.");
            }

            model.RefCode = refCode;
            var results = await _commandCreateMerchantsManagerService.UpdateMerchants(model).ConfigureAwait(false);
            if (!results.Success)
                return BadRequest(results.ErrorMessages);
            if (results != null && results.Success)
            {
                await _commandActivityManagerService.ActivityLogger(
                    Manager.Helpers.UtilityHelper.BuildActivityLogger<MerchantViewModel, MerchantViewModel>(ActionConstHelper.UPDATE_MERCHANT_DETAILS, merchant.Data, model, model.ModifiedBy));
                return Ok(results);
            }
            return BadRequest(results.ErrorMessages);
        }

        /// <summary>
        /// Updates the cash limit.
        /// </summary>
        /// </summary>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPut("merchantSetup/{refCode}")]
        public async Task<ActionResult> Put(string refCode, [FromBody] MerchantSetupViewModel model)
        {
            var validationResults = GetValidationResult(model, new MerchantSetupViewModelValidation());
            if (!validationResults.IsValid)
                return BadRequest(BuildValidationErrorMessage(validationResults));

            var merchant = await _queryMerchantSummaryManagerService.GetMerchantDetailsByRefCode(refCode).ConfigureAwait(false);

            if (merchant.Data == null)
            {
                return BadRequest("Merchant with " + refCode + "not exist.");
            }
            model.RefCode = refCode;
            var results = await _commandMerchantSetupManagerService.UpdateMerchantSetups(model).ConfigureAwait(false);
            if (!results.Success)
                return BadRequest(results.ErrorMessages);
            if (results != null && results.Success)
            {
                await _commandActivityManagerService.ActivityLogger(
                    Manager.Helpers.UtilityHelper.BuildActivityLogger<MerchantViewModel, MerchantSetupViewModel>(ActionConstHelper.UPDATE_MERCHANT_SETUP, merchant.Data, model, model.ModifiedBy));
                return Ok(results);
            }
            return BadRequest(results.ErrorMessages);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="refCode">The reference code.</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult> Delete(string refCode)
        {
            var merchant = await _queryMerchantSummaryManagerService.GetMerchantDetailsByRefCode(refCode).ConfigureAwait(false);

            if (merchant.Data == null)
            {
                return BadRequest("Merchant with " + refCode + "not exist.");
            }

            var results = await _commandCreateMerchantsManagerService.DeleteMerchants(refCode).ConfigureAwait(false);
            if (!results.Success)
                return BadRequest(results.ErrorMessages);
            if (results != null && results.Success)
            {
                return Ok(results);
            }
            return BadRequest(results.ErrorMessages);
        }

        /// <summary>
        ///Merchant Bulk Upload.
        /// </summary>
        /// <param name="uploadFilePath">The upload file path.</param>
        /// <returns></returns>
        [HttpPost("upload")]
        public async Task<ActionResult> Post(string uploadFilePath)
        {
            var results = await _queryMerchantBulkUploadService.MerchantBulkUpload(uploadFilePath).ConfigureAwait(false);

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