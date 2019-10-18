using Contesto.V2.Core.Common.Manager.Results;
using FinoBank.Cola.Manager.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinoBank.Cola.Manager.Interfaces
{
    public interface IQueryCheckForTransactionRequestExpirationManagerService
    {
        Task<OperationResult<List<TransactionViewModel>>> CheckForTransactionRequestExpiration(int timeStamp);
    }
}