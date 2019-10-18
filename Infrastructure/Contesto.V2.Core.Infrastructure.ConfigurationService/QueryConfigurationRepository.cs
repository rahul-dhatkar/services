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
//** Created   : 27-Jun-18                                                                 **
//** Purpose   : Configuration Repository                                                  **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj G      27-06-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Contesto.V2.Core.Data;
using Contesto.V2.Core.Data.Interfaces;
using Contesto.V2.Core.Infrastructure.ConfigurationService.Dtos.DomainModels;
using Contesto.V2.Core.Infrastructure.ConfigurationService.Interfaces;
using Contesto.V2.Core.Infrastructure.Data;
using Dapper;

namespace Contesto.V2.Core.Infrastructure.ConfigurationService
{
    /// <summary>
    /// Query Configuration Repository
    /// </summary>
    internal class QueryConfigurationRepository : IQueryConfigurationRepository   //QueryGenericSqlRepository<ConfigurationSettingDomainModel>,
    {
        protected readonly IDataContext Context = null;
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryConfigurationRepository" /> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public QueryConfigurationRepository(string connectionString) //: base(connectionString)
        {
            Context = new DataContext<SqlConnection>(connectionString);
        }

        public async Task<List<ConfigurationSettingDomainModel>> GetAllConfiguration(string searchTxt = null)
        {
            string sql = "";
            var parameters = new DynamicParameters();
            parameters.Add("@SearchText", searchTxt, DbType.String, ParameterDirection.Input);
            var result = await Context.ExecuteReadSqlAsync< ConfigurationSettingDomainModel>("SELECT Environment,[Key], Value, IsActive FROM ConfigurationSettings WHERE IsDeleted = 0 AND ( [Key]  LIKE '%' + @SearchText + '%' OR @SearchText IS NULL)", parameters).ConfigureAwait(false);
            return new List<ConfigurationSettingDomainModel>(result); 
        }

        public Task<List<ConfigurationSettingDomainModel>> GetAllData(string sql, string searchTxt = null)
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<List<ConfigurationSettingDomainModel>, int>> GetAllWithPaging(string sql, string SortColumn, string SortDirection, int? pageIndex, int? pageSize, string searchTxt)
        {
            throw new NotImplementedException();
        }

        public Task<List<ConfigurationSettingDomainModel>> GetById(string sql, long id)
        {
            throw new NotImplementedException();
        }

        public Task<IModel> GetById<IModel>(string sql, long id)
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<List<TSummary>, int>> GetGridSummaryDataWithPaging<TSummary>(string sql, string SortColumn, string SortDirection, int? pageIndex, int? pageSize, string searchTxt)
        {
            throw new NotImplementedException();
        }
    }
}

