using AutoMapper;
using Contesto.V2.Core.Common.Manager.Base;
using Contesto.V2.Core.Common.Manager.Helpers;
using Contesto.V2.Core.Common.Manager.Results;
using FinoBank.Cola.Manager.Helpers;
using FinoBank.Cola.Manager.Interfaces;
using FinoBank.Cola.Manager.ViewModels;
using FinoBank.Cola.Repository.DomainModels;
using FinoBank.Cola.Repository.Uom.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinoBank.Cola.Manager.Queries
{
    /// <summary>
    /// Query OTP ManagerService
    /// </summary>
    /// <seealso cref="Contesto.V2.Core.Common.Manager.Base.BaseManager" />
    /// <seealso cref="FinoBank.Cola.Manager.Interfaces.IQueryOTPManagerService" />
    public class QueryOTPManagerService : BaseManager, IQueryOTPManagerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IQueryTransactionSummaryManagerService _queryTransactionSummaryManagerService;
        private readonly IConfigurationSettingFromCacheHelper _configurationSettingFromCacheHelper;
        private readonly string CustomerAsReceiverTemplateID;
        private readonly string FeedbackUrlLink;
        private IMapper mapper;
        private IMemoryCache memoryCache;
        private IUnitOfWork unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryOTPManagerService"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public QueryOTPManagerService(IMapper mapper, IMemoryCache cache, IUnitOfWork unitOfWork, IConfigurationSettingFromCacheHelper configurationSettingFromCacheHelper) : base(mapper, null, null)
        {
            _unitOfWork = unitOfWork;
            _configurationSettingFromCacheHelper = configurationSettingFromCacheHelper;
            CustomerAsReceiverTemplateID = _configurationSettingFromCacheHelper.AppSettings("SMS_TEMPLATE_ID_CUSTOMER_AS_RECEIVER");
            FeedbackUrlLink = _configurationSettingFromCacheHelper.AppSettings("FEEDBACK_URL");
        }

        /// <summary>
        /// Gets the merchants with paging.
        /// </summary>
        /// <param name="serviceURL"></param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<OperationResult<GenerateOTPFinalResultViewModel>> GenerateOTP(string serviceURL, GenerateOTPViewModel model)
        {
            var details = MappService.Map<GenerateOTPDomainModel>(model);
            details.RequestId = Manager.Helpers.UtilityHelper.GetReferenceNumber();
            // Map integration properties

            details.MethodId = 1;
            details.SessionId = "";
            details.IsEncrypt = false;

            var requestData = new GenerateRequestDataDomainModel()
            {
                MethodId = "1",
                CustomerMobileNo = model.CustomerMobileNo,
                MessageId = 302,
            };

            var result = await _unitOfWork.QueryGenerateOTPServiceRepository.GenerateOTP(serviceURL, details, requestData).ConfigureAwait(false);
            var jsonModel = JsonConvert.DeserializeObject<GenerateOTPResultViewModel>(result);
            var ResponseValue = JsonConvert.DeserializeObject<GenerateOTPResultViewModel>(jsonModel.ResponseData);
            return ResponseBuilderHelper<GenerateOTPFinalResultViewModel>.Instance.BuildSucessResult(new GenerateOTPFinalResultViewModel { RequestId = ResponseValue.RequestID, TransactionRequestId = jsonModel.RequestID, ResponseCode = jsonModel.ResponseCode });
        }

        /// <summary>
        /// Verifies the otp.
        /// </summary>
        /// <param name="serviceURL">The service URL.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<OperationResult<CommandSuccessStringResultViewModel>> VerifyOTP(string serviceURL, VerifyOTPViewModel model)
        {
            var details = MappService.Map<VerifyOTPDomainModel>(model);

            // Map integration properties
            details.MethodId = 1161;
            details.SessionId = "";
            details.IsEncrypt = false;

            var requestData = new VerifyOTPRequestDataDomainModel()
            {
                MethodId = "2",
                CustomerMobileNo = model.CustomerMobileNo,
                MessageId = 1,
                OtpPin = model.OtpPin,
                RequestId = model.RequestId
            };

            var result = await _unitOfWork.QueryGenerateOTPServiceRepository.VerifyOTP(serviceURL, details, requestData).ConfigureAwait(false);
            return ResponseBuilderHelper<CommandSuccessStringResultViewModel>.Instance.BuildSucessResult(new CommandSuccessStringResultViewModel() { ResponseValue = result });
        }

        /// <summary>
        /// Verifies the otp.
        /// </summary>
        /// <param name="serviceURL">The service URL.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<OperationResult<CommandSuccessBoolResultViewModel>> SendSMS(string serviceURL, long transactionId, SMSRequestViewModel models, string templateId)
        {
            if(templateId != "1173")
            {
                var result = await _unitOfWork.QueryTransactionResultRepository.GetAllMobileNoByTransactionId(transactionId).ConfigureAwait(false);
                var details = MappService.Map<SMSRequestDomainModel>(models);
                details.RequestId = transactionId.ToString();
                var RequestData = new SMSRequestDataDomainModel()
                {
                    //CustomerMobileNo = (templateId == "1166"
                    //|| templateId == "1167" || templateId == "1169"
                    //|| templateId == "1170" || templateId == "1172"
                    //|| templateId == "1174" || templateId == "1176 "
                    //|| templateId == "1177" || templateId == "1178"
                    //|| templateId == "1179" || templateId == "1180" 
                    //|| templateId == "1183" || templateId == "1184"
                    //|| templateId == "1187" || templateId == "1188")
                    //? result.Item1.CustomerMobile : result.Item1.MerchantMobile,
                    CustomerMobileNo = CustomerAsReceiverTemplateID.Contains(templateId) ? result.Item1.CustomerMobile : result.Item1.MerchantMobile,
                    EventId = "",
                    NotifyParam = new ParamDomainModel()
                    {
                        TemplateId = templateId,
                        @ParamID = result.Item1.ReferenceNumber,
                        @Reason = result.Item1.Remarks,
                        @MobileNum = this.GetMobileNumber(templateId, result.Item1),
                        @CaseDecision = string.Concat(FeedbackUrlLink, result.Item1.UniqueId.ToString()),
                        @Amount = result.Item1.ActualAmount.ToString()
                        //(templateId == "1167" || templateId == "1170" || templateId == "1174") ? result.Item1.MerchantMobile : result.Item1.CustomerMobile
                    }
                };

               // var results = 
                await _unitOfWork.QueryGenerateOTPServiceRepository.SendSMS(serviceURL, details, RequestData).ConfigureAwait(false);
                return ResponseBuilderHelper<CommandSuccessBoolResultViewModel>.Instance.BuildSucessResult(new CommandSuccessBoolResultViewModel() { ResponseValue = true });
            }
            else
            {
                var result = await _unitOfWork.QueryTransactionResultRepository.GetAllMobileNoForAbove10K(transactionId).ConfigureAwait(false);
                foreach( var resultData in result)
                {
                    var details = MappService.Map<SMSRequestDomainModel>(models);
                    details.RequestId = transactionId.ToString();
                    var RequestData = new SMSRequestDataDomainModel()
                    {
                        CustomerMobileNo = resultData.MerchantMobile,
                        EventId = "",
                        NotifyParam = new ParamDomainModel()
                        {
                            TemplateId = templateId,
                            @ParamID = resultData.ReferenceNumber,
                            @Reason = resultData.Remarks,
                            @MobileNum = this.GetMobileNumber(templateId, resultData),
                            @CaseDecision = string.Concat(FeedbackUrlLink, resultData.UniqueId.ToString()),
                            @Amount = resultData.ActualAmount.ToString()
                        }
                    };
                    //var results = 
                    await _unitOfWork.QueryGenerateOTPServiceRepository.SendSMS(serviceURL, details, RequestData).ConfigureAwait(false);
                }
                return ResponseBuilderHelper<CommandSuccessBoolResultViewModel>.Instance.BuildSucessResult(new CommandSuccessBoolResultViewModel() { ResponseValue = true });
            }  
        }
        private string GetMobileNumber(string templateId, TransactionRequestsDomainModel item)
        {
            List<string> templateList = new List<string>();
            templateList.Add("1167");
            templateList.Add("1170");
            templateList.Add("1174");
            if (templateList.Contains(templateId))
            {
                return item.MerchantMobile;
            }
            return item.CustomerMobile;
        }
    }
}

