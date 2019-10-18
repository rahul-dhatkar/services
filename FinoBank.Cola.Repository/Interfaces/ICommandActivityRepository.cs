using Contesto.V2.Core.Infrastructure.Data.Interfaces;
using FinoBank.Cola.Repository.DomainModels;
using System.Threading.Tasks;

namespace FinoBank.Cola.Repository.Interfaces
{
    public interface ICommandActivityRepository //: ICommandGenericRepository<ActivityLogDomainModel, long>
    {
        /// <summary>
        /// Creates the activity log.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task CreateActivityLog(ActivityLogDomainModel model);
    }
}