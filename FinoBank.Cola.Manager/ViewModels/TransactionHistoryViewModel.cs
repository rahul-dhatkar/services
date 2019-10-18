using System;

namespace FinoBank.Cola.Manager.ViewModels
{
    /// <summary>
    ///
    /// </summary>
    public class TransactionHistoryViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the reference number.
        /// </summary>
        /// <value>
        /// The reference number.
        /// </value>
        public Guid? ReferenceNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the merchant.
        /// </summary>
        /// <value>
        /// The name of the merchant.
        /// </value>
        public string MerchantName { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        /// <value>
        /// The customer identifier.
        /// </value>
        public long CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the customer mobile.
        /// </summary>
        /// <value>
        /// The customer mobile.
        /// </value>
        public string CustomerMobile { get; set; }

        /// <summary>
        /// Gets or sets the type of the transaction.
        /// </summary>
        /// <value>
        /// The type of the transaction.
        /// </value>
        public string TransactionType { get; set; }

        /// <summary>
        /// Gets or sets the requested date time.
        /// </summary>
        /// <value>
        /// The requested date time.
        /// </value>
        public DateTime RequestedDateTime { get; set; }

        /// <summary>
        /// Gets or sets the requested amount.
        /// </summary>
        /// <value>
        /// The requested amount.
        /// </value>
        public int RequestedAmount { get; set; }

        /// <summary>
        /// Gets or sets the request completed date time.
        /// </summary>
        /// <value>
        /// The request completed date time.
        /// </value>
        public DateTime? RequestCompletedDateTime { get; set; }

        /// <summary>
        /// Gets or sets the transaction status.
        /// </summary>
        /// <value>
        /// The transaction status.
        /// </value>
        public string TransactionStatus { get; set; }
    }
}