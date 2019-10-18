namespace FinoBank.Cola.Repository.DomainModels
{
    public class TransactionStatusUpdateDomainModel
    {
        public long Id { get; set; }

        public string Remarks { get; set; }

        public short TransactionOldStatusId { get; set; }

        public short TransactionNewStatusId { get; set; }
    }
}