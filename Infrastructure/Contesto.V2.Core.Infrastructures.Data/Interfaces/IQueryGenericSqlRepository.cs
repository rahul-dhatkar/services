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
//** Created   : 02-Jul-18                                                                 **
//** Purpose   : IQueryGenericRepository                                                   **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Nam Team      02-07-18     Created                                                   **
//**                                                                                       **
//-------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contesto.V2.Core.Infrastructure.Data.Interfaces
{
    /// <summary>
    /// IQueryGenericRepository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IQueryGenericSqlRepository<T>
    {
        /// <summary>
        /// Gets all with paging.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="SortColumn">The sort column.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="searchTxt">The search text.</param>
        /// <returns></returns>
        Task<Tuple<List<T>, int>> GetAllWithPaging(string sql, string SortColumn, string SortDirection, int? pageIndex, int? pageSize, string searchTxt);

        /// <summary>
        /// Gets the grid summary data with paging.
        /// </summary>
        /// <typeparam name="TSummary">The type of the summary.</typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="SortColumn">The sort column.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="searchTxt">The search text.</param>
        /// <returns></returns>
        Task<Tuple<List<TSummary>, int>> GetGridSummaryDataWithPaging<TSummary>(string sql, string SortColumn, string SortDirection, int? pageIndex, int? pageSize, string searchTxt);

        /// <summary>
        /// Gets all data.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="searchTxt">The search text.</param>
        /// <returns></returns>
        Task<List<T>> GetAllData(string sql, string searchTxt = null);

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<List<T>> GetById(string sql, long id);
        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <typeparam name="IModel">The type of the model.</typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<IModel> GetById<IModel>(string sql, long id);
    }
}