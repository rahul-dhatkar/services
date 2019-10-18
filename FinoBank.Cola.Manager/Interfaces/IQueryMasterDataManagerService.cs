using Contesto.V2.Core.Common.Manager.Results;
using FinoBank.Cola.Manager.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinoBank.Cola.Manager.Interfaces
{
    /// <summary>
    ///
    /// </summary>
    public interface IQueryMasterDataManagerService
    {
        /// <summary>
        /// Gets the withdrawal type master.
        /// </summary>
        /// <returns></returns>
        Task<OperationResult<List<WithdrawalTypeViewModel>>> GetWithdrawalTypeMaster();

        Task<OperationResult<List<TransactionTypeViewModel>>> GetTransactionTypeMaster();

        Task<OperationResult<List<TransactionStatusViewModel>>> GetTransactionStatusMaster();

        Task<OperationResult<List<MerchantTypeViewModel>>> GetMerchantTypeMaster();
    }
}