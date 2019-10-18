using Contesto.V2.Core.Common.Manager.Results;
using FinoBank.Cola.Manager.ViewModels;
using System.Threading.Tasks;

namespace FinoBank.Cola.Manager.Interfaces
{
    /// <summary>
    ///
    /// </summary>
    public interface ICommandMerchantSetupManagerService
    {
        /// <summary>
        /// Updates the master.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<OperationResult<CommandSuccessStringResultViewModel>> UpdateMerchantSetups(MerchantSetupViewModel model);
    }
}