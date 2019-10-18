using AutoMapper;
using Contesto.V2.Core.Common.Manager.Base;
using Contesto.V2.Core.Common.Manager.Helpers;
using Contesto.V2.Core.Common.Manager.Results;
using FinoBank.Cola.Manager.Interfaces;
using FinoBank.Cola.Manager.ViewModels;
using FinoBank.Cola.Repository.Uom.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinoBank.Cola.Manager.Queries
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="Contesto.V2.Core.Common.Manager.Base.BaseManager" />
    /// <seealso cref="FinoBank.Cola.Manager.Interfaces.IQueryMasterDataManagerService" />
    public class QueryMasterDataManagerService : BaseManager, IQueryMasterDataManagerService
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryMasterDataManagerService"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="cache">The cache.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public QueryMasterDataManagerService(IMapper mapper, IMemoryCache cache, IUnitOfWork unitOfWork) : base(mapper, cache, null)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<List<MerchantTypeViewModel>>> GetMerchantTypeMaster()
        {
            var result = await _unitOfWork.QueryMerchantTypeMasterDataRepository.GetMerchantTypeMaster().ConfigureAwait(false);

            return ResponseBuilderHelper<List<MerchantTypeViewModel>>.Instance.BuildSucessResult(MappService.Map<List<MerchantTypeViewModel>>(result.Item1.ToList()));
        }

        public async Task<OperationResult<List<TransactionStatusViewModel>>> GetTransactionStatusMaster()
        {
            var result = await _unitOfWork.QueryTransactionStatusMasterDataRepository.GetTransactionStatusMaster().ConfigureAwait(false);

            return ResponseBuilderHelper<List<TransactionStatusViewModel>>.Instance.BuildSucessResult(MappService.Map<List<TransactionStatusViewModel>>(result.Item1.ToList()));
        }

        public async Task<OperationResult<List<TransactionTypeViewModel>>> GetTransactionTypeMaster()
        {
            var result = await _unitOfWork.QueryTransactionTypeMasterDataRepository.GetTransactionTypeMaster().ConfigureAwait(false);

            return ResponseBuilderHelper<List<TransactionTypeViewModel>>.Instance.BuildSucessResult(MappService.Map<List<TransactionTypeViewModel>>(result.Item1.ToList()));
        }

        /// <summary>
        /// Gets the withdrawal type master.
        /// </summary>
        /// <returns></returns>
        public async Task<OperationResult<List<WithdrawalTypeViewModel>>> GetWithdrawalTypeMaster()
        {
            var result = await _unitOfWork.QueryWithdrawalTypeMasterDataRepository.GetWithdrawalTypeMaster().ConfigureAwait(false);

            return ResponseBuilderHelper<List<WithdrawalTypeViewModel>>.Instance.BuildSucessResult(MappService.Map<List<WithdrawalTypeViewModel>>(result.Item1.ToList()));
        }
    }
}