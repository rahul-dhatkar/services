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
//** Purpose   : IQueryStartupKitRepository                                                **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj G      06-20-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------
using Contesto.V2.Core.Infrastructure.Data;
using Dapper;
using FinoBank.Cola.Repository.DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FinoBank.Cola.Repository.Interfaces
{
    /// <summary>
    /// Interface Query Master Repository
    /// </summary>
    /// <seealso cref="Contesto.V2.Core.Infrastructure.Data.QueryGenericRepository{FinoBank.Cola.Repository.DomainModels.TransactionSummaryResultDomainModel}" />
    /// <seealso cref="FinoBank.Cola.Repository.Interfaces.IQueryTransactionHistoryRepository" />
    internal class QueryTransactionResultRepository : QueryGenericSqlRepository<TransactionRequestsDomainModel>, IQueryTransactionResultRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryMerchantSearchRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        internal QueryTransactionResultRepository(string connectionString) : base(connectionString)
        {

        }

        public async Task<Tuple<TransactionRequestsDomainModel>> GetTransactionDetailsById(long transactionId)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@Id", transactionId, DbType.Int64, ParameterDirection.Input);

            var queryString = "SELECT TR.Id, TR.ReferenceNumber, TR.MerchantId, TR.CustomerId, TR.CustomerLatitude, TR.CustomerLongitude, TR.TransactionTypeId, TR.RequestedDateTime, " + 
            "TR.RequestedAmount, TR.RequestCompletedDateTime, TR.TransactionStatusId, TR.ActualAmount, TR.Remarks, DATEADD(MINUTE, 30, TR.RequestedDateTime) AS Validity, " +
            "M.Name AS MerchantName, ISNULL(M.AddressLine1, '') + ' ' + ISNULL(M.AddressLine2, '') + ' ' + ISNULL(M.District, '') + ', ' + ISNULL(M.City, '') AS MerchantAddress, " +
            "(CASE WHEN(M.MerchantTypeId = 1) THEN 'Branch' WHEN(M.MerchantTypeId = 2) " +
            "THEN 'Merchant' WHEN(M.MerchantTypeId = 3) THEN 'CSP' WHEN(M.MerchantTypeId = 4) THEN 'Others' END) AS MerchantType, " +
            "M.MobileNumber AS MerchantMobile," +
            "MS.Latitude AS MerchantLatitude," +
            "MS.Longitude AS MerchantLongitude, " +
            "(CASE WHEN(TR.WithdrawalTypeId = 1) THEN 'ATM' WHEN(TR.WithdrawalTypeId = 2) THEN 'Aadhaar' WHEN(TR.WithdrawalTypeId = 3) " +
            "THEN 'FINO Bank' WHEN(TR.WithdrawalTypeId = 4) THEN 'Any' END) AS WithdrawalType, " +
            "TR.WithdrawalTypeId, TR.CreatedBy, TR.CreatedDateTime, TR.ModifiedBy, TR.ModifiedDateTime, TR.IsActive, TR.IsDeleted " +
            "FROM TransactionRequests TR " +
            "INNER JOIN Merchants M ON TR.MerchantId = M.Id " +
            "INNER JOIN MerchantSetups MS ON M.Id = MS.MerchantId " +
            "Where TR.Id = @Id";

            var results = await Context.ExecuteReadSqlAsync<TransactionRequestsDomainModel>(queryString, parameters).ConfigureAwait(false);
            return new Tuple<TransactionRequestsDomainModel>(results.FirstOrDefault());
        }

        public async Task<Tuple<TransactionRequestsDomainModel>> GetAllMobileNoByTransactionId(long transactionId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@TransactionId", transactionId, DbType.Int64, ParameterDirection.Input);
            var queryString =
            " SELECT  tr.Id as TransactionId ,m.Id as MerchantId,c.Id as CustomerId,c.Mobile as CustomerMobile,m.MobileNumber as MerchantMobile, " +
            " ( "+
            " Case when (tr.TransactionTypeId=1) then 'Deposit'  " +
            " when (tr.TransactionTypeId=2) then 'Withdrawal' " +
            " End " +
            " ) as TransactionType, " +
            " tr.ActualAmount as ActualAmount, " +
            " tr.ReferenceNumber as ReferenceNumber, " +
            " tr.Remarks as Remarks, " +
            " tr.UniqueId as UniqueId " +
            " from [TransactionRequests] tr " +
            " inner join Merchants m on m.Id = tr.MerchantId " +
            " inner join Customers c on c.Id = tr.CustomerId " +
            " where tr.Id = @TransactionId ";
            var results = await Context.ExecuteReadSqlAsync<TransactionRequestsDomainModel>(queryString, parameters).ConfigureAwait(false);
            return new Tuple<TransactionRequestsDomainModel>(results.FirstOrDefault());
        }

        public async Task<Tuple<TransactionRequestsDomainModel>> GetTransactionDetailsByUniqueId(Guid uniqueId)
        {
            long transId = 0;
            var parameters = new DynamicParameters();
            if (uniqueId != null)
            {
                parameters = new DynamicParameters();
                parameters.Add("@guid", uniqueId, DbType.Guid, ParameterDirection.Input);
                transId = await Context.ExecuteSingleRecordReadSqlAsync<long>("SELECT Id FROM TransactionRequests WHERE UniqueID = @guid", parameters).ConfigureAwait(false);
            }
                parameters = new DynamicParameters();
                parameters.Add("@Id", transId, DbType.Int64, ParameterDirection.Input);
                parameters.Add("@guid", uniqueId, DbType.Guid, ParameterDirection.Input);

                var querystring = "SELECT TR.Id, TR.ReferenceNumber, TR.MerchantId, TR.CustomerId, TR.CustomerLatitude, TR.CustomerLongitude, TR.TransactionTypeId, TR.RequestedDateTime, "+
                " TR.RequestedAmount, TR.RequestCompletedDateTime, TR.TransactionStatusId, TR.ActualAmount, TR.Remarks, DATEADD(MINUTE, 30, TR.RequestedDateTime) AS Validity, "+
                " M.Name AS MerchantName, ISNULL(M.AddressLine1, '') + ' ' + ISNULL(M.AddressLine2, '') + ' ' + ISNULL(M.District, '') + ', ' + ISNULL(M.City, '') AS MerchantAddress, " +
                " (CASE WHEN(M.MerchantTypeId = 1) THEN 'Branch' WHEN(M.MerchantTypeId = 2) " +
                " THEN 'Merchant' WHEN(M.MerchantTypeId = 3) THEN 'CSP' WHEN(M.MerchantTypeId = 4) THEN 'Others' END) AS MerchantType, " +
                " M.MobileNumber AS MerchantMobile, " +
                " MS.Latitude AS MerchantLatitude, " +
                " MS.Longitude AS MerchantLongitude, " +
                " (CASE WHEN(TR.WithdrawalTypeId = 1) THEN 'ATM' WHEN(TR.WithdrawalTypeId = 2) THEN 'Aadhaar' WHEN(TR.WithdrawalTypeId = 3) " +
                " THEN 'FINO Bank' WHEN(TR.WithdrawalTypeId = 4) THEN 'Any' END) AS WithdrawalType, " +
                " TR.WithdrawalTypeId, TR.CreatedBy, TR.CreatedDateTime, TR.ModifiedBy, TR.ModifiedDateTime, TR.IsActive, TR.IsDeleted " +
                " FROM TransactionRequests TR " +
                " INNER JOIN Merchants M ON TR.MerchantId = M.Id " +
                " INNER JOIN MerchantSetups MS ON M.Id = MS.MerchantId " +
                " Where TR.Id = @Id ";
                var results = await Context.ExecuteReadSqlAsync<TransactionRequestsDomainModel>(querystring, parameters).ConfigureAwait(false);
                return new Tuple<TransactionRequestsDomainModel>(results.FirstOrDefault());
        }

        public async Task<List<TransactionRequestsDomainModel>> GetAllMobileNoForAbove10K(long transactionId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@TransactionId", transactionId, DbType.Int64, ParameterDirection.Input);
            var results = await Context.ExecuteReadSqlAsync<TransactionRequestsDomainModel>("SELECT tr.Id as TransactionId , m.Id as MerchantId, m.MobileNumber as MerchantMobile,( Case when (tr.TransactionTypeId= 1) then 'Deposit'  when(tr.TransactionTypeId= 2) then 'Withdrawal' End ) as TransactionType,tr.ActualAmount as ActualAmount, tr.ReferenceNumber as ReferenceNumber,tr.Remarks as Remarks, tr.UniqueId as UniqueId FROM SMSlogs sl inner join Merchants m on m.Id = sl.MerchantId  inner join TransactionRequests tr on tr.Id = sl.TransactionId where sl.TransactionId = @TransactionId", parameters).ConfigureAwait(false);
            return results.ToList();
        }
    }
}

