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
//** Created   : 06-26-18                                                                  **
//** Purpose   :  QueryStartupKitRepository                                                **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Nam Team      06-26-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------
using Contesto.V2.Core.Infrastructure.Data;
using Dapper;
using FinoBank.Cola.Repository.DomainModels;
using FinoBank.Cola.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FinoBank.Cola.Repository.Queries
{
    /// <summary>
    /// Query Master Repository
    /// </summary>
    /// <seealso cref="QueryGenericRepository{StartupKitDomainModel}" />
    /// <seealso cref="IQuerySampleRepository" />
    internal class QuerySampleRepository : QueryGenericRepository<SampleDomainModel>, IQuerySampleRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuerySampleRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        internal QuerySampleRepository(string connectionString) : base(connectionString)
        {
        }

        /// <summary>
        /// Gets the master grid summary data with paging.
        /// </summary>
        /// <param name="typeId">The type identifier.</param>
        /// <param name="SortColumn">The sort column.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="searchTxt">The search text.</param>
        /// <returns></returns>
        public async Task<Tuple<List<SampleDomainModel>, int>> GetGridSummaryDataWithPaging(int typeId, string SortColumn, string SortDirection, int? pageIndex, int? pageSize, string searchTxt)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@TypeId", typeId, DbType.Int16, ParameterDirection.Input);
            parameters.Add("@SearchText", searchTxt, DbType.String, ParameterDirection.Input);
            parameters.Add("@SortColumn", SortColumn, DbType.String, ParameterDirection.Input);
            parameters.Add("@SortDirection", SortDirection, DbType.String, ParameterDirection.Input);
            parameters.Add("@PageIndex", pageIndex, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PageSize", pageSize, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@TotalRecords", 0, DbType.Int32, ParameterDirection.Output);
            var results = await Context.ExecuteReadProcedureAsync<SampleDomainModel>("GetGridSummaryDataWithPaging", parameters).ConfigureAwait(false);

            var totalRecords = parameters.Get<Int32>("@TotalRecords");
            return new Tuple<List<SampleDomainModel>, int>(results.ToList(), totalRecords);
        }

        /// <summary>
        /// Determines whether the specified model is exist.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<bool> IsExist(SampleDomainModel model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", model.Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@Value", model.Name, DbType.String, ParameterDirection.Input);
            var results = await Context.ExecuteReadProcedureAsync<bool>("IsMasterExist", parameters).ConfigureAwait(false);
            return results.FirstOrDefault();
        }
    }
}