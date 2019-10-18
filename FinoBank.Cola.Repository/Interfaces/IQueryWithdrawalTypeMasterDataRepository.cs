using Contesto.V2.Core.Infrastructure.Data.Interfaces;
using FinoBank.Cola.Repository.DomainModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinoBank.Cola.Repository.Interfaces
{
    public interface IQueryWithdrawalTypeMasterDataRepository : IQueryGenericSqlRepository<WithdrawalTypeDomainModel>
    {
        Task<Tuple<List<WithdrawalTypeDomainModel>>> GetWithdrawalTypeMaster();
    }
}