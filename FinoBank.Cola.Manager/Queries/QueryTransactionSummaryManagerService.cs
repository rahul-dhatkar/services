using AutoMapper;
using Contesto.V2.Core.Common.Manager.Base;
using Contesto.V2.Core.Common.Manager.Helpers;
using Contesto.V2.Core.Common.Manager.Results;
using Contesto.V2.Core.Common.Utility.Models;
using FinoBank.Cola.Manager.Helpers;
using FinoBank.Cola.Manager.Interfaces;
using FinoBank.Cola.Manager.ViewModels;
using FinoBank.Cola.Repository.Uom.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinoBank.Cola.Manager.Queries
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="Contesto.V2.Core.Common.Manager.Base.BaseManager" />
    /// <seealso cref="FinoBank.Cola.Manager.Interfaces.IQueryTransactionSummaryManagerService" />
    public class QueryTransactionSummaryManagerService : BaseManager, IQueryTransactionSummaryManagerService
    {
        #region "Variables"

        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        #endregion "Variables"

        #region "Constructor"

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryTransactionSummaryManagerService" /> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="cache">The cache.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public QueryTransactionSummaryManagerService(IMapper mapper, IMemoryCache cache, IUnitOfWork unitOfWork) : base(mapper, cache, null)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets the customer transaction summary data with paging.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<OperationResult<TransactionSummaryResultViewModel>> GetCustomerTransactionSummaryDataWithPaging(CustomerTransactionSummaryRequestViewModel model)
        {
            var customerResultData = await _unitOfWork.QueryTransactionSummaryRepository.GetCustomerTransactionSummaryDataWithPaging(model.CustomerType, model.CustomerRefCode, model.CustomerMobile, model.TransactionStatusId, model.FromDate, model.ToDate, model.StatementType, model.SortColumn, model.SortDirection, model.PageIndex, model.PageSize, model.SearchText, model.TotalCount).ConfigureAwait(false);

            var result = new TransactionSummaryResultViewModel()
            {
                PageIndex = model.PageIndex,
                PageSize = model.PageSize,
                TotalCount = customerResultData.Item2,
                Result = MappService.Map<List<TransactionViewModel>>(customerResultData.Item1)
            };

            return ResponseBuilderHelper<TransactionSummaryResultViewModel>.Instance.BuildSucessResult(result);
        }

        /// <summary>
        /// Gets the merchant transaction summary data with paging.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<OperationResult<TransactionSummaryResultViewModel>> GetMerchantTransactionSummaryDataWithPaging(MerchantTransactionSummaryRequestViewModel model)
        {
            var customerResultData = await _unitOfWork.QueryTransactionSummaryRepository.GetMerchantTransactionSummaryDataWithPaging(model.RefCode, model.MerchantId, model.TransactionStatusId, model.FromDate, model.ToDate, model.SortColumn, model.SortDirection, model.PageIndex, model.PageSize, model.SearchText, model.TotalCount).ConfigureAwait(false);

            var result = new TransactionSummaryResultViewModel()
            {
                PageIndex = model.PageIndex,
                PageSize = model.PageSize,
                TotalCount = customerResultData.Item2,
                Result = MappService.Map<List<TransactionViewModel>>(customerResultData.Item1)
            };
            return ResponseBuilderHelper<TransactionSummaryResultViewModel>.Instance.BuildSucessResult(result);
        }

        public async Task<OperationResult<MobileNoRequestViewModel>> GetAllMobileNoByTransactionId(long transactionId)
        {
            var customerResultData = await _unitOfWork.QueryTransactionResultRepository.GetAllMobileNoByTransactionId(transactionId).ConfigureAwait(false);
            return ResponseBuilderHelper<MobileNoRequestViewModel>.Instance.BuildSucessResult(new MobileNoRequestViewModel { TransactionId = transactionId, CustomerID = customerResultData.Item1.CustomerId, CustomerMobile = customerResultData.Item1.CustomerMobile, MerchantId = customerResultData.Item1.MerchantId, MerchantMobile = customerResultData.Item1.MerchantMobile, TransactionType=customerResultData.Item1.TransactionType,ReferenceNumber = customerResultData.Item1.ReferenceNumber, Remarks = customerResultData.Item1.Remarks, UniqueId = customerResultData.Item1.UniqueId });
        }

        public async Task<OperationResult<TransactionViewModel>> GetTransactionDetailsById(long transactionId)
        {
            var customerResultData = await _unitOfWork.QueryTransactionResultRepository.GetTransactionDetailsById(transactionId).ConfigureAwait(false);
            return ResponseBuilderHelper<TransactionViewModel>.Instance.BuildSucessResult(MappService.Map<TransactionViewModel>(customerResultData.Item1));
        }

        #endregion "Constructor"
    }
}