using Contesto.V2.Core.Common.Manager.Results;
using FinoBank.Cola.Manager.ViewModels;
using System.Threading.Tasks;

namespace FinoBank.Cola.Manager.Interfaces
{
    public interface IQueryMerchantBulkUploadManagerService
    {
        Task<OperationResult<CommandSuccessBoolResultViewModel>> MerchantBulkUpload(string uploadFilePath);
    }
}