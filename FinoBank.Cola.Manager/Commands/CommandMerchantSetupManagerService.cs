using AutoMapper;
using Contesto.V2.Core.Common.Manager.Base;
using Contesto.V2.Core.Common.Manager.Helpers;
using Contesto.V2.Core.Common.Manager.Results;
using Contesto.V2.Core.Common.Utility.Models;
using Contesto.V2.Core.Infrastructure.Data;
using FinoBank.Cola.Manager.Helpers;
using FinoBank.Cola.Manager.Interfaces;
using FinoBank.Cola.Manager.ViewModels;
using FinoBank.Cola.Repository.DomainModels;
using FinoBank.Cola.Repository.Uom.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinoBank.Cola.Manager.Commands
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="Contesto.V2.Core.Common.Manager.Base.BaseManager" />
    /// <seealso cref="FinoBank.Cola.Manager.Interfaces.ICommandMerchantSetupManagerService" />
    public class CommandMerchantSetupManagerService : BaseManager, ICommandMerchantSetupManagerService
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// The query merchant summary manager service
        /// </summary>
        private readonly IQueryMerchantSummaryManagerService _queryMerchantSummaryManagerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandTransactionRequestsManagerService" /> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public CommandMerchantSetupManagerService(IMapper mapper, IUnitOfWork unitOfWork) :
            base(mapper, null, null)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Updates the master.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<OperationResult<CommandSuccessStringResultViewModel>> UpdateMerchantSetups(MerchantSetupViewModel model)
        {
            var details = MappService.Map<MerchantSetupDomainModel>(model);
            var result = await _unitOfWork.CommandMerchantSetupRepository.Update(details).ConfigureAwait(false);
            return ResponseBuilderHelper<CommandSuccessStringResultViewModel>.Instance.BuildSucessResult(new CommandSuccessStringResultViewModel() { ResponseValue = result.ToString() });
        }
    }
}