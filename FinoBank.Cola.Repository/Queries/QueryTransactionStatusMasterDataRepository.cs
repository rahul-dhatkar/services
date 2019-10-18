using Contesto.V2.Core.Infrastructure.Data;
using FinoBank.Cola.Repository.DomainModels;
using FinoBank.Cola.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinoBank.Cola.Repository.Queries
{
    internal class QueryTransactionStatusMasterDataRepository : QueryGenericSqlRepository<TransactionStatusDomainModel>, IQueryTransactionStatusMasterDataRepository
    {
        internal QueryTransactionStatusMasterDataRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<Tuple<List<TransactionStatusDomainModel>>> GetTransactionStatusMaster()
        {
            var results = await Context.ExecuteReadSqlAsync<TransactionStatusDomainModel>("SELECT Id,Name,CreatedBy,CreatedDateTime,ModifiedBy,ModifiedDateTime,IsActive,IsDeleted FROM TransactionStatuses WHERE IsActive=1 AND IsDeleted=0").ConfigureAwait(false);

            return new Tuple<List<TransactionStatusDomainModel>>(results.ToList());
        }
    }
}