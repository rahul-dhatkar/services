using Contesto.V2.Core.Common.Manager.Results;
using FinoBank.Cola.Manager.ViewModels;
using System.Threading.Tasks;

namespace FinoBank.Cola.Manager.Interfaces
{
    /// <summary>
    /// Command interface for Startup Kit Manager Service
    /// </summary>
    public interface ICommandSampleManagerService
    {
        /// <summary>
        /// Creates the master.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<OperationResult<CommandSuccessResultViewModel>> CreateStartupKit(SampleViewModel model);

        /// <summary>
        /// Updates the master.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<OperationResult<CommandSuccessResultViewModel>> UpdateStartupKit(SampleViewModel model);
    }
}