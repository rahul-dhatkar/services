using Contesto.V2.Core.Common.Manager.Results;
using FinoBank.Cola.Manager.ViewModels;
using System.Threading.Tasks;

namespace FinoBank.Cola.Manager.Interfaces
{
    /// <summary>
    ///
    /// </summary>
    public interface ICommandTransactionRequestsManagerService
    {
        /// <summary>
        /// Creates the master.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<OperationResult<TransactionViewModel>> CreateTransactionRequests(TransactionViewModel model);

        /// <summary>
        /// Updates the master.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<OperationResult<CommandSuccessResultViewModel>> UpdateTransactionRequests(TransactionStatusUpdateViewModel model);
    }
}