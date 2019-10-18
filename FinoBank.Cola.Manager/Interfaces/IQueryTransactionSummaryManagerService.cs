using Contesto.V2.Core.Common.Manager.Results;
using FinoBank.Cola.Manager.ViewModels;
using System.Threading.Tasks;

namespace FinoBank.Cola.Manager.Interfaces
{
    /// <summary>
    ///
    /// </summary>
    public interface IQueryTransactionSummaryManagerService
    {
        /// <summary>
        /// Gets the customer transaction summary data with paging.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<OperationResult<TransactionSummaryResultViewModel>> GetCustomerTransactionSummaryDataWithPaging(CustomerTransactionSummaryRequestViewModel model);

        /// <summary>
        /// Gets the merchant transaction summary data with paging.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<OperationResult<TransactionSummaryResultViewModel>> GetMerchantTransactionSummaryDataWithPaging(MerchantTransactionSummaryRequestViewModel model);

        Task<OperationResult<MobileNoRequestViewModel>> GetAllMobileNoByTransactionId(long transactionId);

        Task<OperationResult<TransactionViewModel>> GetTransactionDetailsById(long transactionId);

    }
}