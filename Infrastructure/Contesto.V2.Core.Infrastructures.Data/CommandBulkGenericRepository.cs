using Contesto.V2.Core.Infrastructure.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contesto.V2.Core.Infrastructure.Data
{
    /// <summary>
    /// Command Bulk GenericRepository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TPrimaryKey">The type of the primary key.</typeparam>
    /// <typeparam name="DbTableName">The type of the b table name.</typeparam>
    /// <seealso cref="Contesto.V2.Core.Infrastructure.Data.Interfaces.ICommandBulkGenericRepository{T, TPrimaryKey, DbTableName}" />
    public class CommandBulkGenericRepository<T, TPrimaryKey, DbTableName> : ICommandBulkGenericRepository<T, TPrimaryKey, DbTableName>
    {

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task<TPrimaryKey> Create(T model)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the specified primary key identifier.
        /// </summary>
        /// <param name="primaryKeyId">The primary key identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task<bool> Delete(TPrimaryKey primaryKeyId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task<TPrimaryKey> Update(T model)
        {
            throw new NotImplementedException();
        }
    }
}
