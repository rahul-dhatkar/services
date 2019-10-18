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
    public class CommandSMSlogManagerService : BaseManager, ICommandSMSlogManagerService
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
        public CommandSMSlogManagerService(IMapper mapper, IUnitOfWork unitOfWork) :
            base(mapper, null, null)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Creates the master.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<OperationResult<CommandSuccessResultViewModel>> CreateSMSlogs(SMSlogViewModel model)
        {
            var details = MappService.Map<SMSlogDomainModel>(model);
            var result = await _unitOfWork.CommandSMSlogRepository.Create(details).ConfigureAwait(false);
            return ResponseBuilderHelper<CommandSuccessResultViewModel>.Instance.BuildSucessResult(new CommandSuccessResultViewModel() { ResponseValue = Convert.ToInt16(result) });
        }

        public async Task<OperationResult<CommandSuccessBoolResultViewModel>> DeleteSMSlog(int timeStamp)
        {
            var result = await _unitOfWork.CommandSMSlogRepository.Delete(timeStamp).ConfigureAwait(false);
            return ResponseBuilderHelper<CommandSuccessBoolResultViewModel>.Instance.BuildSucessResult(new CommandSuccessBoolResultViewModel() { ResponseValue = result });
        }
    }
}
