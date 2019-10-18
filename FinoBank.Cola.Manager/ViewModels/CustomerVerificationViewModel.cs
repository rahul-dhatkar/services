using System;

namespace FinoBank.Cola.Manager.ViewModels
{
    /// <summary>
    ///
    /// </summary>
    public class CustomerVerificationViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        /// <value>
        /// The customer identifier.
        /// </value>
        public long CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the refrence code.
        /// </summary>
        /// <value>
        /// The refrence code.
        /// </value>
        public string RefrenceCode { get; set; }

        /// <summary>
        /// Gets or sets the is varified.
        /// </summary>
        /// <value>
        /// The is varified.
        /// </value>
        public bool? IsVarified { get; set; }

        /// <summary>
        /// Gets or sets the sent date time.
        /// </summary>
        /// <value>
        /// The sent date time.
        /// </value>
        public DateTime? SentDateTime { get; set; }
    }
}