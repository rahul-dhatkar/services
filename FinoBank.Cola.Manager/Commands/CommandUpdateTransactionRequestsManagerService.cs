using AutoMapper;
using Contesto.V2.Core.Common.Manager.Base;
using Contesto.V2.Core.Common.Manager.Helpers;
using Contesto.V2.Core.Common.Manager.Results;
using Contesto.V2.Core.Infrastructure.Data;
using FinoBank.Cola.Manager.Interfaces;
using FinoBank.Cola.Manager.ViewModels;
using FinoBank.Cola.Repository.DomainModels;
using FinoBank.Cola.Repository.Uom.Interfaces;
using System.Threading.Tasks;

namespace FinoBank.Cola.Manager.Commands
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="Contesto.V2.Core.Common.Manager.Base.BaseManager" />
    /// <seealso cref="FinoBank.Cola.Manager.Interfaces.ICommandUpdateTransactionRequestsManagerService" />
    public class CommandUpdateTransactionRequestsManagerService : BaseManager, ICommandUpdateTransactionRequestsManagerService
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandTransactionRequestsManagerService" /> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public CommandUpdateTransactionRequestsManagerService(IMapper mapper, IUnitOfWork unitOfWork) :
            base(mapper, null, null)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Updates the master.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<OperationResult<CommandSuccessLongResultViewModel>> UpdateTransactionRequests(TransactionStatusUpdateViewModel model)
        {
            var details = MappService.Map<TransactionStatusUpdateDomainModel>(model);
            var result = await _unitOfWork.CommandUpdateTransactionRequestsRepository.Update(details).ConfigureAwait(false);

            return ResponseBuilderHelper<CommandSuccessLongResultViewModel>.Instance.BuildSucessResult(new CommandSuccessLongResultViewModel() { ResponseValue = result });
        }
    }
}