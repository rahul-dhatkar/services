using Contesto.V2.Core.Common.Manager.Results;
using FinoBank.Cola.Manager.ViewModels;
using System.Threading.Tasks;

namespace FinoBank.Cola.Manager.Interfaces
{
    /// <summary>
    ///
    /// </summary>
    public interface ICommandCreateMerchantsManagerService
    {
        /// <summary>
        /// Creates the master.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<OperationResult<CommandSuccessResultViewModel>> CreateMerchants(MerchantViewModel model);

        /// <summary>
        /// Updates the master.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<OperationResult<CommandSuccessResultViewModel>> UpdateMerchants(MerchantViewModel model);

        /// <summary>
        /// Updates the master.
        /// </summary>
        /// <param name="refCode">The reference code.</param>
        /// <returns></returns>
        Task<OperationResult<CommandSuccessBoolResultViewModel>> DeleteMerchants(string refCode);
    }
}