using System.Threading.Tasks;
using Contesto.V2.Core.Infrastructure.Data.Interfaces;
using FinoBank.Cola.Repository.DomainModels;

namespace FinoBank.Cola.Repository.Interfaces
{
    public interface ICommandUpdateTransactionRequestsRepository //: ICommandGenericRepository<TransactionStatusUpdateDomainModel, long>
    {
        Task<long> Update(TransactionStatusUpdateDomainModel model);
    }
}