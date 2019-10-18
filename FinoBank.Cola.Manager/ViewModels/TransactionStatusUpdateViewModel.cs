namespace FinoBank.Cola.Manager.ViewModels
{
    /// <summary>
    ///
    /// </summary>
    public class TransactionStatusUpdateViewModel
    {
        /// <summary>
        /// Gets or sets the transaction identifier.
        /// </summary>
        /// <value>
        /// The transaction identifier.
        /// </value>
        public long TransactionId { get; set; }

        /// <summary>
        /// Gets or sets the remarks.
        /// </summary>
        /// <value>
        /// The remarks.
        /// </value>
        public string Remarks { get; set; }

        /// <summary>
        /// Gets or sets the transaction status identifier.
        /// </summary>
        /// <value>
        /// The transaction status identifier.
        /// </value>
        public short TransactionOldStatusId { get; set; }

        /// <summary>
        /// Gets or sets the transaction new status identifier.
        /// </summary>
        /// <value>
        /// The transaction new status identifier.
        /// </value>
        public short TransactionNewStatusId { get; set; }
    }
}