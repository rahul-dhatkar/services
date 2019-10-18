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
    /// <seealso cref="FinoBank.Cola.Manager.Interfaces.IQueryMerchantDataManagerService" />
    public class QueryMerchantSummaryDataManagerService : BaseManager, IQueryMerchantSummaryDataManagerService
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
        public QueryMerchantSummaryDataManagerService(IMapper mapper, IMemoryCache cache, IUnitOfWork unitOfWork) : base(mapper, cache, null)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<OperationResult<MerchantDataViewModel>> GetMerchantDataById(string refCode)
        {
            var dbResults = await _unitOfWork.QueryMerchantDataRepository.GetMerchantDataById(refCode).ConfigureAwait(false);
            return ResponseBuilderHelper<MerchantDataViewModel>.Instance.BuildSucessResult(MappService.Map<MerchantDataViewModel>(dbResults));
        }
    }
}