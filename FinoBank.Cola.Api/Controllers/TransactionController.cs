using Contesto.V2.Core.Common.Api.Base;
using Contesto.V2.Core.Common.Manager.Helpers;
using Contesto.V2.Core.Common.Utility.Models;
using FinoBank.Cola.Manager.Helpers;
using FinoBank.Cola.Manager.Interfaces;
using FinoBank.Cola.Manager.ViewModels;
using FinoBank.Cola.Manager.ViewModelValidators;
using FinoBank.Cola.Repository.Interfaces;
using FinoBank.Cola.Repository.Uom.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinoBank.Cola.Web.Api.Controllers
{
    /// <summary>
    /// Transaction Request
    /// </summary>
    /// <seealso cref="Contesto.V2.Core.Common.Api.Base.BaseApiController" />
    /// <seealso cref="BaseApiController" />
    [Route("api/v1/transactionHistory")]
    public class TransactionController : BaseApiController
    {
        /// <summary>
        /// The query merchant search manager service
        /// </summary>
        private readonly IQueryTransactionResultRepository _queryTransactionResultRepository;
        /// <summary>
        /// The query merchant search manager service
        /// </summary>
        private readonly IQueryTransactionSummaryManagerService _queryTransactionSummaryManagerService;

        /// <summary>
        /// The query customer summary manager service
        /// </summary>
        private readonly IQueryCustomerSummaryManagerService _queryCustomerSummaryManagerService;

        /// <summary>
        /// The query accept transaction request manager service
        /// </summary>
        private readonly IQueryAcceptTransactionRequestManagerService _queryAcceptTransactionRequestManagerService;

        /// <summary>
        /// The query otp manager service
        /// </summary>
        private readonly IQueryOTPManagerService _queryOTPManagerService;

        private readonly IQueryMerchantSearchManagerService _queryMerchantSearchManagerService;

        private readonly ICommandSMSlogManagerService _commandSMSlogManagerService;

        /// <summary>
        /// The command transaction requests manager service
        /// </summary>
        private readonly ICommandTransactionRequestsManagerService _commandTransactionRequestsManagerService;

        /// <summary>
        /// The command transaction feedbacks manager service
        /// </summary>
        private readonly ICommandTransactionFeedbacksManagerService _commandTransactionFeedbacksManagerService;

        /// <summary>
        /// The command update transaction requests manager service
        /// </summary>
        private readonly ICommandUpdateTransactionRequestsManagerService _commandUpdateTransactionRequestsManagerService;

        /// <summary>
        /// The command activity manager service
        /// </summary>
        private readonly ICommandActivityManagerService _commandActivityManagerService;

        /// <summary>
        /// The configuration setting from cache helper
        /// </summary>
        private readonly IConfigurationSettingFromCacheHelper _configurationSettingFromCacheHelper;

        /// <summary>
        /// The service URL
        /// </summary>
        private readonly string serviceUrl;

        private readonly string withdrawalLimit;

        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchController" /> class.
        /// </summary>
        /// <param name="queryTransactionSummaryManagerService">The query transaction summary manager service.</param>
        /// <param name="commandTransactionRequestsManagerService">The command transaction requests manager service.</param>
        /// <param name="commandTransactionFeedbacksManagerService">The command transaction feedbacks manager service.</param>
        /// <param name="commandUpdateTransactionRequestsManagerService">The command update transaction requests manager service.</param>
        public TransactionController(IQueryTransactionSummaryManagerService queryTransactionSummaryManagerService,
            IQueryCustomerSummaryManagerService queryCustomerSummaryManagerService,
           IQueryAcceptTransactionRequestManagerService queryAcceptTransactionRequestManagerService,
           IQueryOTPManagerService queryOTPManagerService,
           IQueryMerchantSearchManagerService queryMerchantSearchManagerService,
           ICommandSMSlogManagerService commandSMSlogManagerService,
        ICommandTransactionRequestsManagerService commandTransactionRequestsManagerService,
            ICommandTransactionFeedbacksManagerService commandTransactionFeedbacksManagerService,
            ICommandUpdateTransactionRequestsManagerService commandUpdateTransactionRequestsManagerService,
            ICommandActivityManagerService commandActivityManagerService,
            IConfigurationSettingFromCacheHelper configurationSettingFromCacheHelper
            )
        {
            _queryCustomerSummaryManagerService = queryCustomerSummaryManagerService;
            _queryTransactionSummaryManagerService = queryTransactionSummaryManagerService;
            _queryAcceptTransactionRequestManagerService = queryAcceptTransactionRequestManagerService;
            _queryMerchantSearchManagerService = queryMerchantSearchManagerService;
            _commandTransactionRequestsManagerService = commandTransactionRequestsManagerService;
            _commandTransactionFeedbacksManagerService = commandTransactionFeedbacksManagerService;
            _commandUpdateTransactionRequestsManagerService = commandUpdateTransactionRequestsManagerService;
            _commandTransactionRequestsManagerService = commandTransactionRequestsManagerService;
            _commandActivityManagerService = commandActivityManagerService;
            _commandSMSlogManagerService = commandSMSlogManagerService;
            _queryOTPManagerService = queryOTPManagerService;
            _configurationSettingFromCacheHelper = configurationSettingFromCacheHelper;
            serviceUrl = _configurationSettingFromCacheHelper.AppSettings("INTEGRATION_OTP_SERVICEURL");
            withdrawalLimit = _configurationSettingFromCacheHelper.AppSettings("WITHDRAWAL_LIMIT");
        }


        [HttpGet("get/{transactionId}")]
        public async Task<ActionResult> GetTransactionDetailsById(long transactionId)
        {
            var result = await _queryTransactionSummaryManagerService.GetTransactionDetailsById(transactionId).ConfigureAwait(false);
            if (result != null && result.Success)
                return Ok(result);
            else
                return NotFound();
        }
        /// <summary>
        /// Gets the customer all transaction request history.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpGet("customer/{transactionStatusId}/getAll")]
        public async Task<ActionResult> GetCustomerAllTransactionRequestHistory([FromQuery] CustomerTransactionSummaryRequestViewModel model)
        {
            var validationResults = GetValidationResult(model, new CustomerTransactionSummaryRequestViewModelValidation());
            if (!validationResults.IsValid)
                return BadRequest(BuildValidationErrorMessage(validationResults));
            var result = await _queryTransactionSummaryManagerService.GetCustomerTransactionSummaryDataWithPaging(model).ConfigureAwait(false); 
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

        /// <summary>
        /// Gets the merchant all transaction request history.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpGet("merchant/{transactionStatusId}/getAll")]
        public async Task<ActionResult> GetMerchantAllTransactionRequestHistory([FromQuery] MerchantTransactionSummaryRequestViewModel model)
        {
            var validationResults = GetValidationResult(model, new MerchantTransactionSummaryRequestViewModelValidation());
            if (!validationResults.IsValid)
                return BadRequest(BuildValidationErrorMessage(validationResults));
            var result = await _queryTransactionSummaryManagerService.GetMerchantTransactionSummaryDataWithPaging(model).ConfigureAwait(false);
            if (result.Success)
            {
                if (result.Data.Result.Any())
                    return Ok(result);
                else
                {
                    return BadRequest(ResponseBuilderHelper<MerchantTransactionSummaryRequestViewModel>.Instance.BuildUnSucessResult(new List<ErrorModel>()
                    {
                        new ErrorModel()
                        {
                            Message = "No records found."
                        }
                    }));
                }
            }
            return BadRequest(result.ErrorMessages);
        }

        /// <summary>
        /// Posts the specified mobile number.
        /// </summary>
        /// <param name="mobileNumber">The mobile number.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost("customer/newRequest/{mobileNumber}")]
        public async Task<ActionResult> Post(string mobileNumber, [FromBody] TransactionViewModel model)
        {
            if (mobileNumber != null)
            {
                var customer = await _queryCustomerSummaryManagerService.GetCustomerDetailsByMobileNumber(mobileNumber).ConfigureAwait(false);

                if (customer.Data != null)
                {
                    model.CustomerId = customer.Data.Id;

                    var result = await _commandTransactionRequestsManagerService.CreateTransactionRequests(model).ConfigureAwait(false);

                    if(model.RequestedAmount > Convert.ToDouble(withdrawalLimit) & model.TransactionTypeId == 2)
                    {
                        MerchantSearchRequestViewModel searchModel = new MerchantSearchRequestViewModel();
                        searchModel.CurrentLatitude = Convert.ToDouble(model.CustomerLatitude);
                        searchModel.CurrentLongitude = Convert.ToDouble(model.CustomerLongitude);
                        searchModel.ByTransactionTypeId = model.TransactionTypeId;
                        searchModel.Amount = Convert.ToInt32(model.ActualAmount);
                        searchModel.CustomerRefCode = customer.Data.RefCode;
                        searchModel.CustomerType = model.CustomerType;
                        searchModel.CustomerMobile = customer.Data.Mobile;
                        searchModel.ByWithdrawalTypeId = Convert.ToInt32(model.WithdrawalTypeId);

                        var searchResults = await _queryMerchantSearchManagerService.GetMerchantSearchDataWithPagingAsync(searchModel).ConfigureAwait(false);
                        if (searchResults.Data.TotalCount > 0)
                        {
                            foreach (var searchdata in searchResults.Data.Result)
                            {
                                SMSlogViewModel smslogViewModel = new SMSlogViewModel();
                                smslogViewModel.TransactionId = result.Data.Id;
                                smslogViewModel.MerchantId = Convert.ToInt32(searchdata.Id);
                                var records = await _commandSMSlogManagerService.CreateSMSlogs(smslogViewModel).ConfigureAwait(false);
                            }

                            SMSRequestViewModel models = new SMSRequestViewModel();
                            await _queryOTPManagerService.SendSMS(serviceUrl, result.Data.Id, models, Manager.Helpers.TemplateConstHelper.MERCHANT_ACCEPT_AND_PROCESS_TRANSACTION).ConfigureAwait(false);
                            return Ok(result);
                        }
                        else
                        {
                            return Ok(ResponseBuilderHelper<TransactionViewModel>.Instance.BuildUnSucessResult(new List<ErrorModel>()
                            {
                                new ErrorModel()
                                {
                                    Message = "No Merchant Found."
                                }
                            }));
                        }
                    }
                    else
                    {
                        if (!result.Success)
                            return BadRequest(result.ErrorMessages);
                        if (result != null && result.Success)
                        {
                            await _commandActivityManagerService.ActivityLogger
                               (
                                 Manager.Helpers.UtilityHelper.BuildActivityLogger<TransactionViewModel, TransactionViewModel>(ActionConstHelper.CREATE_TRANSACTION_REQUEST, null, model, model.ModifiedBy != null ? model.ModifiedBy : "Customer")
                               );
                            // Cash Deposit - Customer 
                            if (result.Data.TransactionTypeId == 1)
                            {
                                SMSRequestViewModel models = new SMSRequestViewModel();
                                await _queryOTPManagerService.SendSMS(serviceUrl, result.Data.Id, models, Manager.Helpers.TemplateConstHelper.CUSTOMER_CASH_DEPOSIT_RECEIVED).ConfigureAwait(false);
                                await _queryOTPManagerService.SendSMS(serviceUrl, result.Data.Id, models, Manager.Helpers.TemplateConstHelper.MERCHANT_CASH_DEPOSIT_RECEIVED).ConfigureAwait(false);
                            }
                            // Cash Withdrawal - Customer 
                            else if (result.Data.TransactionTypeId == 2)
                            {
                                SMSRequestViewModel models = new SMSRequestViewModel();
                                await _queryOTPManagerService.SendSMS(serviceUrl, result.Data.Id, models, Manager.Helpers.TemplateConstHelper.CUSTOMER_CASH_WITHDRAWAL_REQUEST).ConfigureAwait(false);
                                await _queryOTPManagerService.SendSMS(serviceUrl, result.Data.Id, models, Manager.Helpers.TemplateConstHelper.MERCHANT_CASH_WITHDRAWAL_REQUEST).ConfigureAwait(false);
                            }
                            return Ok(result);
                        }
                        return BadRequest(result.ErrorMessages);
                    }
                }
                return BadRequest("Customer not exist.");
            }
            return BadRequest("Mobile number is mandatory.");
        }

        /// <summary>
        /// Posts the specified mobile number.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost("customer/feedback/{transactionId}")]
        public async Task<ActionResult> Post([FromBody] TransactionFeedbackViewModel model)
        {
            var results = await _commandTransactionFeedbacksManagerService.CreateTransactionFeedbacks(model).ConfigureAwait(false);
            if (!results.Success)
                return BadRequest(results.ErrorMessages);
            if (results != null && results.Success)
            {
                await _commandActivityManagerService.ActivityLogger(Manager.Helpers.UtilityHelper.BuildActivityLogger<TransactionFeedbackViewModel, TransactionFeedbackViewModel>
                    (ActionConstHelper.CREATE_CUSTOMER_FEEDBACK, null, model, model.CustomerId.ToString()));
                return Ok(results);
            }
            return BadRequest(results.ErrorMessages);
        }

        /// <summary>
        /// Puts the specified transaction request identifier.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPut("merchant/requestStatusUpdate/{transactionId}")]
        public async Task<ActionResult> Put(long transactionId, [FromBody] TransactionStatusUpdateViewModel model)
         {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var results = await _commandUpdateTransactionRequestsManagerService.UpdateTransactionRequests(model).ConfigureAwait(false);
            if (!results.Success)
                return BadRequest(results.ErrorMessages);

            if (results != null && results.Success)
            {
                await _commandActivityManagerService.ActivityLogger
                (
                Manager.Helpers.UtilityHelper.BuildActivityLogger<TransactionStatusUpdateViewModel, TransactionStatusUpdateViewModel>(ActionConstHelper.UPDATE_MERCHANT_REQUEST_STATUS, null, model, "Merchant")
                );

                var customerResultData = await _queryTransactionSummaryManagerService.GetAllMobileNoByTransactionId(transactionId).ConfigureAwait(false);
                // Cancelled Transaction - Cancellation by merchant - Customer SMS on Deposit
                if (model.TransactionNewStatusId == 3 && customerResultData.Data.TransactionType == "Deposit")
                {
                    SMSRequestViewModel models = new SMSRequestViewModel();
                    await _queryOTPManagerService.SendSMS(serviceUrl, transactionId, models, Manager.Helpers.TemplateConstHelper.CASH_DEPOSIT_CANCELLED_MERCHANT_REQUEST).ConfigureAwait(false);
                    await _queryOTPManagerService.SendSMS(serviceUrl, transactionId, models, Manager.Helpers.TemplateConstHelper.MERCHANT_CASH_DEPOSIT_CANCELLED_MERCHANT_REQUEST).ConfigureAwait(false);
                }
                // Cancelled Transaction - Cancellation by merchant  - Customer SMS on Withdrawal
                else if (model.TransactionNewStatusId == 3 && customerResultData.Data.TransactionType == "Withdrawal")
                {
                    SMSRequestViewModel models = new SMSRequestViewModel();
                    await _queryOTPManagerService.SendSMS(serviceUrl, transactionId, models, Manager.Helpers.TemplateConstHelper.CASH_WITHDRAWAL_CANCELLED_MERCHANT_REQUEST).ConfigureAwait(false);
                    await _queryOTPManagerService.SendSMS(serviceUrl, transactionId, models, Manager.Helpers.TemplateConstHelper.MERCHANT_CASH_WITHDRAWAL_CANCELLED_MERCHANT_REQUEST).ConfigureAwait(false);
                }

                return Ok(results);
            }
            return BadRequest(results.ErrorMessages);
        }

        /// <summary>
        /// Puts the specified transaction request identifier.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPut("customer/requestStatusUpdate/{transactionId}")]
        public async Task<ActionResult> CustomerStatusUpdate(long transactionId, [FromBody] TransactionStatusUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var results = await _commandUpdateTransactionRequestsManagerService.UpdateTransactionRequests(model).ConfigureAwait(false);
            if (!results.Success)
                return BadRequest(results.ErrorMessages);

            if (results != null && results.Success)
            {
                await _commandActivityManagerService.ActivityLogger
               (
               Manager.Helpers.UtilityHelper.BuildActivityLogger<TransactionStatusUpdateViewModel, TransactionStatusUpdateViewModel>(ActionConstHelper.UPDATE_CUSTOMER_REQUEST_STATUS, null, model, "Customer")
               );
                var customerResultData = await _queryTransactionSummaryManagerService.GetAllMobileNoByTransactionId(transactionId).ConfigureAwait(false);
                //Completed Transaction - Customer SMS on Deposit
                if (model.TransactionNewStatusId == 2 && customerResultData.Data.TransactionType == "Deposit")
                {
                    SMSRequestViewModel models = new SMSRequestViewModel();
                    await _queryOTPManagerService.SendSMS(serviceUrl, transactionId, models, Manager.Helpers.TemplateConstHelper.CUSTOMER_COMPLETED_TRANSACTION_DEPOSIT).ConfigureAwait(false);
                }
                //Completed Transaction - Customer SMS on Withdrawal
                else if (model.TransactionNewStatusId == 2 && customerResultData.Data.TransactionType == "Withdrawal")
                {
                    SMSRequestViewModel models = new SMSRequestViewModel();
                    await _queryOTPManagerService.SendSMS(serviceUrl, transactionId, models, Manager.Helpers.TemplateConstHelper.CUSTOMER_COMPLETED_TRANSACTION_WITHDRAWAL).ConfigureAwait(false);
                }
                //Cancelled Transaction - Customer SMS on Deposit
                else if (model.TransactionNewStatusId == 3 && customerResultData.Data.TransactionType == "Deposit")
                {
                    SMSRequestViewModel models = new SMSRequestViewModel();
                    await _queryOTPManagerService.SendSMS(serviceUrl, transactionId, models, Manager.Helpers.TemplateConstHelper.CASH_DEPOSIT_CANCELLED_CUSTOMER_REQUEST).ConfigureAwait(false);
                    await _queryOTPManagerService.SendSMS(serviceUrl, transactionId, models, Manager.Helpers.TemplateConstHelper.MERCHANT_CASH_DEPOSIT_CANCELLED_CUSTOMER_REQUEST).ConfigureAwait(false);

                }
                //Cancelled Transaction - Customer SMS on Withdrawal
                else if (model.TransactionNewStatusId == 3 && customerResultData.Data.TransactionType == "Withdrawal")
                {
                    SMSRequestViewModel models = new SMSRequestViewModel();
                    await _queryOTPManagerService.SendSMS(serviceUrl, transactionId, models, Manager.Helpers.TemplateConstHelper.CASH_WITHDRAWAL_CANCELLED_CUSTOMER_REQUEST).ConfigureAwait(false);
                    await _queryOTPManagerService.SendSMS(serviceUrl, transactionId, models, Manager.Helpers.TemplateConstHelper.MERCHANT_CASH_WITHDRAWAL_CANCELLED_CUSTOMER_REQUEST).ConfigureAwait(false);
                }
                return Ok(results);
            }
            return BadRequest(results.ErrorMessages);
        }

        /// <summary>
        /// Puts the specified transaction request identifier.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPut("merchant/acceptTransactionRequest/{transactionId}")]
        public async Task<ActionResult> Put(long transactionId, [FromBody] AcceptTransactionRequestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var results = await _queryAcceptTransactionRequestManagerService.AcceptTransactionRequest(model).ConfigureAwait(false);
            var customerResultData = await _queryTransactionSummaryManagerService.GetAllMobileNoByTransactionId(transactionId).ConfigureAwait(false);
            if(customerResultData.Data.TransactionType == "Withdrawal")
            {
                SMSRequestViewModel models = new SMSRequestViewModel();
                await _queryOTPManagerService.SendSMS(serviceUrl, transactionId, models, Manager.Helpers.TemplateConstHelper.CUSTOMER_WITHDRAWAL_RECEIVED).ConfigureAwait(false);
                await _queryOTPManagerService.SendSMS(serviceUrl, transactionId, models, Manager.Helpers.TemplateConstHelper.MERCHANT_CUSTOMER_WITHDRAWAL_RECEIVED).ConfigureAwait(false);
            }
            if (results.Data.ResponseValue)
            {
                return Ok(results);
            }
            return BadRequest(results.ErrorMessages);
        }
    }
}



