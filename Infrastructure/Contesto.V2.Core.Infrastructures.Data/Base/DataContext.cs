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
//** Purpose   : DataContext                                                               **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Nam Team      02-07-18     Created                                                   **
//**                                                                                       **
//-------------------------------------------------------------------------------------------

using Contesto.V2.Core.Data.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Contesto.V2.Core.Data
{
    /// <summary>
    /// DataContext
    /// </summary>
    /// <typeparam name="ConnectionType">The type of the Connection type.</typeparam>
    /// <seealso cref="IDataContext" />
    public class DataContext<ConnectionType> : IDataContext
         where ConnectionType : DbConnection
    {
        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <value>
        /// The connection.
        /// </value>
        public DbConnection Connection { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataContext{ConnectionType}"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public DataContext(string connectionString)
        {
            Connection = (DbConnection)Activator.CreateInstance(typeof(ConnectionType), connectionString);
        }


        /// <summary>
        /// Executes the read procedure.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procedure">The procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public IEnumerable<T> ExecuteReadProcedure<T>(string procedure, object parameters = null)
        {
            var inputs = new DynamicParameters();

            if (parameters != null)
            {
                inputs.AddDynamicParams(parameters);
            }

            var results = Connection.Query<T>(procedure, inputs, commandType: CommandType.StoredProcedure);
            return results;
        }

        /// <summary>
        /// Executes the read SQL.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public IEnumerable<T> ExecuteReadSql<T>(string sql, object parameters = null)
        {
            var inputs = new DynamicParameters();

            if (parameters != null)
            {
                inputs.AddDynamicParams(parameters);
            }

            var results = Connection.Query<T>(sql, inputs, commandType: CommandType.Text);
            return results;
        }

        /// <summary>
        /// Executes the read procedure asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procedure">The procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> ExecuteReadProcedureAsync<T>(string procedure, object parameters = null)
        {
            var inputs = new DynamicParameters();

            if (parameters != null)
            {
                inputs.AddDynamicParams(parameters);
            }

            var results = await Connection.QueryAsync<T>(procedure, inputs, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
            return results;
        }

        /// <summary>
        /// Executes the read SQL asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> ExecuteReadSqlAsync<T>(string sql, object parameters = null)
        {
            var inputs = new DynamicParameters();

            if (parameters != null)
            {
                inputs.AddDynamicParams(parameters);
            }

            var results = await Connection.QueryAsync<T>(sql, inputs, commandType: CommandType.Text).ConfigureAwait(false);
            return results;
        }

        /// <summary>
        /// Executes the read query multiple procedure asynchronous.
        /// </summary>
        /// <param name="procedure">The procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public async Task<GridReader> ExecuteReadQueryMultipleProcedureAsync(string procedure, object parameters = null)
        {
            var inputs = new DynamicParameters();

            if (parameters != null)
            {
                inputs.AddDynamicParams(parameters);
            }

            var results = await Connection.QueryMultipleAsync(procedure, inputs, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
            return results;
        }

        /// <summary>
        /// Executes the read query multiple SQL asynchronous.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public async Task<GridReader> ExecuteReadQueryMultipleSqlAsync(string sql, object parameters = null)
        {
            var inputs = new DynamicParameters();

            if (parameters != null)
            {
                inputs.AddDynamicParams(parameters);
            }

            var results = await Connection.QueryMultipleAsync(sql, inputs, commandType: CommandType.Text).ConfigureAwait(false);
            return results;
        }

        /// <summary>
        /// Executes the write procedure.
        /// </summary>
        /// <param name="procedure">The procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public int ExecuteWriteProcedure(string procedure, object parameters = null)
        {
            var inputs = new DynamicParameters();

            if (parameters != null)
            {
                inputs.AddDynamicParams(parameters);
            }

            int index = Connection.Execute(procedure, inputs, commandType: CommandType.StoredProcedure);
            return index;
        }

        /// <summary>
        /// Executes the write SQL.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public int ExecuteWriteSql(string sql, object parameters = null)
        {
            var inputs = new DynamicParameters();

            if (parameters != null)
            {
                inputs.AddDynamicParams(parameters);
            }

            int index = Connection.Execute(sql, inputs, commandType: CommandType.Text);
            return index;
        }

        /// <summary>
        /// Executes the write procedure asynchronous.
        /// </summary>
        /// <param name="procedure">The procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public async Task<int> ExecuteWriteProcedureAsync(string procedure, object parameters = null)
        {
            var inputs = new DynamicParameters();

            if (parameters != null)
            {
                inputs.AddDynamicParams(parameters);
            }

            int index = await Connection.ExecuteAsync(procedure, inputs, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
            return index;
        }

        /// <summary>
        /// Executes the write SQL asynchronous.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public async Task<int> ExecuteWriteSqlAsync(string sql, object parameters = null)
        {
            var inputs = new DynamicParameters();

            if (parameters != null)
            {
                inputs.AddDynamicParams(parameters);
            }

            int index = await Connection.ExecuteAsync(sql, inputs, commandType: CommandType.Text).ConfigureAwait(false);
            return index;
        }

        /// <summary>
        /// Executes the single record read procedure asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procedure">The procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public async Task<T> ExecuteSingleRecordReadProcedureAsync<T>(string procedure, object parameters = null)
        {
            var inputs = new DynamicParameters();

            if (parameters != null)
            {
                inputs.AddDynamicParams(parameters);
            }
            var result = await Connection.QuerySingleOrDefaultAsync<T>(procedure, inputs, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
            return result;
        }

        /// <summary>
        /// Executes the single record SQL asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public async Task<T> ExecuteSingleRecordReadSqlAsync<T>(string sql, object parameters = null)
        {
            var inputs = new DynamicParameters();

            if (parameters != null)
            {
                inputs.AddDynamicParams(parameters);
            }
            var result = await Connection.QueryFirstOrDefaultAsync<T>(sql, inputs, commandType: CommandType.Text).ConfigureAwait(false);
            return result;
        }
        /// <summary>
        /// Executes the single record read procedure.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procedure">The procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public T ExecuteSingleRecordReadProcedure<T>(string procedure, object parameters = null)
        {
            var inputs = new DynamicParameters();

            if (parameters != null)
            {
                inputs.AddDynamicParams(parameters);
            }

            var result = Connection.QuerySingleOrDefault<T>(procedure, inputs, commandType: CommandType.StoredProcedure);
            return result;
        }

        /// <summary>
        /// Executes the single record SQL.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public T ExecuteSingleRecordReadSql<T>(string sql, object parameters = null)
        {
            var inputs = new DynamicParameters();

            if (parameters != null)
            {
                inputs.AddDynamicParams(parameters);
            }

            var result = Connection.QuerySingleOrDefault<T>(sql, inputs, commandType: CommandType.Text);
            return result;
        }
    }
}