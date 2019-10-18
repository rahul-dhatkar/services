using Contesto.V2.Core.Infrastructure.Data;
using FinoBank.Cola.Repository.DomainModels;
using FinoBank.Cola.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinoBank.Cola.Repository.Queries
{
    internal class QueryMerchantTypeMasterDataRepository : QueryGenericSqlRepository<MerchantTypeDomainModel>, IQueryMerchantTypeMasterDataRepository
    {
        internal QueryMerchantTypeMasterDataRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<Tuple<List<MerchantTypeDomainModel>>> GetMerchantTypeMaster()
        {
            var results = await Context.ExecuteReadSqlAsync<MerchantTypeDomainModel>("SELECT Id,Code,Name,CreatedBy,CreatedDateTime,ModifiedBy,ModifiedDateTime,IsActive,IsDeleted FROM MerchantTypes WHERE IsActive=1 AND IsDeleted=0").ConfigureAwait(false);

            return new Tuple<List<MerchantTypeDomainModel>>(results.ToList());
        }
    }
}