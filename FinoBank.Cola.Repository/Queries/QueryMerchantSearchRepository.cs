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
using Contesto.V2.Core.Infrastructure.Data.Interfaces;
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
    /// <seealso cref="Contesto.V2.Core.Infrastructure.Data.QueryGenericRepository{FinoBank.Cola.Repository.DomainModels.MerchantSearchResultDomainModel}" />
    /// <seealso cref="FinoBank.Cola.Repository.Interfaces.IQueryMerchantSearchRepository" />
    /// <seealso cref="IQueryGenericRepository{MerchantSearchResultDomainModel}" />
    internal class QueryMerchantSearchRepository : QueryGenericRepository<MerchantSearchResultDomainModel>, IQueryMerchantSearchRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryMerchantSearchRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        internal QueryMerchantSearchRepository(string connectionString) : base(connectionString)
        {
        }

        /// <summary>
        /// Gets the merchant data with paging.
        /// </summary>
        /// <param name="customerType">Type of the customer.</param>
        /// <param name="customerRefCode">The customer reference code.</param>
        /// <param name="customerMobile">The customer mobile.</param>
        /// <param name="currentLatitude">The current latitude.</param>
        /// <param name="currentLongitude">The current longitude.</param>
        /// <param name="byRating">The by rating.</param>
        /// <param name="byDistance">The by distance.</param>
        /// <param name="byBankingType">Type of the by banking.</param>
        /// <param name="byOnlyBranchesOrMerchant">The by only branches or merchant.</param>
        /// <param name="txn">The TXN.</param>
        /// <param name="byWithdrawalType">Type of the by withdrawal.</param>
        /// <param name="sortColumn">The sort column.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="searchText">The search text.</param>
        /// <param name="totalCount">The total count.</param>
        /// <returns></returns>
        public async Task<Tuple<List<MerchantSearchResultDomainModel>, int>> GetMerchantSearchDataWithPaging(string customerType, string customerRefCode, string customerMobile, int amount, double currentLatitude, double currentLongitude, int byMerchantTypeId, int byTransactionTypeId, int byWithdrawalTypeId, int? distance, string sortColumn, string sortDirection, int? pageIndex, int? pageSize, string searchText, int? totalCount)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@CustomerType", customerType, DbType.String, ParameterDirection.Input);
            parameters.Add("@CustomerRefCode", customerRefCode, DbType.String, ParameterDirection.Input);
            parameters.Add("@CustomerMobile", customerMobile, DbType.String, ParameterDirection.Input);
            parameters.Add("@Amount", amount, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@CurrentLatitude", currentLatitude, DbType.Decimal, ParameterDirection.Input);
            parameters.Add("@CurrentLongitude", currentLongitude, DbType.Decimal, ParameterDirection.Input);
            parameters.Add("@ByMerchantTypeId", byMerchantTypeId, DbType.Int16, ParameterDirection.Input);
            parameters.Add("@ByTransactionTypeId", byTransactionTypeId, DbType.Int16, ParameterDirection.Input);
            parameters.Add("@ByWithdrawalTypeId", byWithdrawalTypeId, DbType.Int16, ParameterDirection.Input);
            parameters.Add("@Distance", distance, DbType.Int16, ParameterDirection.Input);
            parameters.Add("@SortColumn", sortColumn, DbType.String, ParameterDirection.Input);
            parameters.Add("@SortDirection", sortDirection, DbType.String, ParameterDirection.Input);
            parameters.Add("@PageIndex", pageIndex, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PageSize", pageSize, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@SearchText", searchText, DbType.String, ParameterDirection.Input);
            parameters.Add("@TotalCount", totalCount, DbType.Int32, ParameterDirection.Input);

            parameters.Add("@TotalRecords", 0, DbType.Int32, ParameterDirection.Output);

            IEnumerable<MerchantSearchResultDomainModel> results = await Context.ExecuteReadProcedureAsync<MerchantSearchResultDomainModel>("GetMerchantSearchDataWithPaging", parameters).ConfigureAwait(false);

            List<MerchantSearchResultDomainModel> a = new List<MerchantSearchResultDomainModel>();
            List<MerchantSearchResultDomainModel> b = new List<MerchantSearchResultDomainModel>();
            int count = 0;
            foreach (var item in results)
            {
                if (item.MerchantTypeId == 1 && count == 0)
                {
                    a.Add(item);
                    item.Rating = null;
                    count++;
                }
                else
                {
                    b.Add(item);
                }
            }

            a.AddRange(b);

            var totalRecords = parameters.Get<Int32>("@TotalRecords");
            return new Tuple<List<MerchantSearchResultDomainModel>, int>(a.ToList(), totalRecords);
        }
    }
}