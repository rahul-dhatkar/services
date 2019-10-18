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
    internal class CommandTransactionFeedbacksRepository : ICommandTransactionFeedbacksRepository //CommandGenericRepository<TransactionFeedbacksDomainModel, int>, 
    {
        protected readonly IDataContext Context = null;
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandSampleRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        internal CommandTransactionFeedbacksRepository(string connectionString) //: base(connectionString)
        {
            Context = new DataContext<SqlConnection>(connectionString);
        }

        public async Task<int> Create(TransactionFeedbacksDomainModel model)
        {
            int insertedId = 0;
            var parameters = new DynamicParameters();
            if(model.TransactionId > 0)
            {
                parameters.Add("@TransactionId", model.TransactionId, DbType.Int64, ParameterDirection.Input);
                await Context.ExecuteWriteSqlAsync("UPDATE [dbo].[TransactionFeedbacks] set IsActive = 0 , IsDeleted = 1 , ModifiedDateTime = GETDATE() WHERE TransactionId = @TransactionId ", parameters).ConfigureAwait(false);
            }
            parameters.Add("@MerchantId", model.MerchantId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@CustomerId", model.CustomerId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@TransactionId", model.TransactionId, DbType.Int64, ParameterDirection.Input);
            parameters.Add("@Notes", model.Notes, DbType.String, ParameterDirection.Input);
            parameters.Add("@Rating", model.Rating, DbType.Int16, ParameterDirection.Input);
            parameters.Add("@CreatedBy", model.CustomerId, DbType.String, ParameterDirection.Input);
            parameters.Add("@IsActive", true, DbType.String, ParameterDirection.Input);
            parameters.Add("@IsDeleted", false, DbType.String, ParameterDirection.Input);
            var queryFeedbackInsert = "INSERT INTO TransactionFeedbacks(MerchantId, CustomerId, TransactionId, Notes, Rating, CreatedBy, CreatedDateTime, IsActive, IsDeleted) " +
            " values (@MerchantId, @CustomerId, @TransactionId, @Notes, @Rating, @CreatedBy, GETDATE(), @IsActive, @IsDeleted)";
            await Context.ExecuteWriteSqlAsync(queryFeedbackInsert, parameters).ConfigureAwait(false);
            insertedId = await Context.ExecuteSingleRecordReadSqlAsync<int>("SELECT Id FROM TransactionFeedbacks WHERE MerchantId = @MerchantId AND TransactionId = @TransactionId AND CustomerId = @CustomerId AND IsActive = 1 AND IsDeleted = 0", parameters).ConfigureAwait(false);
            return insertedId;
        }

        public Task<int> Update(TransactionFeedbacksDomainModel model)
        {
            throw new NotImplementedException();
        }
    }
}