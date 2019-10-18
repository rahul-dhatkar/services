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
    /// <seealso cref="FinoBank.Cola.Manager.Interfaces.ICommandTransactionFeedbacksManagerService" />
    public class CommandTransactionFeedbacksManagerService : BaseManager, ICommandTransactionFeedbacksManagerService
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
        public CommandTransactionFeedbacksManagerService(IMapper mapper, IUnitOfWork unitOfWork) :
            base(mapper, null, null)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Creates the master.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<OperationResult<CommandSuccessResultViewModel>> CreateTransactionFeedbacks(TransactionFeedbackViewModel model)
        {
            var resultModel = await _unitOfWork.QueryTransactionResultRepository.GetTransactionDetailsByUniqueId(model.UniqueId).ConfigureAwait(false);

            var details = MappService.Map<TransactionFeedbacksDomainModel>(model);
            details.TransactionId = resultModel.Item1.Id;
            details.CustomerId = resultModel.Item1.CustomerId;
            details.MerchantId = resultModel.Item1.MerchantId;

            if (resultModel.Item1.TransactionStatusId == 2)
            {
                var result = await _unitOfWork.CommandTransactionFeedbacksRepository.Create(details).ConfigureAwait(false);
                return ResponseBuilderHelper<CommandSuccessResultViewModel>.Instance.BuildSucessResult(new CommandSuccessResultViewModel() { ResponseValue = result });
            }
            else
            {
                return ResponseBuilderHelper<CommandSuccessResultViewModel>.Instance.BuildSucessResult(new CommandSuccessResultViewModel() { ResponseValue = 0 });
            }
        }

        /// <summary>
        /// Updates the master.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<OperationResult<CommandSuccessResultViewModel>> UpdateTransactionFeedbacks(TransactionFeedbackViewModel model)
        {
            var details = MappService.Map<TransactionFeedbacksDomainModel>(model);

            var result = await _unitOfWork.CommandTransactionFeedbacksRepository.Update(details).ConfigureAwait(false);

            return ResponseBuilderHelper<CommandSuccessResultViewModel>.Instance.BuildSucessResult(new CommandSuccessResultViewModel() { ResponseValue = result });
        }
    }
}