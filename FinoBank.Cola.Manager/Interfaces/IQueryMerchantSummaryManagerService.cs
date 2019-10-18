using Contesto.V2.Core.Common.Manager.Results;
using FinoBank.Cola.Manager.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinoBank.Cola.Manager.Interfaces
{
    public interface IQueryMerchantSummaryManagerService
    {
        /// <summary>
        /// Gets all merchant data.
        /// </summary>
        /// <param name="searchText">The search text.</param>
        /// <returns></returns>
        Task<OperationResult<List<MerchantViewModel>>> GetAllMerchantData(string searchText = null);

        Task<OperationResult<MerchantViewModel>> GetMerchantDetailsByRefCode(string refCode);
    }
}