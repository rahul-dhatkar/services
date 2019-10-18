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
//** Purpose   : ICommandGenericRepository                                                 **
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
using System.Threading.Tasks;

namespace Contesto.V2.Core.Infrastructure.Data.Interfaces
{
    /// <summary>
    /// Interface Command Generic Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TPrimaryKey">The type of the primary key.</typeparam>
    public interface ICommandGenericRepository<T, TPrimaryKey>
    {
        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="dbInPutFormat">The database in put format.</param>
        /// <returns></returns>
        Task<TPrimaryKey> Create(T model, EmumDbInPutFormat dbInPutFormat = EmumDbInPutFormat.Json);

        /// <summary>
        /// Creates the specified patient identifier.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="childType">Type of the child.</param>
        /// <param name="model">The model.</param>
        /// <param name="dbInPutFormat">The database in put format.</param>
        /// <returns></returns>
        Task<TPrimaryKey> Create(long patientId, int childType, T model, EmumDbInPutFormat dbInPutFormat = EmumDbInPutFormat.Json);

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="dbInPutFormat">The database in put format.</param>
        /// <returns></returns>
        Task<TPrimaryKey> Update(T model, EmumDbInPutFormat dbInPutFormat = EmumDbInPutFormat.Json);

        /// <summary>
        /// Deletes the specified primary key identifier.
        /// </summary>
        /// <param name="primaryKeyId">The primary key identifier.</param>
        /// <returns></returns>
        Task<bool> Delete(TPrimaryKey primaryKeyId);

        /// <summary>
        /// Deletes the specified primary key ids.
        /// </summary>
        /// <param name="primaryKeyIds">The primary key ids.</param>
        /// <returns></returns>
        Task<bool> Delete(IEnumerable<TPrimaryKey> primaryKeyIds);
    }
}