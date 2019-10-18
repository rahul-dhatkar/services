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
//** Created   : 08-06-18                                                                  **
//** Purpose   : CommandLoggerRepository                                                    **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj G      08-06-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------


using Contesto.V2.Core.Infrastructure.LoggerService.Dtos;
using Contesto.V2.Core.Infrastructure.LoggerService.Interfaces;
using Contesto.V2.Core.Infrastructure.Data;
using Newtonsoft.Json;
using Dapper;
using System.Data;
using System.Threading.Tasks;
using Contesto.V2.Core.Data.Interfaces;
using System.Data.SqlClient;
using Contesto.V2.Core.Data;
using System;

namespace Contesto.V2.Core.Infrastructure.LoggerService
{
    /// <summary>
    /// QueryLoggerRepository
    /// </summary>
    /// <seealso cref="Contesto.V2.Core.Infrastructure.LoggerService.Interfaces.ICommandLoggerRepository" />
    /// <seealso cref="CommandGenericRepository{LoggerDomainModel, System.Int64}" />
    /// <seealso cref="ICommandLoggerRepository" />
    internal class CommandLoggerRepository : ICommandLoggerRepository
    {
        protected readonly IDataContext Context = null;
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandLoggerRepository" /> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public CommandLoggerRepository(string connectionString) //: base(connectionString)
        {
            Context = new DataContext<SqlConnection>(connectionString);
        }
        
        public async Task<long> Create(LoggerDomainModel model)
        {
            var jsonModel = JsonConvert.SerializeObject(model);
            int insertedId = 0;

            var parameters = new DynamicParameters();
            parameters.Add("@EventId", model.EventId, DbType.Int16, ParameterDirection.Input);
            parameters.Add("@LogLevel", model.LogLevel, DbType.String, ParameterDirection.Input);
            parameters.Add("@Message", model.Message, DbType.String, ParameterDirection.Input);
            parameters.Add("@InnerExceptionMessage", model.InnerExceptionMessage, DbType.String, ParameterDirection.Input);
            parameters.Add("@StackTrace", model.StackTrace, DbType.String, ParameterDirection.Input);
            parameters.Add("@InsertedId", insertedId, DbType.Int16, ParameterDirection.Output);

            var queryString = " INSERT INTO dbo.Loggers (EventId, LogLevel , Message, InnerExceptionMessage, StackTrace, LogDateTime ) " +
            " values(@EventId, @LogLevel, @Message, @InnerExceptionMessage,@StackTrace,GETDATE())";

            insertedId = await Context.ExecuteWriteSqlAsync(queryString, parameters).ConfigureAwait(false);
            return insertedId;
        }
    }
}
