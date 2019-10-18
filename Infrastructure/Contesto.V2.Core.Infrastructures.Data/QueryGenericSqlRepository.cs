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
//** Purpose   : QueryGenericRepository                                                    **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Nam Team      02-07-18     Created                                                   **
//**                                                                                       **
//-------------------------------------------------------------------------------------------

using Contesto.V2.Core.Infrastructure.Data.Helpers;
using Contesto.V2.Core.Infrastructure.Data.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Contesto.V2.Core.Data.Interfaces;
using Contesto.V2.Core.Data;

namespace Contesto.V2.Core.Infrastructure.Data
{
    /// <summary>
    /// Query Generic Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Contesto.V2.Core.Infrastructure.Data.Interfaces.IQueryGenericRepository{T}" />
    public abstract class QueryGenericSqlRepository<T> : IQueryGenericSqlRepository<T>
    {
        /// <summary>
        /// The context
        /// </summary>
        protected readonly IDataContext Context = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryGenericRepository{T}"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        protected QueryGenericSqlRepository(string connectionString)
        {
            Context = new DataContext<SqlConnection>(connectionString);
        }

        /// <summary>
        /// Gets all data.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="searchTxt">The search text.</param>
        /// <returns></returns>
        public async Task<List<T>> GetAllData(string sql, string searchTxt = null)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@SearchText", searchTxt, DbType.String, ParameterDirection.Input);
            var result = await Context.ExecuteReadSqlAsync<T>(sql, parameters).ConfigureAwait(false);
            return result.ToList();
        }

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
        public async Task<Tuple<List<T>, int>> GetAllWithPaging(string sql, string SortColumn, string SortDirection, int? pageIndex, int? pageSize, string searchTxt)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@SearchText", searchTxt, DbType.String, ParameterDirection.Input);
            parameters.Add("@SortColumn", SortColumn, DbType.String, ParameterDirection.Input);
            parameters.Add("@SortDirection", SortDirection, DbType.String, ParameterDirection.Input);
            parameters.Add("@PageIndex", pageIndex, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PageSize", pageSize, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@TotalRecords", searchTxt, DbType.Int32, ParameterDirection.Output);

            var results = await Context.ExecuteReadSqlAsync<T>(sql, parameters).ConfigureAwait(false); ;
            var totalRecords = parameters.Get<Int32>("@TotalRecords");
            return new Tuple<List<T>, int>(results.ToList(), totalRecords);
        }


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
        public async Task<Tuple<List<TSummary>, int>> GetGridSummaryDataWithPaging<TSummary>(string sql, string SortColumn, string SortDirection, int? pageIndex, int? pageSize, string searchTxt)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@SearchText", searchTxt, DbType.String, ParameterDirection.Input);
            parameters.Add("@SortColumn", SortColumn, DbType.String, ParameterDirection.Input);
            parameters.Add("@SortDirection", SortDirection, DbType.String, ParameterDirection.Input);
            parameters.Add("@PageIndex", pageIndex, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PageSize", pageSize, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@TotalRecords", 0, DbType.Int32, ParameterDirection.Output);

            var results = await Context.ExecuteReadSqlAsync<TSummary>(sql, parameters).ConfigureAwait(false); ;

            var totalRecords = parameters.Get<Int32>("@TotalRecords");
            return new Tuple<List<TSummary>, int>(results.ToList(), totalRecords);
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<List<T>> GetById(string sql, long id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id, DbType.Int64, ParameterDirection.Input);
            var result = await Context.ExecuteReadSqlAsync<T>(sql, parameters).ConfigureAwait(false);
            return result.ToList();
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <typeparam name="IModel">The type of the model.</typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<IModel> GetById<IModel>(string sql, long id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id, DbType.Int64, ParameterDirection.Input);
            var result = await Context.ExecuteReadSqlAsync<IModel>(sql, parameters).ConfigureAwait(false);
            return result.FirstOrDefault();
        }
    }
}