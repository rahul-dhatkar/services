using Contesto.V2.Core.Infrastructure.Data;
using Dapper;
using FinoBank.Cola.Repository.DomainModels;
using FinoBank.Cola.Repository.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FinoBank.Cola.Repository.Queries
{
    internal class QueryCheckForMerchantAcceptanceExpirationRepository : QueryGenericRepository<TransactionRequestsDomainModel>, IQueryCheckForMerchantAcceptanceExpirationRepository
    {
        internal QueryCheckForMerchantAcceptanceExpirationRepository(string connectionString) : base(connectionString)
        {

        }

        public async Task<List<TransactionRequestsDomainModel>> CheckForMerchantAcceptanceExpiration(int timeStamp)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@SlaInMinutes", timeStamp, DbType.Int32, ParameterDirection.Input);
            var queryTempAcceptance = "SELECT Id " +
                               " FROM TransactionRequests WHERE TransactionStatusId = 0 " +
                               " AND MerchantId = 0 " +
                               " AND(DATEADD(MINUTE, @SlaInMinutes, RequestedDateTime)) <= GETDATE()";
            var queryresults = await Context.ExecuteReadSqlAsync<TransactionRequestsDomainModel>(queryTempAcceptance, parameters).ConfigureAwait(false);
            var expiredId = queryresults.Select(t => t.Id.ToString()).ToList();

            if (queryresults.Count() > 0 )
            {
                var updatedstring = "UPDATE TransactionRequests SET IsActive = 0, " +
                         " TransactionStatusId = 4, " +
                         " ModifiedBy = 'CheckForMerchantAcceptanceJob', " +
                         " Remarks = 'Expired by CheckForMerchantAcceptanceExpirationJob', " +
                         " ModifiedDateTime = GETDATE() " +
                         " where Id in (" + string.Join(",", expiredId) + ")";
                await Context.ExecuteReadSqlAsync<TransactionRequestsDomainModel>(updatedstring, parameters).ConfigureAwait(false);
            }
            var resultstring = "SELECT Id, ReferenceNumber, MerchantId, CustomerId, RequestedAmount " +
                            " FROM TransactionRequests WHERE TransactionStatusId = 0 " +
                            " AND MerchantId = 0 " +
                            " AND(DATEADD(MINUTE, @SlaInMinutes, RequestedDateTime)) <= GETDATE()";
            var results = await Context.ExecuteReadSqlAsync<TransactionRequestsDomainModel>(resultstring, parameters).ConfigureAwait(false);
            return results.ToList();
        }
    }
}

