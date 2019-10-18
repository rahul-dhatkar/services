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
using FinoBank.Cola.Manager.ViewModels;
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
    internal class QueryTransactionSummaryRepository : QueryGenericRepository<TransactionSummaryResultDomainModel>, IQueryTransactionSummaryRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryMerchantSearchRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        internal QueryTransactionSummaryRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<Tuple<List<TransactionSummaryResultDomainModel>, int>> GetCustomerTransactionSummaryDataWithPaging(string customerType, string customerRefCode, string customerMobile, int transactionStatusId, DateTime? fromDate, DateTime? toDate, int statementType, string sortColumn, string sortDirection, int? pageIndex, int? pageSize, string searchText, int? totalCount)
        {
            if(statementType == 1)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@CustomerType", customerType, DbType.String, ParameterDirection.Input);
                parameters.Add("@CustomerRefCode", customerRefCode, DbType.String, ParameterDirection.Input);
                parameters.Add("@CustomerMobile", customerMobile, DbType.String, ParameterDirection.Input);
                parameters.Add("@TransactionStatusId", transactionStatusId, DbType.Int16, ParameterDirection.Input);
                parameters.Add("@FromDate", fromDate, DbType.DateTime, ParameterDirection.Input);
                parameters.Add("@ToDate", toDate, DbType.DateTime, ParameterDirection.Input);
                parameters.Add("@StatementType", statementType, DbType.Int16, ParameterDirection.Input);
                parameters.Add("@SortColumn", sortColumn, DbType.String, ParameterDirection.Input);
                parameters.Add("@SortDirection", sortDirection, DbType.String, ParameterDirection.Input);
                parameters.Add("@PageIndex", pageIndex, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@PageSize", pageSize, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@SearchText", searchText, DbType.String, ParameterDirection.Input);
                parameters.Add("@TotalCount", totalCount, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@TotalRecords", 0, DbType.Int32, ParameterDirection.Output);
                var results = await Context.ExecuteReadSqlAsync<TransactionSummaryResultDomainModel>("SELECT TOP (10)* FROM vwCustomerTransactions WHERE TransactionStatusId = @TransactionStatusId AND CustomerMobile = @CustomerMobile AND IsActive=1 AND IsDeleted=0 ORDER BY RequestedDateTime desc", parameters).ConfigureAwait(false);
                var totalRecords = results.Count();//parameters.Get<Int32>("@TotalRecords");
                return new Tuple<List<TransactionSummaryResultDomainModel>, int>(results.ToList(), totalRecords);
            }
            else
            {
                var parameters = new DynamicParameters();
                parameters.Add("@CustomerType", customerType, DbType.String, ParameterDirection.Input);
                parameters.Add("@CustomerRefCode", customerRefCode, DbType.String, ParameterDirection.Input);
                parameters.Add("@CustomerMobile", customerMobile, DbType.String, ParameterDirection.Input);
                parameters.Add("@TransactionStatusId", transactionStatusId, DbType.Int16, ParameterDirection.Input);
                parameters.Add("@FromDate", fromDate, DbType.DateTime, ParameterDirection.Input);
                parameters.Add("@ToDate", toDate, DbType.DateTime, ParameterDirection.Input);
                parameters.Add("@StatementType", statementType, DbType.Int16, ParameterDirection.Input);
                parameters.Add("@SortColumn", sortColumn, DbType.String, ParameterDirection.Input);
                parameters.Add("@SortDirection", sortDirection, DbType.String, ParameterDirection.Input);
                parameters.Add("@PageIndex", pageIndex, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@PageSize", pageSize, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@SearchText", searchText, DbType.String, ParameterDirection.Input);
                parameters.Add("@TotalCount", totalCount, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@TotalRecords", 0, DbType.Int32, ParameterDirection.Output);
                var querystring = "SELECT Id, ReferenceNumber, TransactionStatus, TransactionType , MerchantName, MerchantAddress ," +
                " RequestedDateTime ,Validity, RequestedAmount, ActualAmount , Remarks, MerchantMobile, " +
                " MerchantId ,MerchantLatitude, MerchantLongitude ,CustomerId, CustomerLatitude, CustomerLongitude , " +
                " TransactionTypeId ,TransactionStatusId, WithdrawalTypeId, CustomerName ,CustomerMobile,CustomerType , " +
                " RefCode ,MerchantTypeId,State ,Country ,PinCode,Email ,Telephone ,Extension,Fax ,IsActive ," +
                " IsDeleted, ModifiedDateTime, WithdrawalType, RequestCompletedDateTime , CustomerRefCode , MerchantType, UniqueId " +
                " FROM vwCustomerTransactions WHERE TransactionStatusId = @TransactionStatusId " +
                " AND RequestedDateTime between @FromDate AND @ToDate AND CustomerMobile=@CustomerMobile AND IsActive=1 AND IsDeleted=0 " +
                " ORDER BY RequestedDateTime desc";
                var results = await Context.ExecuteReadSqlAsync<TransactionSummaryResultDomainModel>(querystring, parameters).ConfigureAwait(false);
                var totalRecords = results.Count(); //parameters.Get<Int32>("@TotalRecords");
                return new Tuple<List<TransactionSummaryResultDomainModel>, int>(results.ToList(), totalRecords);
            }
           
        }

        public async Task<Tuple<List<TransactionSummaryResultDomainModel>, int>> GetMerchantTransactionSummaryDataWithPaging(string refCode, int merchantId, int transactionStatusId, DateTime? fromDate, DateTime? toDate, string sortColumn, string sortDirection, int? pageIndex, int? pageSize, string searchText, int? totalCount)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@RefCode", refCode, DbType.String, ParameterDirection.Input);
            parameters.Add("@MerchantId", merchantId, DbType.String, ParameterDirection.Input);
            parameters.Add("@TransactionStatusId", transactionStatusId, DbType.Int16, ParameterDirection.Input);
            parameters.Add("@FromDate", fromDate, DbType.DateTime, ParameterDirection.Input);
            parameters.Add("@ToDate", toDate, DbType.DateTime, ParameterDirection.Input);
            parameters.Add("@SortColumn", sortColumn, DbType.String, ParameterDirection.Input);
            parameters.Add("@SortDirection", sortDirection, DbType.String, ParameterDirection.Input);
            parameters.Add("@PageIndex", pageIndex, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PageSize", pageSize, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@SearchText", searchText, DbType.String, ParameterDirection.Input);
            parameters.Add("@TotalCount", totalCount, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@TotalRecords", 0, DbType.Int32, ParameterDirection.Output);
            if (transactionStatusId != 0)
            {
              
                if (!String.IsNullOrEmpty(fromDate.ToString()) && !String.IsNullOrEmpty(toDate.ToString()))
                {
                    var querystring = "SELECT ReferenceNumber,MerchantId,CustomerId,CustomerLatitude,CustomerLongitude,TransactionTypeId,WithdrawalTypeId,RequestedDateTime,RequestedAmount,RequestCompletedDateTime, " +
                        " TransactionStatusId,ActualAmount,Remarks,Validity,MerchantName,MerchantAddress,MerchantType,MerchantMobile,TransactionStatus,TransactionType,WithdrawalType,CustomerMobile, " +
                        " CustomerType,CustomerName,MerchantLatitude,MerchantLongitude,CreatedDateTime,CreatedBy,ModifiedDateTime,ModifiedBy,IsActive,IsDeleted FROM [dbo].[vwMerchantTransactions] " +
                        " where TransactionStatusId = @TransactionStatusId " +
                        " AND RequestedDateTime >  @FromDate "+
                        " AND RequestedDateTime < @ToDate "+
                        " AND RefCode = @RefCode "+
                        " AND IsActive = 1 AND IsDeleted = 0 " +
                        " ORDER BY RequestedDateTime DESC ";
                    var results = await Context.ExecuteReadSqlAsync<TransactionSummaryResultDomainModel>(querystring, parameters).ConfigureAwait(false);
                    var totalRecords = results.Count();
                    return new Tuple<List<TransactionSummaryResultDomainModel>, int>(results.ToList(), totalRecords);
                }
                else
                {
                    var querystring = " SELECT ReferenceNumber,MerchantId,CustomerId,CustomerLatitude,CustomerLongitude,TransactionTypeId,WithdrawalTypeId,RequestedDateTime,RequestedAmount,RequestCompletedDateTime, " +
                        "  TransactionStatusId,ActualAmount,Remarks,Validity,MerchantName,MerchantAddress,MerchantType,MerchantMobile,TransactionStatus,TransactionType,WithdrawalType,CustomerMobile, " +
                        "  CustomerType,CustomerName,MerchantLatitude,MerchantLongitude,CreatedDateTime,CreatedBy,ModifiedDateTime,ModifiedBy,IsActive,IsDeleted FROM [dbo].[vwMerchantTransactions] " +
                        "  where TransactionStatusId = @TransactionStatusId " +
                        "  AND RefCode = @RefCode " +
                        "  AND IsActive = 1 AND IsDeleted = 0 " +
                        "  ORDER BY RequestedDateTime DESC ";
                    var results = await Context.ExecuteReadSqlAsync<TransactionSummaryResultDomainModel>(querystring, parameters).ConfigureAwait(false);
                    var totalRecords = results.Count();
                    return new Tuple<List<TransactionSummaryResultDomainModel>, int>(results.ToList(), totalRecords);
                }
            }
            /*********************TRANSACTION SUMMARY FOR ABOVE 10K********************/
            else
            {
                if (!String.IsNullOrEmpty(fromDate.ToString()) && !String.IsNullOrEmpty(toDate.ToString()))
                {
                    var querystring = "SELECT ReferenceNumber,MerchantId,CustomerId,CustomerLatitude,CustomerLongitude,TransactionTypeId,WithdrawalTypeId,RequestedDateTime,RequestedAmount,RequestCompletedDateTime, " +
                      " TransactionStatusId,ActualAmount,Remarks,Validity,MerchantName,MerchantAddress,MerchantType,MerchantMobile,TransactionStatus,TransactionType,WithdrawalType,CustomerMobile, " +
                      " CustomerType,CustomerName,MerchantLatitude,MerchantLongitude,CreatedDateTime,CreatedBy,ModifiedDateTime,ModifiedBy,IsActive,IsDeleted FROM [dbo].[vwMerchantTransactions] " +
                      " where TransactionStatusId = @TransactionStatusId " +
                      " AND RequestedDateTime >  @FromDate " +
                      " AND RequestedDateTime < @ToDate " +
                      " AND MerchantId = @MerchantId " +
                      " AND IsActive = 1 AND IsDeleted = 0 " +
                      " ORDER BY RequestedDateTime DESC ";
                    var results = await Context.ExecuteReadSqlAsync<TransactionSummaryResultDomainModel>(querystring, parameters).ConfigureAwait(false);
                    var totalRecords = results.Count();
                    return new Tuple<List<TransactionSummaryResultDomainModel>, int>(results.ToList(), totalRecords);
                }
                else
                {
                    var querystring = "SELECT ReferenceNumber,MerchantId,CustomerId,CustomerLatitude,CustomerLongitude,TransactionTypeId,WithdrawalTypeId,RequestedDateTime,RequestedAmount,RequestCompletedDateTime, " +
                      " TransactionStatusId,ActualAmount,Remarks,Validity,MerchantName,MerchantAddress,MerchantType,MerchantMobile,TransactionStatus,TransactionType,WithdrawalType,CustomerMobile, " +
                      " CustomerType,CustomerName,MerchantLatitude,MerchantLongitude,CreatedDateTime,CreatedBy,ModifiedDateTime,ModifiedBy,IsActive,IsDeleted FROM [dbo].[vwMerchantTransactions] " +
                      " where TransactionStatusId = @TransactionStatusId " +
                      " AND  MerchantId = @MerchantId " +
                      " AND IsActive = 1 AND IsDeleted = 0 " +
                      " ORDER BY RequestedDateTime DESC ";
                    var results = await Context.ExecuteReadSqlAsync<TransactionSummaryResultDomainModel>(querystring, parameters).ConfigureAwait(false);
                    var totalRecords = results.Count();
                    return new Tuple<List<TransactionSummaryResultDomainModel>, int>(results.ToList(), totalRecords);
                }
            }

        }
        
    }
}