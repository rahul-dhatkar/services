using Contesto.V2.Core.Infrastructure.Data.Interfaces;
using FinoBank.Cola.Manager.ViewModels;
using FinoBank.Cola.Repository.DomainModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinoBank.Cola.Repository.Interfaces
{
    public interface IQueryTransactionSummaryRepository : IQueryGenericRepository<TransactionSummaryResultDomainModel>
    {
        /// <summary>
        /// Gets the transaction history data with paging.
        /// </summary>
        /// <param name="refCode">The reference code.</param>
        /// <param name="type">The type.</param>
        /// <param name="mobile">The mobile.</param>
        /// <param name="transactionStatusId">The transaction status identifier.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="sortColumn">The sort column.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="searchText">The search text.</param>
        /// <param name="totalCount">The total count.</param>
        /// <returns></returns>
        Task<Tuple<List<TransactionSummaryResultDomainModel>, int>> GetCustomerTransactionSummaryDataWithPaging(string customerType, string customerRefCode, string customerMobile, int transactionStatusId, DateTime? fromDate, DateTime? toDate, int statementType, string sortColumn, string sortDirection, int? pageIndex, int? pageSize, string searchText, int? totalCount);

        Task<Tuple<List<TransactionSummaryResultDomainModel>, int>> GetMerchantTransactionSummaryDataWithPaging(string refCode, int merchantId, int transactionStatusId, DateTime? fromDate, DateTime? toDate, string sortColumn, string sortDirection, int? pageIndex, int? pageSize, string searchText, int? totalCount);
    
    }
}