using System;
using System.Collections.Generic;

namespace FinoBank.Cola.Manager.ViewModels
{
    public class MobileNoRequestViewModel
    {
        public long TransactionId { get; set; }
        public long CustomerID { get; set; }
        public string CustomerMobile { get; set; }
        public int MerchantId { get; set; }
        public string MerchantMobile { get; set; }
        public string TransactionType { get; set; }
        public string ReferenceNumber { get; set; }
        public string Remarks { get; set; }
        public Guid UniqueId { get; set; }
    }
}

