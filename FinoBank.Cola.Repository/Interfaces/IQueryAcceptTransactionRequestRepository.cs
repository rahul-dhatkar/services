using System.Threading.Tasks;
using Contesto.V2.Core.Infrastructure.Data.Interfaces;

namespace FinoBank.Cola.Repository.Interfaces
{
    public interface IQueryAcceptTransactionRequestRepository : IQueryGenericRepository<bool>
    {
        Task<bool> AcceptTransactionRequest(long TransactionId, string RefCode);
    }
}