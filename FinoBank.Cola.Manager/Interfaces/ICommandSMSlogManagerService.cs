using Contesto.V2.Core.Common.Manager.Results;
using Contesto.V2.Core.Common.ViewModel.Base;
using Contesto.V2.Core.Infrastructure.Data.Interfaces;
using FinoBank.Cola.Manager.ViewModels;
using FinoBank.Cola.Repository.DomainModels;
using System.Threading.Tasks;
using CommandSuccessBoolResultViewModel = FinoBank.Cola.Manager.ViewModels.CommandSuccessBoolResultViewModel;

namespace FinoBank.Cola.Manager.Interfaces
{
    public interface ICommandSMSlogManagerService
    {
        Task<OperationResult<CommandSuccessResultViewModel>> CreateSMSlogs(SMSlogViewModel model);
        Task<OperationResult<CommandSuccessBoolResultViewModel>> DeleteSMSlog(int timeStamp);
    }
}
