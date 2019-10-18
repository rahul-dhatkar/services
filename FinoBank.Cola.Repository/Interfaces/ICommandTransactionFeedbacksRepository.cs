using System.Threading.Tasks;
using Contesto.V2.Core.Infrastructure.Data.Interfaces;
using FinoBank.Cola.Repository.DomainModels;

namespace FinoBank.Cola.Repository.Interfaces
{
    public interface ICommandTransactionFeedbacksRepository //: ICommandGenericRepository<TransactionFeedbacksDomainModel, int>
    {
        Task<int> Create(TransactionFeedbacksDomainModel model);
        Task<int> Update(TransactionFeedbacksDomainModel model);
    }
}