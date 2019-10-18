using System;

namespace FinoBank.Cola.Manager.ViewModels
{
    /// <summary>
    ///
    /// </summary>
    public class TransactionFeedbackViewModel
    {
        public int MerchantId { get; set; }

        public long CustomerId { get; set; }

        public long TransactionId { get; set; }

        public byte Rating { get; set; }

        public string Notes { get; set; }

        public Guid UniqueId { get; set; }
    }
}