using Contesto.V2.Core.Infrastructure.Data;
using FinoBank.Cola.Repository.DomainModels;
using FinoBank.Cola.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinoBank.Cola.Repository.Queries
{
    internal class QueryTransactionTypeMasterDataRepository : QueryGenericSqlRepository<TransactionTypeDomainModel>, IQueryTransactionTypeMasterDataRepository
    {
        internal QueryTransactionTypeMasterDataRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<Tuple<List<TransactionTypeDomainModel>>> GetTransactionTypeMaster()
        {
            var results = await Context.ExecuteReadSqlAsync<TransactionTypeDomainModel>("SELECT Id,Name,CreatedBy,CreatedDateTime,ModifiedBy,ModifiedDateTime,IsActive,IsDeleted FROM TransactionTypes WHERE IsActive=1 AND IsDeleted=0").ConfigureAwait(false);

            return new Tuple<List<TransactionTypeDomainModel>>(results.ToList());
        }
    }
}