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
    /// Command Master Manager Service
    /// </summary>
    /// <seealso cref="Contesto.V2.Core.Common.Manager.Base.BaseManager" />
    /// <seealso cref="FinoBank.Cola.Manager.Interfaces.ICommandSampleManagerService" />
    /// <seealso cref="BaseManager" />
    /// <seealso cref="ICommandSampleManagerService" />
    public class CommandSampleManagerService : BaseManager, ICommandSampleManagerService
    {
        #region "Variables"

        /// <summary>
        /// The startup kit unit of work
        /// </summary>
        private readonly IUnitOfWork _startupKitUnitOfWork;

        #endregion "Variables"

        #region "Constructor"

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandSampleManagerService" /> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="startupKitUnitOfWork">The master unit of work.</param>
        public CommandSampleManagerService(IMapper mapper, IUnitOfWork startupKitUnitOfWork) :
            base(mapper, null, null)
        {
            _startupKitUnitOfWork = startupKitUnitOfWork;
        }

        #endregion "Constructor"

        #region "Master"

        /// <summary>
        /// Creates the startup kit.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<OperationResult<CommandSuccessResultViewModel>> CreateStartupKit(SampleViewModel model)
        {
            var details = MappService.Map<SampleDomainModel>(model);
            var validationResult = await _startupKitUnitOfWork.QuerySampleRepository.IsExist(details).ConfigureAwait(false);
            if (validationResult)
            {
                return ResponseBuilderHelper<CommandSuccessResultViewModel>.Instance.BuildUnSucessResult(new List<ErrorModel>()
                { new ErrorModel()
                { Message = ValidationMessageHelper.UnableToCreateMaster }
                });
            }

            var result = await _startupKitUnitOfWork.CommandSampleRepository.Create(details, EmumDbInPutFormat.Json).ConfigureAwait(false);

            return ResponseBuilderHelper<CommandSuccessResultViewModel>.Instance.BuildSucessResult(new CommandSuccessResultViewModel() { ResponseValue = result });
        }

        /// <summary>
        /// Updates the startup kit.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<OperationResult<CommandSuccessResultViewModel>> UpdateStartupKit(SampleViewModel model)
        {
            var details = MappService.Map<SampleDomainModel>(model);
            var validationResult = await _startupKitUnitOfWork.QuerySampleRepository.IsExist(details).ConfigureAwait(false);
            if (validationResult)
            {
                return ResponseBuilderHelper<CommandSuccessResultViewModel>.Instance.BuildUnSucessResult(new List<ErrorModel>()
                { new ErrorModel()
                { Message = ValidationMessageHelper.UnableToCreateMaster }
                });
            }

            var result = await _startupKitUnitOfWork.CommandSampleRepository.Update(details, EmumDbInPutFormat.Json).ConfigureAwait(false);

            return ResponseBuilderHelper<CommandSuccessResultViewModel>.Instance.BuildSucessResult(new CommandSuccessResultViewModel() { ResponseValue = result });
        }

        #endregion "Master"
    }
}