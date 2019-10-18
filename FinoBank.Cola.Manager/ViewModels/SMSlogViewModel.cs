using System.Collections.Generic;
using Contesto.V2.Core.Common.ViewModel.Base;

namespace FinoBank.Cola.Manager.ViewModels
{
    public class SMSlogViewModel : BaseViewModel<int>
    {
        public long TransactionId { get; set; }
        public int MerchantId { get; set; }
        //public List<MerchantId> MerchantId { get; set; }
    }

    //public class MerchantId
    //{
    //    public string Id { get; set; }
    //}
}
