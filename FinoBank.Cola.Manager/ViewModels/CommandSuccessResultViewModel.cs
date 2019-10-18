using Contesto.V2.Core.Common.ViewModel.Base;

namespace FinoBank.Cola.Manager.ViewModels
{
    /// <summary>
    /// Master Success Result ViewModel
    /// </summary>
    /// <seealso cref="Contesto.V2.Core.Common.Manager.Base.BaseCommandSuccessResultViewModel{System.Int32}" />
    /// <seealso cref="BaseCommandSuccessResultViewModel{System.Int32}" />
    public class CommandSuccessResultViewModel : BaseCommandSuccessResultViewModel<int>
    {
    }

    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="Contesto.V2.Core.Common.Manager.Base.BaseCommandSuccessResultViewModel{System.Boolean}" />
    public class CommandSuccessBoolResultViewModel : BaseCommandSuccessResultViewModel<bool>
    {
    }

    public class CommandSuccessLongResultViewModel : BaseCommandSuccessResultViewModel<long>
    {
    }

    public class CommandSuccessStringResultViewModel : BaseCommandSuccessResultViewModel<string>
    {
    }
}