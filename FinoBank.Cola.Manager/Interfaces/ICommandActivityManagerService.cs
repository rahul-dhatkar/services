using FinoBank.Cola.Manager.ViewModels;
using System.Threading.Tasks;

namespace FinoBank.Cola.Manager.Interfaces
{
    /// <summary>
    ///
    /// </summary>
    public interface ICommandActivityManagerService
    {
        /// <summary>
        /// Activities the logger.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task ActivityLogger(ActivityLogViewModel model);
    }
}