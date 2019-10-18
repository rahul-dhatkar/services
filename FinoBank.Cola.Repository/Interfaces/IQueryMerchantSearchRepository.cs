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
using Contesto.V2.Core.Infrastructure.Data.Interfaces;
using FinoBank.Cola.Repository.DomainModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinoBank.Cola.Repository.Interfaces
{
    /// <summary>
    /// Interface Query Master Repository
    /// </summary>
    /// <seealso cref="Contesto.V2.Core.Infrastructure.Data.Interfaces.IQueryGenericRepository{FinoBank.Cola.Repository.DomainModels.MerchantSearchResultDomainModel}" />
    /// <seealso cref="IQueryGenericRepository{MerchantSearchResultDomainModel}" />
    public interface IQueryMerchantSearchRepository : IQueryGenericRepository<MerchantSearchResultDomainModel>
    {
        /// <summary>
        /// Gets the merchants with paging.
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
        Task<Tuple<List<MerchantSearchResultDomainModel>, int>> GetMerchantSearchDataWithPaging(string customerType, string customerRefCode, string customerMobile, int amount, double currentLatitude, double currentLongitude, int byMerchantTypeId, int byTransactionTypeId, int byWithdrawalTypeId, int? distance, string sortColumn, string sortDirection, int? pageIndex, int? pageSize, string searchText, int? totalCount);
    }
}