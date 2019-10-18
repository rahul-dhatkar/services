using Contesto.V2.Core.Infrastructure.Data;
using Dapper;
using FinoBank.Cola.Repository.Interfaces;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FinoBank.Cola.Repository.Queries
{
    internal class QueryAcceptTransactionRequestRepository : QueryGenericRepository<bool>, IQueryAcceptTransactionRequestRepository
    {
        internal QueryAcceptTransactionRequestRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<bool> AcceptTransactionRequest(long TransactionId, string RefCode)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@TransactionId", TransactionId, DbType.Int64, ParameterDirection.Input);
            parameters.Add("@RefCode", RefCode, DbType.String, ParameterDirection.Input);

            int MerchantId = await Context.ExecuteSingleRecordReadSqlAsync<int>("SELECT Id FROM Merchants WHERE RefCode=@RefCode", parameters).ConfigureAwait(false);
            parameters.Add("@MerchantId", MerchantId, DbType.Int16, ParameterDirection.Input);

            var result = await Context.ExecuteWriteSqlAsync("UPDATE TransactionRequests SET TransactionStatusId=1, MerchantId=@MerchantId WHERE Id=@TransactionId", parameters).ConfigureAwait(false);

            int RequestedAmount = await Context.ExecuteSingleRecordReadSqlAsync<int>("Select RequestedAmount from TransactionRequests WHERE Id=@TransactionId", parameters).ConfigureAwait(false);
            parameters.Add("@RequestedAmount", RequestedAmount, DbType.Int16, ParameterDirection.Input);

            var results = await Context.ExecuteWriteSqlAsync("UPDATE MerchantSetups SET WithdrawCashBalance= WithdrawCashBalance - @RequestedAmount WHERE MerchantId=@MerchantId", parameters).ConfigureAwait(false);
            return Convert.ToBoolean(results);
                //results.FirstOrDefault();
        }
    }
}