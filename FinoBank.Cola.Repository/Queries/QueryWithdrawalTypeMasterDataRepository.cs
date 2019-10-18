using Contesto.V2.Core.Infrastructure.Data;
using FinoBank.Cola.Repository.DomainModels;
using FinoBank.Cola.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinoBank.Cola.Repository.Queries
{
    internal class QueryWithdrawalTypeMasterDataRepository : QueryGenericSqlRepository<WithdrawalTypeDomainModel>, IQueryWithdrawalTypeMasterDataRepository
    {
        internal QueryWithdrawalTypeMasterDataRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<Tuple<List<WithdrawalTypeDomainModel>>> GetWithdrawalTypeMaster()
        {
            var results = await Context.ExecuteReadSqlAsync<WithdrawalTypeDomainModel>("SELECT Id,Name,CreatedBy,CreatedDateTime,ModifiedBy,ModifiedDateTime,IsActive,IsDeleted FROM WithdrawalTypes WHERE IsActive = 1 AND IsDeleted = 0").ConfigureAwait(false);
            return new Tuple<List<WithdrawalTypeDomainModel>>(results.ToList());
        }
    }
}