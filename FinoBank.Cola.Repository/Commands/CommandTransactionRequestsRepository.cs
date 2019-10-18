//-------------------------------------------------------------------------------------------
//** Copyright © 2018, Fulcrum Digital                                  **
//** All rights reserved.                                                                  **
//**                                                                                       **
//** Redistribution, re-engineering or use of this code - in source                        **
//** or binary forms with or without modifications, are not                                **
//** permitted without prior written consent from appropriate person                       **
//** in Fulcrum Digital                                                 **
//**                                                                                       **
//**                                                                                       **
//** Author    : Fulcrum World Wide                                                        **
//** Created   : 06-20-18                                                                  **
//** Purpose   : CommandMasterRepository                                                   **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj G      06-20-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------
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
    internal class CommandTransactionRequestsRepository :  ICommandTransactionRequestsRepository //CommandGenericRepository<TransactionRequestsDomainModel, int>,
    {
        protected readonly IDataContext Context = null;
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandSampleRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        internal CommandTransactionRequestsRepository(string connectionString) //: base(connectionString)
        {
            Context = new DataContext<SqlConnection>(connectionString);
        }

        public async Task<int> Create(TransactionRequestsDomainModel model)
        {
            int insertedId = 0;
            var parameters = new DynamicParameters();
            parameters.Add("@ReferenceNumber", model.ReferenceNumber, DbType.String, ParameterDirection.Input);
            parameters.Add("@MerchantId", model.MerchantId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@CustomerId", model.CustomerId, DbType.Int64, ParameterDirection.Input);
            parameters.Add("@CustomerLatitude", model.CustomerLatitude, DbType.Decimal, ParameterDirection.Input);
            parameters.Add("@CustomerLongitude", model.CustomerLongitude, DbType.Decimal, ParameterDirection.Input);
            parameters.Add("@MerchantLatitude", model.MerchantLatitude, DbType.Decimal, ParameterDirection.Input);
            parameters.Add("@MerchantLongitude", model.MerchantLongitude, DbType.Decimal, ParameterDirection.Input);
            parameters.Add("@TransactionTypeId", model.TransactionTypeId, DbType.Int16, ParameterDirection.Input);
            parameters.Add("@WithdrawalTypeId", model.WithdrawalTypeId, DbType.Int16, ParameterDirection.Input);
            parameters.Add("@RequestedAmount", model.RequestedAmount, DbType.Int64, ParameterDirection.Input);
            parameters.Add("@TransactionStatusId", model.TransactionStatusId, DbType.Int16, ParameterDirection.Input);
            parameters.Add("@ActualAmount", model.ActualAmount, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@Remarks", model.Remarks, DbType.String, ParameterDirection.Input);
            parameters.Add("@CreatedBy", model.CustomerId, DbType.String, ParameterDirection.Input);
            parameters.Add("@IsActive", true, DbType.String, ParameterDirection.Input);
            parameters.Add("@IsDeleted", false, DbType.String, ParameterDirection.Input);
            parameters.Add("@UniqueId",Guid.NewGuid(), DbType.Guid, ParameterDirection.Input);

            var queryFeedbackInsert = "INSERT INTO [dbo].[TransactionRequests](ReferenceNumber, MerchantId, CustomerId, CustomerLatitude, CustomerLongitude,MerchantLatitude, MerchantLongitude,TransactionTypeId, WithdrawalTypeId," +
            " RequestedDateTime, RequestedAmount, RequestCompletedDateTime,TransactionStatusId, ActualAmount,Remarks ,CreatedBy ,CreatedDateTime , ModifiedBy ,ModifiedDateTime , IsActive, IsDeleted ,UniqueId) " +
            " values (@ReferenceNumber, @MerchantId, @CustomerId, @CustomerLatitude, @CustomerLongitude, @MerchantLatitude, @MerchantLongitude, @TransactionTypeId, @WithdrawalTypeId," +
            " GETDATE(), @RequestedAmount, NULL,@TransactionStatusId, @ActualAmount,@Remarks ,@CreatedBy ,GETDATE(), NULL ,NULL , @IsActive, @IsDeleted ,@UniqueId) ";

            await Context.ExecuteWriteSqlAsync(queryFeedbackInsert, parameters).ConfigureAwait(false);

            insertedId = await Context.ExecuteSingleRecordReadSqlAsync<int>("SELECT Id FROM TransactionRequests WHERE UniqueId = @UniqueId AND ReferenceNumber = @ReferenceNumber AND IsActive = 1 AND IsDeleted = 0", parameters).ConfigureAwait(false);

            if (insertedId > 0 && model.TransactionTypeId == 2 && model.MerchantId > 0)
            {
                int withdrawCashBalance = 0;
                int amount = withdrawCashBalance - model.RequestedAmount;
                parameters = new DynamicParameters();
                parameters.Add("@WithdrawCashBalance",amount, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@ModifiedBy", model.ModifiedBy, DbType.String, ParameterDirection.Input);
                parameters.Add("@MerchantId", model.MerchantId, DbType.Int32, ParameterDirection.Input);
                await Context.ExecuteWriteSqlAsync("UPDATE MerchantSetups set LimitSetupDate = GETDATE() ,WithdrawCashBalance = @WithdrawCashBalance , ModifiedBy = @ModifiedBy , ModifiedDateTime = GETDATE() WHERE MerchantId = @MerchantId ", parameters).ConfigureAwait(false);
            }

            return insertedId;
        }

        public Task<int> Update(TransactionRequestsDomainModel model)
        {
            throw new NotImplementedException();
        }
    }
}

        

		
	
	