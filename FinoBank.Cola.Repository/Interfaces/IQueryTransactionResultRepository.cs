using Contesto.V2.Core.Infrastructure.Data.Interfaces;
using FinoBank.Cola.Repository.DomainModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinoBank.Cola.Repository.Interfaces
{
    public interface IQueryTransactionResultRepository : IQueryGenericSqlRepository<TransactionRequestsDomainModel>
    {
        Task<Tuple<TransactionRequestsDomainModel>> GetTransactionDetailsById(long transactionId);
        Task<Tuple<TransactionRequestsDomainModel>> GetTransactionDetailsByUniqueId(Guid uniqueId);
        Task<Tuple<TransactionRequestsDomainModel>> GetAllMobileNoByTransactionId(long transactionId);
        Task<List<TransactionRequestsDomainModel>> GetAllMobileNoForAbove10K(long transactionId);
    }
}