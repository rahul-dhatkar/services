using Contesto.V2.Core.Infrastructure.Data;
using Dapper;
using FinoBank.Cola.Repository.DomainModels;
using FinoBank.Cola.Repository.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FinoBank.Cola.Repository.Queries
{
    internal class QueryMerchantBulkUploadRepository : QueryGenericRepository<bool>, IQueryMerchantBulkUploadRepository
    {
        internal QueryMerchantBulkUploadRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<bool> MerchantBulkUpload(List<MerchantDomainModel> merchantList)
        {
            var parameters = new DynamicParameters();
            
            var Json = JsonConvert.SerializeObject(merchantList);
            parameters.Add("@Json", Json, DbType.String, ParameterDirection.Input);

            var results = await Context.ExecuteReadProcedureAsync<bool>("MerchantBulkUpload", parameters).ConfigureAwait(false);
            return results.FirstOrDefault();
        }
    }
}
