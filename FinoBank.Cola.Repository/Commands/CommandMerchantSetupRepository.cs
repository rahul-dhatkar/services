using System;
using System.Collections.Generic;
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
    internal class CommandMerchantSetupRepository :  ICommandMerchantSetupRepository 
    {
        protected readonly IDataContext Context = null;
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandSampleRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        internal CommandMerchantSetupRepository(string connectionString) 
        {
            Context = new DataContext<SqlConnection>(connectionString);
        }

        public async Task<int> Update(MerchantSetupDomainModel model)
        {
            int updatedId = 0;

            var parameters = new DynamicParameters();
            parameters.Add("@RefCode", model.RefCode, DbType.String, ParameterDirection.Input);

            int MerchantId = await Context.ExecuteSingleRecordReadSqlAsync<int>("SELECT TOP 1 Id FROM Merchants WHERE RefCode = @RefCode AND IsActive = 1 AND IsDeleted = 0", parameters).ConfigureAwait(false);
            parameters.Add("@MerchantId", MerchantId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@DepositCashBalance", model.DepositCashBalance, DbType.Decimal, ParameterDirection.Input);
            parameters.Add("@WithdrawCashBalance", model.WithdrawCashBalance, DbType.Decimal, ParameterDirection.Input);
            parameters.Add("@IsOnline", model.IsOnline, DbType.String, ParameterDirection.Input);
            parameters.Add("@Latitude", model.Latitude, DbType.Decimal, ParameterDirection.Input);
            parameters.Add("@Longitude", model.Longitude, DbType.Decimal, ParameterDirection.Input);
            parameters.Add("@ModifiedBy", model.ModifiedBy, DbType.String, ParameterDirection.Input);
            parameters.Add("@UpdatedId", updatedId, DbType.Int32, ParameterDirection.Output);

            var querystring = "UPDATE [dbo].[MerchantSetups]"+
            " SET LimitSetupDate = GETDATE(), DepositCashBalance = @DepositCashBalance, WithdrawCashBalance = @WithdrawCashBalance," +
            " IsOnline = @IsOnline, Latitude = @Latitude, Longitude = @Longitude, ModifiedBy = @ModifiedBy, ModifiedDateTime = GETDATE() " +
            " where MerchantId = @MerchantId";
            await Context.ExecuteWriteSqlAsync(querystring, parameters).ConfigureAwait(false);

            var queryDelete = "Delete from MerchantWithdrawalTypes where MerchantId = @MerchantId";
            await Context.ExecuteWriteSqlAsync(queryDelete, parameters).ConfigureAwait(false);

            if (model.WithdrawalTypes.Count > 0 )
            {
                foreach(var types in model.WithdrawalTypes)
                {
                    parameters = new DynamicParameters();
                    parameters.Add("@MerchantId", MerchantId, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@WithdrawalTypeId",types.Id, DbType.Int16, ParameterDirection.Input);
                    parameters.Add("@Settings",types.Value, DbType.Boolean, ParameterDirection.Input);
                    var queryMerchantWithdrawalTypes = " INSERT INTO MerchantWithdrawalTypes " +
                      "(MerchantId, WithdrawalTypeId, Settings)" +
                      "values" +
                      "(@MerchantId, @WithdrawalTypeId, @Settings)";
                    await Context.ExecuteWriteSqlAsync(queryMerchantWithdrawalTypes, parameters).ConfigureAwait(false);
                }
            }

            updatedId = await Context.ExecuteSingleRecordReadSqlAsync<int>("SELECT TOP 1 Id FROM Merchants WHERE ID = @MerchantId AND IsActive = 1 AND IsDeleted = 0", parameters).ConfigureAwait(false);
            return updatedId;
        }
    }
}