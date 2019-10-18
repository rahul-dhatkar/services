using AutoMapper;
using Contesto.V2.Core.Common.Manager.Base;
using Contesto.V2.Core.Common.Manager.Helpers;
using Contesto.V2.Core.Common.Manager.Results;
using FinoBank.Cola.Manager.Interfaces;
using FinoBank.Cola.Repository.DomainModels;
using FinoBank.Cola.Repository.Uom.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;

namespace FinoBank.Cola.Manager.Queries
{
    public class QueryCustomerSummaryManagerService : BaseManager, IQueryCustomerSummaryManagerService
    {
        #region "Variables"

        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        #endregion "Variables"

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryTransactionSummaryManagerService" /> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="cache">The cache.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public QueryCustomerSummaryManagerService(IMapper mapper, IMemoryCache cache, IUnitOfWork unitOfWork) : base(mapper, cache, null)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<CustomerDomainModel>> GetCustomerDetailsByMobileNumber(string mobileNumber)
        {
            var dbResults = await _unitOfWork.QueryCustomerSummaryRepository.GetCustomerDetailsByMobileNumber(mobileNumber).ConfigureAwait(false);
            return ResponseBuilderHelper<CustomerDomainModel>.Instance.BuildSucessResult(MappService.Map<CustomerDomainModel>(dbResults.Item1));
        }
    }
}