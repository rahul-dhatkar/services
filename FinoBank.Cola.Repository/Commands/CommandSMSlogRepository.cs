using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Contesto.V2.Core.Data;
using Contesto.V2.Core.Data.Interfaces;
using Contesto.V2.Core.Infrastructure.Data;
using Dapper;
using FinoBank.Cola.Repository.DomainModels;
using FinoBank.Cola.Repository.Interfaces;

namespace FinoBank.Cola.Repository.Commands
{
    internal class CommandSMSlogRepository : ICommandSMSlogRepository 
    {
        protected readonly IDataContext Context = null;
       
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandSampleRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        internal CommandSMSlogRepository(string connectionString)
        {
            Context = new DataContext<SqlConnection>(connectionString);
        }
        public async Task<long> Create(SMSlogDomainModel model)
        {
            int insertedId = 0;
            var parameters = new DynamicParameters();
            parameters = new DynamicParameters();
            parameters.Add("@TransactionId", model.TransactionId, DbType.Int64, ParameterDirection.Input);
            parameters.Add("@MerchantId", model.MerchantId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@CreatedBy", "Insertion for 10k above transactions", DbType.String, ParameterDirection.Input);
            parameters.Add("@IsActive", true, DbType.Int16, ParameterDirection.Input);
            parameters.Add("@IsDeleted", false, DbType.Int16, ParameterDirection.Input);

            var queryString = "INSERT INTO dbo.SMSlogs(TransactionId,MerchantId,CreatedBy,CreatedDateTime)values(@TransactionId,@MerchantId,@CreatedBy,GETDATE())";

            insertedId = await Context.ExecuteWriteSqlAsync(queryString, parameters).ConfigureAwait(false);

            var smsLogId = await Context.ExecuteSingleRecordReadSqlAsync<long>("SELECT Id FROM SMSlogs WHERE TransactionId = @TransactionId and MerchantId = @MerchantId", parameters).ConfigureAwait(false);
            return smsLogId;
        }
        public async Task<bool> Delete(int timeStamp)
        {
            int results = 0;
            if (timeStamp != 0)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@TimeStamp", timeStamp, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@ResultStatus", true, DbType.Boolean, ParameterDirection.Output);
                results = await Context.ExecuteWriteSqlAsync("Delete from SMSlogs", parameters).ConfigureAwait(false);
                return Convert.ToBoolean(results);
            }
            return Convert.ToBoolean(results);
        }
    }
}

