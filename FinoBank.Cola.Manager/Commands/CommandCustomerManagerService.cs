using AutoMapper;
using Contesto.V2.Core.Common.Manager.Base;
using Contesto.V2.Core.Common.Manager.Helpers;
using Contesto.V2.Core.Common.Manager.Results;
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
    /// <seealso cref="FinoBank.Cola.Manager.Interfaces.ICommandCustomerManagerService" />

    public class CommandCustomerManagerService : BaseManager, ICommandCustomerManagerService
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
        public CommandCustomerManagerService(IMapper mapper, IUnitOfWork unitOfWork) :
            base(mapper, null, null)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Updates the master.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<OperationResult<CommandSuccessLongResultViewModel>> CheckAndCreateCustomer(CustomerViewModel model)
        {
            var details = MappService.Map<CustomerDomainModel>(model);

            var result = await _unitOfWork.CommandCustomerRepository.CheckAndCreateCustomer(details).ConfigureAwait(false);

            return ResponseBuilderHelper<CommandSuccessLongResultViewModel>.Instance.BuildSucessResult(new CommandSuccessLongResultViewModel() { ResponseValue = result });
        }
    }
}