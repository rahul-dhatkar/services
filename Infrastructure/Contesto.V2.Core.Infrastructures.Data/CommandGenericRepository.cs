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
//** Purpose   : CommandGenericRepository                                                  **
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
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Contesto.V2.Core.Data.Interfaces;
using Contesto.V2.Core.Data;
using System.Data.Common;

namespace Contesto.V2.Core.Infrastructure.Data
{
    /// <summary>
    /// Command Generic Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TPrimaryKey">The type of the primary key.</typeparam>
    /// <seealso cref="Contesto.V2.Core.Infrastructure.Data.Interfaces.ICommandGenericRepository{T, TPrimaryKey}" />
    /// <seealso cref="ICommandGenericRepository{T, TPrimaryKey}" />
    public abstract class CommandGenericRepository<T, TPrimaryKey> : ICommandGenericRepository<T, TPrimaryKey> 
    {
        /// <summary>
        /// The context
        /// </summary>
        protected readonly IDataContext Context = null;


        /// <summary>
        /// Initializes a new instance of the <see cref="CommandGenericRepository{T, TPrimaryKey}"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        protected CommandGenericRepository(string connectionString)
        {
            Context = new DataContext<SqlConnection>(connectionString);
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="dbInPutFormat">The database in put format.</param>
        /// <returns></returns>
        public virtual async Task<TPrimaryKey> Create(T model, EmumDbInPutFormat dbInPutFormat = EmumDbInPutFormat.Json)
        {
            var jsonModel = JsonConvert.SerializeObject(model);
            dynamic insertedId = null;
            var parameters = new DynamicParameters();
            parameters.Add("@Json", jsonModel, DbType.String, ParameterDirection.Input);
            parameters.Add("@InsertedId", insertedId, GetResultParameters(), ParameterDirection.Output);
            await Context.ExecuteWriteProcedureAsync(StoredProcedureNameHelper.CreateSPName<T>(), parameters).ConfigureAwait(false);
            insertedId = parameters.Get<TPrimaryKey>("@InsertedId");
            return insertedId;
        }

        /// <summary>
        /// Creates the specified patient identifier.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="childType">Type of the child.</param>
        /// <param name="model">The model.</param>
        /// <param name="dbInPutFormat">The database in put format.</param>
        /// <returns></returns>
        public virtual async Task<TPrimaryKey> Create(long patientId, int childType, T model, EmumDbInPutFormat dbInPutFormat = EmumDbInPutFormat.Json)
        {
            var jsonModel = JsonConvert.SerializeObject(model);
            dynamic insertedId = null;
            var parameters = new DynamicParameters();
            parameters.Add("@Json", jsonModel, DbType.String, ParameterDirection.Input);
            parameters.Add("@PatientId", patientId, DbType.Int64, ParameterDirection.Input);
            parameters.Add("@ChildType", childType, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@InsertedId", insertedId, GetResultParameters(), ParameterDirection.Output);
            await Context.ExecuteWriteProcedureAsync(StoredProcedureNameHelper.CreateSPName<T>(), parameters).ConfigureAwait(false); ;
            insertedId = parameters.Get<TPrimaryKey>("@InsertedId");
            return insertedId;
        }

        /// <summary>
        /// Deletes the specified primary key identifier.
        /// </summary>
        /// <param name="primaryKeyId">The primary key identifier.</param>
        /// <returns></returns>
        public virtual async Task<bool> Delete(TPrimaryKey primaryKeyId)
        {
            bool resultStatus = false;
            var parameters = new DynamicParameters();
            parameters.Add("@PrimaryKeyId", primaryKeyId, GetResultParameters(), ParameterDirection.Input);
            parameters.Add("@ResultStatus", resultStatus, DbType.Boolean, ParameterDirection.Output);
            await Context.ExecuteWriteProcedureAsync(StoredProcedureNameHelper.DeleteSPName<T>(), parameters).ConfigureAwait(false); ;
            resultStatus = parameters.Get<bool>("@ResultStatus");
            return resultStatus;
        }

        /// <summary>
        /// Deletes the specified primary key ids.
        /// </summary>
        /// <param name="primaryKeyIds">The primary key ids.</param>
        /// <returns></returns>
        public virtual async Task<bool> Delete(IEnumerable<TPrimaryKey> primaryKeyIds)
        {
            var primaryKeyIdsString = string.Join(",", primaryKeyIds);
            bool resultStatus = false;
            var parameters = new DynamicParameters();
            parameters.Add("@Ids", primaryKeyIdsString, DbType.String, ParameterDirection.Input);
            parameters.Add("@ResultStatus", resultStatus, DbType.Boolean, ParameterDirection.Output);
            await Context.ExecuteWriteProcedureAsync(StoredProcedureNameHelper.DeleteSPName<T>(), parameters).ConfigureAwait(false); ;
            resultStatus = parameters.Get<bool>("@ResultStatus");
            return resultStatus;
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="dbInPutFormat">The database in put format.</param>
        /// <returns></returns>
        public virtual async Task<TPrimaryKey> Update(T model, EmumDbInPutFormat dbInPutFormat = EmumDbInPutFormat.Json)
        {
            var jsonModel = JsonConvert.SerializeObject(model);
            dynamic updatedId = null;
            var parameters = new DynamicParameters();
            parameters.Add("@Json", jsonModel, DbType.String, ParameterDirection.Input);
            parameters.Add("@UpdatedId", updatedId, GetResultParameters(), ParameterDirection.Output);
            await Context.ExecuteWriteProcedureAsync(StoredProcedureNameHelper.UpdateSPName<T>(), parameters).ConfigureAwait(false);
            updatedId = parameters.Get<TPrimaryKey>("@UpdatedId");
            return updatedId;
        }

        /// <summary>
        /// Gets the result parameters.
        /// </summary>
        /// <returns></returns>
        protected static DbType GetResultParameters()
        {
            switch (typeof(TPrimaryKey).Name)
            {
                case "Int16":
                    {
                        return DbType.Int16;
                    }
                case "Int32":
                    {
                        return DbType.Int32;
                    }
                case "Int64":
                    {
                        return DbType.Int64;
                    }
                case "String":
                    {
                        return DbType.String;
                    }
                case "Guid":
                    {
                        return DbType.Guid;
                    }
            }
            return DbType.String;
        }
    }
}