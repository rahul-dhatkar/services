using System.Threading.Tasks;
using Contesto.V2.Core.Infrastructure.Data;
using FinoBank.Cola.Repository.DomainModels;
using FinoBank.Cola.Repository.Interfaces;

namespace FinoBank.Cola.Repository.Commands
{
    internal class CommandTransactionRequestExpirationRepository :  ICommandUpdateTransactionRequestsRepository //CommandGenericRepository<TransactionStatusUpdateDomainModel, long>,
    {
        internal CommandTransactionRequestExpirationRepository(string connectionString) //: base(connectionString)
        {
        }

        public Task<long> Update(TransactionStatusUpdateDomainModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}