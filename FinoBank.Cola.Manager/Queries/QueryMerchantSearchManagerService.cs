using AutoMapper;
using Contesto.V2.Core.Common.Manager.Base;
using Contesto.V2.Core.Common.Manager.Helpers;
using Contesto.V2.Core.Common.Manager.Results;
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
    /// <seealso cref="FinoBank.Cola.Manager.Interfaces.IQueryMerchantSearchManagerService" />
    public class QueryMerchantSearchManagerService : BaseManager, IQueryMerchantSearchManagerService
    {
        #region "Variables"

        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        #endregion "Variables"

        #region "Constructor"

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryMerchantSearchManagerService" /> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="cache">The cache.</param>
        /// <param name="unitOfWork">The startup kit unit of work.</param>
        public QueryMerchantSearchManagerService(IMapper mapper, IMemoryCache cache, IUnitOfWork unitOfWork) : base(mapper, cache, null)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets the merchants with paging.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<OperationResult<MerchantSearchResultViewModel>> GetMerchantSearchDataWithPagingAsync(MerchantSearchRequestViewModel model)
        {
            var searchResultData = await _unitOfWork.QueryMerchantSearchRepository.GetMerchantSearchDataWithPaging(model.CustomerType, model.CustomerRefCode, model.CustomerMobile, model.Amount, model.CurrentLatitude, model.CurrentLongitude, model.ByMerchantTypeId, model.ByTransactionTypeId, model.ByWithdrawalTypeId, model.Distance, model.SortColumn, model.SortDirection, model.PageIndex, model.PageSize, model.SearchText, model.TotalCount).ConfigureAwait(false);

            var result = new MerchantSearchResultViewModel()
            {
                PageIndex = model.PageIndex,
                PageSize = model.PageSize,
                TotalCount = searchResultData.Item2,
                Result = MappService.Map<List<MerchantViewModel>>(searchResultData.Item1)
            };

            return ResponseBuilderHelper<MerchantSearchResultViewModel>.Instance.BuildSucessResult(result);
        }

        #endregion "Constructor"
    }
}