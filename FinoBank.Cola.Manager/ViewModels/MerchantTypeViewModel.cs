using Contesto.V2.Core.Common.Manager.Base;
using Contesto.V2.Core.Common.ViewModel.Base;

namespace FinoBank.Cola.Manager.ViewModels
{
    /// <summary>
    ///
    /// </summary>

    public class MerchantTypeViewModel : BaseMasterViewModel<int>
    {
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string Code { get; set; }
    }
}