using Contesto.V2.Core.Infrastructure.Data.Interfaces;
using FinoBank.Cola.Repository.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinoBank.Cola.Repository.Interfaces
{
    public interface IQueryCheckForMerchantAcceptanceExpirationRepository : IQueryGenericRepository<TransactionRequestsDomainModel>
    {
        Task<List<TransactionRequestsDomainModel>> CheckForMerchantAcceptanceExpiration(int timeStamp);
    }
}