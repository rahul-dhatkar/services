using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Contesto.V2.Core.Data;
using Contesto.V2.Core.Data.Interfaces;
using Contesto.V2.Core.Infrastructure.Data;
using Dapper;
using FinoBank.Cola.Repository.DomainModels;
using FinoBank.Cola.Repository.Interfaces;

namespace FinoBank.Cola.Repository.Commands
{
    /// <summary>
    /// Command Master Repository
    /// </summary>
    /// <seealso cref="CommandGenericRepository{MasterDomainModel, System.Int32}" />
    /// <seealso cref="ICommandTransactionRequestsRepository" />
    internal class CommandUpdateTransactionRequestsRepository : ICommandUpdateTransactionRequestsRepository //CommandGenericRepository<TransactionStatusUpdateDomainModel, long>,
    {
        protected readonly IDataContext Context = null;
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandSampleRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        internal CommandUpdateTransactionRequestsRepository(string connectionString) //: base(connectionString)
        {
            Context = new DataContext<SqlConnection>(connectionString);
        }

        public async Task<long> Update(TransactionStatusUpdateDomainModel model)
        {
            long updatedId = 0;
            var parameters = new DynamicParameters();
            parameters.Add("@Id", model.Id, DbType.Int64, ParameterDirection.Input);
            int transactionId = await Context.ExecuteSingleRecordReadSqlAsync<int>("SELECT Id FROM dbo.TransactionRequests WHERE Id = @Id AND IsActive = 1 AND IsDeleted = 0", parameters).ConfigureAwait(false);
            int RequestedAmount = await Context.ExecuteSingleRecordReadSqlAsync<int>("SELECT RequestedAmount FROM dbo.TransactionRequests WHERE Id = @Id AND IsActive = 1 AND IsDeleted = 0", parameters).ConfigureAwait(false);
            parameters.Add("@ActualAmount", RequestedAmount, DbType.Int32, ParameterDirection.Input);

            if (model.TransactionNewStatusId == 0 || model.TransactionNewStatusId == 1 || model.TransactionNewStatusId == 3 || model.TransactionNewStatusId == 4)
            {
                parameters.Add("@ModifiedDateTime", DateTime.Now, DbType.DateTime, ParameterDirection.Input);
            }
            else
            {
                parameters.Add("@ModifiedDateTime","", DbType.String, ParameterDirection.Input);
            }
            if (model.TransactionNewStatusId == 2)
            {
                parameters.Add("@RequestCompletedDateTime", DateTime.Now, DbType.DateTime, ParameterDirection.Input);
            }
            else
            {
                parameters.Add("@RequestCompletedDateTime","", DbType.String, ParameterDirection.Input);
            }

            parameters.Add("@TransactionStatusId", model.TransactionNewStatusId, DbType.Int16, ParameterDirection.Input);
            parameters.Add("@Remarks", model.Remarks, DbType.String, ParameterDirection.Input);
            parameters.Add("@UpdatedId", updatedId, DbType.Int64, ParameterDirection.Output);

            var querystring = "UPDATE [dbo].[TransactionRequests]  SET ActualAmount = @ActualAmount, RequestCompletedDateTime = @RequestCompletedDateTime ," +
            "ModifiedDateTime = @ModifiedDateTime ,TransactionStatusId = @TransactionStatusId, Remarks = @Remarks WHERE Id = @Id";


            await Context.ExecuteWriteSqlAsync(querystring, parameters).ConfigureAwait(false);
            updatedId = await Context.ExecuteSingleRecordReadSqlAsync<long>("SELECT Id FROM dbo.TransactionRequests WHERE Id = @Id AND IsActive = 1 AND IsDeleted = 0", parameters).ConfigureAwait(false);
            return updatedId;
        }
    }
}