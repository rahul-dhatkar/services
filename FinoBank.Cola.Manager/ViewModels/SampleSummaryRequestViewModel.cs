using Contesto.V2.Core.Common.Manager.Base;
using Contesto.V2.Core.Common.ViewModel.Base;

namespace FinoBank.Cola.Manager.ViewModels
{
    /// <summary>
    /// Master Summary Request ViewModel
    /// </summary>
    /// <seealso cref="Contesto.V2.Core.Common.Manager.Base.BaseGridPagingViewModel" />
    /// <seealso cref="BaseGridPagingViewModel" />
    public class SampleSummaryRequestViewModel : BaseGridPagingViewModel
    {
        /// <summary>
        /// Gets or sets the master type identifier.
        /// </summary>
        /// <value>
        /// The master type identifier.
        /// </value>
        public int TypeId { get; set; }
    }
}