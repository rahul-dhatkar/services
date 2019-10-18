using AutoMapper;
using Contesto.V2.Core.Common.Manager.Base;
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
    /// <seealso cref="FinoBank.Cola.Manager.Interfaces.ICommandActivityManagerService" />
    public class CommandActivityManagerService : BaseManager, ICommandActivityManagerService
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandActivityManagerService"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public CommandActivityManagerService(IMapper mapper, IUnitOfWork unitOfWork) :
            base(mapper, null, null)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Activities the logger.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task ActivityLogger(ActivityLogViewModel model)
        {
            var details = MappService.Map<ActivityLogDomainModel>(model);

            await _unitOfWork.CommandActivityRepository.CreateActivityLog(details).ConfigureAwait(false);
        }
    }
}