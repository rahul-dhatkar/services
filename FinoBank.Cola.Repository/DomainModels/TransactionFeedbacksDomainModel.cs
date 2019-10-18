using System;

namespace FinoBank.Cola.Repository.DomainModels
{
    public class TransactionFeedbacksDomainModel
    {
        public int MerchantId { get; set; }

        public long CustomerId { get; set; }

        public long TransactionId { get; set; }

        public byte Rating { get; set; }

        public string Notes { get; set; }
        public Guid UniqueId { get; set; }
    }
}