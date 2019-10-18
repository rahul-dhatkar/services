using Contesto.V2.Core.Common.Manager.Base;
using Contesto.V2.Core.Common.ViewModel.Base;

namespace FinoBank.Cola.Manager.ViewModels
{
    /// <summary>
    /// Master ViewModel
    /// </summary>
    /// <seealso cref="Contesto.V2.Core.Common.Manager.Base.BaseMasterViewModel{System.Int32}" />
    /// <seealso cref="BaseMasterViewModel{System.Int32}" />
    public class SampleViewModel : BaseMasterViewModel<int>
    {
        /// <summary>
        /// Gets or sets the master type identifier.
        /// </summary>
        /// <value>
        /// The master type identifier.
        /// </value>
        public int TypeId { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
    }
}