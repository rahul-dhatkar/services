using Contesto.V2.Core.Common.Manager.Results;
using FinoBank.Cola.Manager.ViewModels;
using System.Threading.Tasks;

namespace FinoBank.Cola.Manager.Interfaces
{
    /// <summary>
    /// Interface Query Merchant Search Manager Service
    /// </summary>
    public interface IQueryMerchantSearchManagerService
    {
        /// <summary>
        /// Gets the merchants with paging.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<OperationResult<MerchantSearchResultViewModel>> GetMerchantSearchDataWithPagingAsync(MerchantSearchRequestViewModel model);
    }
}