using AutoMapper;
using Contesto.V2.Core.Common.Manager.Base;
using Contesto.V2.Core.Common.Manager.Helpers;
using Contesto.V2.Core.Common.Manager.Results;
using FinoBank.Cola.Manager.Interfaces;
using FinoBank.Cola.Manager.ViewModels;
using FinoBank.Cola.Repository.Uom.Interfaces;
using System.Threading.Tasks;

namespace FinoBank.Cola.Manager.Queries
{
    public class QueryAcceptTransactionRequestManagerService : BaseManager, IQueryAcceptTransactionRequestManagerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public QueryAcceptTransactionRequestManagerService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, null, null)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<CommandSuccessBoolResultViewModel>> AcceptTransactionRequest(AcceptTransactionRequestViewModel model)
        {
            //var dbResults = 
            await _unitOfWork.QueryAcceptTransactionRequestRepository.AcceptTransactionRequest(model.TransactionId, model.RefCode).ConfigureAwait(false);
            return ResponseBuilderHelper<CommandSuccessBoolResultViewModel>.Instance.BuildSucessResult(new CommandSuccessBoolResultViewModel() { ResponseValue = true });
        }
    }
}