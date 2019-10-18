using AutoMapper;
using Contesto.V2.Core.Common.Manager.Base;
using Contesto.V2.Core.Common.Manager.Helpers;
using Contesto.V2.Core.Common.Manager.Results;
using FinoBank.Cola.Manager.Helpers;
using FinoBank.Cola.Manager.Interfaces;
using FinoBank.Cola.Manager.ViewModels;
using FinoBank.Cola.Repository.Uom.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinoBank.Cola.Manager.Queries
{
    /// <summary>
    /// Check ForTransaction Request Expiration ManagerService
    /// </summary>
    /// <seealso cref="Contesto.V2.Core.Common.Manager.Base.BaseManager" />
    /// <seealso cref="FinoBank.Cola.Manager.Interfaces.IQueryCheckForTransactionRequestExpirationManagerService" />
    public class QueryCheckForTransactionRequestExpirationManagerService : BaseManager, IQueryCheckForTransactionRequestExpirationManagerService
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;
        private readonly string serviceUrl;
        private readonly IQueryOTPManagerService _queryOTPManagerService;
        /// <summary>
        /// The configuration setting from cache helper
        /// </summary>
        private readonly IConfigurationSettingFromCacheHelper _configurationSettingFromCacheHelper;
        private IMapper mapper;
        private IUnitOfWork unitOfWork;
        private IQueryOTPManagerService queryOTPManagerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryCheckForTransactionRequestExpirationManagerService" /> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public QueryCheckForTransactionRequestExpirationManagerService(IMapper mapper, IUnitOfWork unitOfWork, IQueryOTPManagerService queryOTPManagerService,
             IConfigurationSettingFromCacheHelper configurationSettingFromCacheHelper) : base(mapper, null, null)
        {
            this.mapper = mapper;
            _unitOfWork = unitOfWork;
            _queryOTPManagerService = queryOTPManagerService;
            _configurationSettingFromCacheHelper = configurationSettingFromCacheHelper;
            serviceUrl = _configurationSettingFromCacheHelper.AppSettings("INTEGRATION_OTP_SERVICEURL");
        }

        /// <summary>
        /// Checks for transaction request expiration.
        /// </summary>
        /// <param name="timeStamp">The time stamp.</param>
        /// <returns></returns>
        public async Task<OperationResult<List<TransactionViewModel>>> CheckForTransactionRequestExpiration(int timeStamp)
        {
            var dbResults = await _unitOfWork.QueryCheckForTransactionRequestExpirationRepository.CheckForTransactionRequestExpiration(timeStamp).ConfigureAwait(false);
            foreach (var record in dbResults)
            {
                var customerResultData = await _unitOfWork.QueryTransactionResultRepository.GetAllMobileNoByTransactionId(record.Id).ConfigureAwait(false);
                
              
                    if (customerResultData.Item1.TransactionType == "Deposit")
                    {
                        SMSRequestViewModel models = new SMSRequestViewModel();
                        await _queryOTPManagerService.SendSMS(serviceUrl, record.Id, models, Manager.Helpers.TemplateConstHelper.CUSTOMER_CASH_DEPOSIT_EXPIRE).ConfigureAwait(false);
                        await _queryOTPManagerService.SendSMS(serviceUrl, record.Id, models, Manager.Helpers.TemplateConstHelper.MERCHANT_CASH_DEPOSIT_EXPIRE).ConfigureAwait(false);
                    }
                    else if (customerResultData.Item1.TransactionType == "Withdrawal")
                    {
                        SMSRequestViewModel models = new SMSRequestViewModel();
                        await _queryOTPManagerService.SendSMS(serviceUrl, record.Id, models, Manager.Helpers.TemplateConstHelper.CUSTOMER_CASH_WITHDRAWAL_EXPIRE).ConfigureAwait(false);
                        await _queryOTPManagerService.SendSMS(serviceUrl, record.Id, models, Manager.Helpers.TemplateConstHelper.MERCHANT_CASH_WITHDRAWAL_EXPIRE).ConfigureAwait(false);
                    }
            }
            return ResponseBuilderHelper<List<TransactionViewModel>>.Instance.BuildSucessResult(MappService.Map<List<TransactionViewModel>>(dbResults));
        }
    }
}

