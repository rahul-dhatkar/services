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
    /// <seealso cref="FinoBank.Cola.Manager.Interfaces.IQueryMerchantSummaryManagerService" />
    public class QueryMerchantSummaryManagerService : BaseManager, IQueryMerchantSummaryManagerService
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryMerchantSummaryManagerService"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="cache">The cache.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public QueryMerchantSummaryManagerService(IMapper mapper, IMemoryCache cache, IUnitOfWork unitOfWork) : base(mapper, cache, null)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets all merchant data.
        /// </summary>
        /// <param name="searchText">The search text.</param>
        /// <returns></returns>
        public async Task<OperationResult<List<MerchantViewModel>>> GetAllMerchantData(string searchText = null)
        {
            var dbResults = await _unitOfWork.QueryMerchantSummaryRepository.GetAllData(searchText).ConfigureAwait(false);
            return ResponseBuilderHelper<List<MerchantViewModel>>.Instance.BuildSucessResult(MappService.Map<List<MerchantViewModel>>(dbResults));
        }

        public async Task<OperationResult<MerchantViewModel>> GetMerchantDetailsByRefCode(string refCode)
        {
            var dbResults = await _unitOfWork.QueryMerchantSummaryRepository.GetMerchantDetailsByRefCode(refCode).ConfigureAwait(false);
            return ResponseBuilderHelper<MerchantViewModel>.Instance.BuildSucessResult(MappService.Map<MerchantViewModel>(dbResults));
        }
    }
}