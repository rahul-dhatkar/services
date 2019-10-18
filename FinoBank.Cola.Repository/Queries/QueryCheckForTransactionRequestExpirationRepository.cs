using Contesto.V2.Core.Infrastructure.Data;
using Dapper;
using FinoBank.Cola.Repository.DomainModels;
using FinoBank.Cola.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FinoBank.Cola.Repository.Queries
{
    internal class QueryCheckForTransactionRequestExpirationRepository : QueryGenericSqlRepository<TransactionRequestsDomainModel>, IQueryCheckForTransactionRequestExpirationRepository
    {
        internal QueryCheckForTransactionRequestExpirationRepository(string connectionString) : base(connectionString)
        {

        }

        public async Task<List<TransactionRequestsDomainModel>> CheckForTransactionRequestExpiration(int timeStamp)
        {
            var results = new List<TransactionRequestsDomainModel>();
            var parameters = new DynamicParameters();
            parameters.Add("@SlaInMinutes", timeStamp, DbType.Int32, ParameterDirection.Input);
            var querystring = " SELECT Id,ReferenceNumber,MerchantId,CustomerId,RequestedAmount " +
                              " FROM TransactionRequests WHERE TransactionStatusId = 1 " +
                              " AND(DATEADD(MINUTE, @SlaInMinutes, RequestedDateTime)) <= GETDATE()";

            var queryresults = await Context.ExecuteReadSqlAsync<TransactionRequestsDomainModel>(querystring, parameters).ConfigureAwait(false);

            var expiredId = queryresults.Select(t => t.Id.ToString()).ToList();

            if (queryresults.Any() && queryresults.Count() > 0 && expiredId.Any())
            {
                var updatestring = " UPDATE TRANSACTIONREQUESTS WITH (ROWLOCK) SET  " +
                                    " TransactionStatusId = 4, " +
                                    " ModifiedBy = 'CheckForTransactionRequestExpirationJob', " +
                                    " Remarks = 'Expired by CheckForTransactionRequestExpirationJob', " +
                                    " ModifiedDateTime = getdate() " +
                                    " where Id in (" + string.Join(",", expiredId) + ")";
                await Context.ExecuteWriteSqlAsync(updatestring, parameters).ConfigureAwait(false);

                var subQueryresults = queryresults.Where(t => t.MerchantId != 0);
                foreach (var record in subQueryresults)
                {
                    parameters = new DynamicParameters();
                    parameters.Add("@SlaInMinutes", timeStamp, DbType.Int32, ParameterDirection.Input);
                    var queryAmount = " SELECT RequestedAmount " +
                                " FROM TransactionRequests WHERE TransactionStatusId = 4 " +
                                " AND(DATEADD(MINUTE, @SlaInMinutes, RequestedDateTime)) <= GETDATE() AND Id = " + record.Id + " AND MerchantId =" + record.MerchantId + " ";

                    var queryAmountData = await Context.ExecuteReadSqlAsync<TransactionRequestsDomainModel>(queryAmount, parameters).ConfigureAwait(false);

                    var requestAmount = queryAmountData.Select(t => t.RequestedAmount.ToString()).FirstOrDefault();

                    var updateMerchantSetupString = " UPDATE MerchantSetups WITH (ROWLOCK) " +
                                                    " SET WithdrawCashBalance = WithdrawCashBalance + " + requestAmount + " , ModifiedDateTime = getdate() where MerchantId =" + record.MerchantId + " ";

                    await Context.ExecuteWriteSqlAsync(updateMerchantSetupString, parameters).ConfigureAwait(false);

                }

                var queryExpiredData = " SELECT Id,ReferenceNumber,MerchantId,CustomerId,RequestedAmount " +
                         " FROM TransactionRequests WHERE TransactionStatusId = 4 " +
                         " AND(DATEADD(MINUTE, @SlaInMinutes, RequestedDateTime)) <= GETDATE() AND MerchantId != 0 AND Id in (" + string.Join(",", expiredId) + ") ";
                var dbresults = await Context.ExecuteReadSqlAsync<TransactionRequestsDomainModel>(queryExpiredData, parameters).ConfigureAwait(false);
                results = dbresults.ToList();
            }
            return results.ToList();
        }
    }
}
