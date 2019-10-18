using AutoMapper;
using Contesto.V2.Core.Common.Manager.Base;
using Contesto.V2.Core.Common.Manager.Helpers;
using Contesto.V2.Core.Common.Manager.Results;
using Contesto.V2.Core.Infrastructure.Data;
using FinoBank.Cola.Manager.Interfaces;
using FinoBank.Cola.Manager.ViewModels;
using FinoBank.Cola.Repository.DomainModels;
using FinoBank.Cola.Repository.Uom.Interfaces;
using System;
using System.Threading.Tasks;

namespace FinoBank.Cola.Manager.Commands
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="Contesto.V2.Core.Common.Manager.Base.BaseManager" />
    /// <seealso cref="FinoBank.Cola.Manager.Interfaces.ICommandCreateMerchantsManagerService" />
    public class CommandMerchantManagerService : BaseManager, ICommandCreateMerchantsManagerService
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
        public CommandMerchantManagerService(IMapper mapper, IUnitOfWork unitOfWork) :
            base(mapper, null, null)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Creates the master.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<OperationResult<CommandSuccessResultViewModel>> CreateMerchants(MerchantViewModel model)
        {
            var details = MappService.Map<MerchantDomainModel>(model);
            var result = await _unitOfWork.CommandCreateMerchantRepository.Create(details).ConfigureAwait(false);

            return ResponseBuilderHelper<CommandSuccessResultViewModel>.Instance.BuildSucessResult(new CommandSuccessResultViewModel() { ResponseValue = Convert.ToInt32(result) });
        }

        /// <summary>
        /// Updates the master.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<OperationResult<CommandSuccessResultViewModel>> UpdateMerchants(MerchantViewModel model)
        {
            var details = MappService.Map<MerchantDomainModel>(model);

            var result = await _unitOfWork.CommandCreateMerchantRepository.Update(details).ConfigureAwait(false);

            return ResponseBuilderHelper<CommandSuccessResultViewModel>.Instance.BuildSucessResult(new CommandSuccessResultViewModel() { ResponseValue = result });
        }

        /// <summary>
        /// Updates the master.
        /// </summary>
        /// <param name="refCode"></param>
        /// <returns></returns>
        public async Task<OperationResult<CommandSuccessBoolResultViewModel>> DeleteMerchants(string refCode)
        {
            var result = await _unitOfWork.CommandCreateMerchantRepository.Delete(refCode).ConfigureAwait(false);

            return ResponseBuilderHelper<CommandSuccessBoolResultViewModel>.Instance.BuildSucessResult(new CommandSuccessBoolResultViewModel() { ResponseValue = result });
        }
    }
}