using Contesto.V2.Core.Common.Manager.Results;
using FinoBank.Cola.Manager.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinoBank.Cola.Manager.Interfaces
{
    public interface IQueryCheckForMerchantAcceptanceExpirationManagerService
    {
        Task<OperationResult<List<TransactionViewModel>>> CheckForMerchantAcceptanceExpiration(int timeStamp);
    }
}