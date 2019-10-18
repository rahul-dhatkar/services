using Contesto.V2.Core.Common.Manager.Results;
using FinoBank.Cola.Manager.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinoBank.Cola.Manager.Interfaces
{
    /// <summary>
    /// Interface Query StartupKit Manager Service
    /// </summary>
    public interface IQuerySampleManagerService
    {
        /// <summary>
        /// Gets the startup kit by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<OperationResult<SampleViewModel>> GetStartupKitById(int id);

        /// <summary>
        /// Gets the startup kit summary.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<OperationResult<SampleSummaryResultViewModel>> GetStartupKitSummary(SampleSummaryRequestViewModel model);

        /// <summary>
        /// Gets the startup kit summary by type identifier.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<OperationResult<SampleSummaryResultViewModel>> GetStartupKitSummaryByTypeId(SampleSummaryRequestViewModel model);

        /// <summary>
        /// Gets the startup kits.
        /// </summary>
        /// <param name="searchText">The search text.</param>
        /// <returns></returns>
        Task<OperationResult<List<SampleViewModel>>> GetStartupKits(string searchText = null);
    }
}