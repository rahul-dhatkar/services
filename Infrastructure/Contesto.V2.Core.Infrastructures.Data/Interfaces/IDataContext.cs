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
//** Purpose   : IDataContext                                                              **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Nam Team      02-07-18     Created                                                   **
//**                                                                                       **
//-------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Contesto.V2.Core.Data.Interfaces
{
    /// <summary>
    /// IDataContext
    /// </summary>
    public interface IDataContext
    {
        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <value>
        /// The connection.
        /// </value>
        DbConnection Connection { get; }

        /// <summary>
        /// Executes the read procedure.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procedure">The procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        IEnumerable<T> ExecuteReadProcedure<T>(string procedure, object parameters = null);

        /// <summary>
        /// Executes the read SQL.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        IEnumerable<T> ExecuteReadSql<T>(string sql, object parameters = null);

        /// <summary>
        /// Executes the read procedure asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procedure">The procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        Task<IEnumerable<T>> ExecuteReadProcedureAsync<T>(string procedure, object parameters = null);

        /// <summary>
        /// Executes the read SQL asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        Task<IEnumerable<T>> ExecuteReadSqlAsync<T>(string sql, object parameters = null);
        /// <summary>
        /// Executes the write procedure.
        /// </summary>
        /// <param name="procedure">The procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        int ExecuteWriteProcedure(string procedure, object parameters = null);

        /// <summary>
        /// Executes the write SQL.
        /// </summary>
        /// <param name="procedure">The procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        int ExecuteWriteSql(string procedure, object parameters = null);

        /// <summary>
        /// Executes the write procedure asynchronous.
        /// </summary>
        /// <param name="procedure">The procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        Task<int> ExecuteWriteProcedureAsync(string procedure, object parameters = null);

        /// <summary>
        /// Executes the write SQL asynchronous.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        Task<int> ExecuteWriteSqlAsync(string sql, object parameters = null);

        /// <summary>
        /// Executes the single record read procedure asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procedure">The procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        Task<T> ExecuteSingleRecordReadProcedureAsync<T>(string procedure, object parameters = null);

        /// <summary>
        /// Executes the single record read SQL asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        Task<T> ExecuteSingleRecordReadSqlAsync<T>(string sql, object parameters = null);
        /// <summary>
        /// Executes the single record read procedure.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procedure">The procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        T ExecuteSingleRecordReadProcedure<T>(string procedure, object parameters = null);

        /// <summary>
        /// Executes the single record read SQL.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        T ExecuteSingleRecordReadSql<T>(string sql, object parameters = null);

        /// <summary>
        /// Executes the read query multiple procedure asynchronous.
        /// </summary>
        /// <param name="procedure">The procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        Task<GridReader> ExecuteReadQueryMultipleProcedureAsync(string procedure, object parameters = null);

        /// <summary>
        /// Executes the read query multiple SQL asynchronous.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        Task<GridReader> ExecuteReadQueryMultipleSqlAsync(string sql, object parameters = null);
    }
}