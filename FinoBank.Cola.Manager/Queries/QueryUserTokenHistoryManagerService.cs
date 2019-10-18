using System.Collections.Generic;
using Contesto.V2.Core.Common.Manager.Base;
using FinoBank.Cola.Manager.Interfaces;
using AutoMapper;
using Contesto.V2.Core.Common.Manager.Helpers;
using Contesto.V2.Core.Common.Manager.Results;
using FinoBank.Cola.Manager.ViewModels;
using FinoBank.Cola.Repository.Uom.Interfaces;
using System.Threading.Tasks;

namespace FinoBank.Cola.Manager.Queries
{
    public class QueryUserTokenHistoryManagerService : BaseManager, IQueryUserTokenHistoryManagerService
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryCheckForMerchantAcceptanceExpirationManagerService"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="unitOfWork">The unit of work.</param>

        public QueryUserTokenHistoryManagerService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, null, null)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<List<UserTokenViewModel>>> CheckForUserTokenHistory(int timeStamp)
        {
            var dbResults = await _unitOfWork.QueryUserTokenHistoryRepository.CheckForUserTokenHistory(timeStamp).ConfigureAwait(false);
            return ResponseBuilderHelper<List<UserTokenViewModel>>.Instance.BuildSucessResult(MappService.Map<List<UserTokenViewModel>>(dbResults));
        }
    }
}


