using Contesto.V2.Core.Common.Manager.Results;
using FinoBank.Cola.Manager.ViewModels;
using System.Threading.Tasks;

namespace FinoBank.Cola.Manager.Interfaces
{
    public interface IQueryOTPManagerService
    {
        /// <summary>
        /// Gets the merchants with paging.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<OperationResult<GenerateOTPFinalResultViewModel>> GenerateOTP(string serviceURL, GenerateOTPViewModel model);

        Task<OperationResult<CommandSuccessStringResultViewModel>> VerifyOTP(string serviceURL, VerifyOTPViewModel model);

        Task<OperationResult<CommandSuccessBoolResultViewModel>> SendSMS(string serviceURL,long transactionId,SMSRequestViewModel models,string templateId);
    }
}