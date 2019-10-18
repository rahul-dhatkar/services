using Contesto.V2.Core.Common.ViewModel.Base;
using System.Collections.Generic;

namespace FinoBank.Cola.Manager.ViewModels
{
    /// <summary>
    /// Master Summary Result ViewModel
    /// </summary>
    /// <seealso cref="Contesto.V2.Core.Common.Manager.Base.BaseSummaryResultViewModel{System.Collections.Generic.List{FinoBank.Cola.Manager.ViewModels.TransactionViewModel}}" />
    /// <seealso cref="Contesto.V2.Core.Common.Manager.Base.BaseSummaryResultViewModel{System.Collections.Generic.List{FinoBank.Cola.Manager.ViewModels.TransactionHistoryViewModel}}" />
    public class TransactionSummaryResultViewModel : BaseSummaryResultViewModel<List<TransactionViewModel>>
    {
    }
}