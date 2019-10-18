using System.Threading.Tasks;

namespace Contesto.V2.Core.Infrastructure.Data.Interfaces
{
    /// <summary>
    /// Interface Command Bulk Generic Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TPrimaryKey">The type of the primary key.</typeparam>
    /// <typeparam name="DbTableName">The type of the b table name.</typeparam>
    public interface ICommandBulkGenericRepository<T, TPrimaryKey, DbTableName> 
    {
        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<TPrimaryKey> Create(T model);

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<TPrimaryKey> Update(T model);

        /// <summary>
        /// Deletes the specified primary key identifier.
        /// </summary>
        /// <param name="primaryKeyId">The primary key identifier.</param>
        /// <returns></returns>
        Task<bool> Delete(TPrimaryKey primaryKeyId);
    }
}
