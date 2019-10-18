using Contesto.V2.Core.Data;
using Contesto.V2.Core.Data.Interfaces;
using Contesto.V2.Core.Infrastructure.Data;
using Dapper;
using FinoBank.Cola.Repository.DomainModels;
using FinoBank.Cola.Repository.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace FinoBank.Cola.Repository.Commands
{
    internal class CommandActivityRepository : ICommandActivityRepository 
    {
        protected readonly IDataContext Context = null;
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandActivityRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        internal CommandActivityRepository(string connectionString) 
        {
            Context = new DataContext<SqlConnection>(connectionString);
        }

        /// <summary>
        /// Creates the activity log.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task CreateActivityLog(ActivityLogDomainModel model)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@ActionCode", model.ActionCode, DbType.String, ParameterDirection.Input);
            parameters.Add("@NewJsonData", model.NewJsonData, DbType.String, ParameterDirection.Input);
            parameters.Add("@OldJsonData", model.OldJsonData, DbType.String, ParameterDirection.Input);
            parameters.Add("@CreatedBy", model.CreatedBy, DbType.String, ParameterDirection.Input);
         
            await Context.ExecuteWriteSqlAsync("INSERT INTO dbo.ActivityLogs (ActionCode,NewJsonData,OldJsonData,CreatedBy,CreatedDateTime) values (@ActionCode,@NewJsonData,@OldJsonData,@CreatedBy, GETDATE()) ", parameters).ConfigureAwait(false);
        } 
    }
}