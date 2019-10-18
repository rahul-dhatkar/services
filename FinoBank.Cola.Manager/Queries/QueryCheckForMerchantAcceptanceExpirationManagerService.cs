using AutoMapper;
using Contesto.V2.Core.Common.Manager.Base;
using Contesto.V2.Core.Common.Manager.Helpers;
using Contesto.V2.Core.Common.Manager.Results;
using FinoBank.Cola.Manager.Interfaces;
using FinoBank.Cola.Manager.ViewModels;
using FinoBank.Cola.Repository.Uom.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinoBank.Cola.Manager.Queries
{
    public class QueryCheckForMerchantAcceptanceExpirationManagerService : BaseManager, IQueryCheckForMerchantAcceptanceExpirationManagerService
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryCheckForMerchantAcceptanceExpirationManagerService"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public QueryCheckForMerchantAcceptanceExpirationManagerService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, null, null)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<List<TransactionViewModel>>> CheckForMerchantAcceptanceExpiration(int timeStamp)
        {
            var dbResults = await _unitOfWork.QueryCheckForMerchantAcceptanceExpirationRepository.CheckForMerchantAcceptanceExpiration(timeStamp).ConfigureAwait(false);
            return ResponseBuilderHelper<List<TransactionViewModel>>.Instance.BuildSucessResult(MappService.Map<List<TransactionViewModel>>(dbResults));
        }
    }
}