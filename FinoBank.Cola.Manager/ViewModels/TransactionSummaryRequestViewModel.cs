using Contesto.V2.Core.Common.ViewModel.Base;
using System;

namespace FinoBank.Cola.Manager.ViewModels
{
    /// <summary>
    /// Master Summary Request ViewModel
    /// </summary>
    /// <seealso cref="Contesto.V2.Core.Common.Manager.Base.BaseGridPagingViewModel" />
    /// <seealso cref="BaseGridPagingViewModel" />
    public class TransactionSummaryRequestViewModel : BaseGridPagingViewModel
    {
        /// <summary>
        /// Gets or sets the reference code.
        /// </summary>
        /// <value>
        /// The reference code.
        /// </value>
        public string RefCode { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the mobile.
        /// </summary>
        /// <value>
        /// The mobile.
        /// </value>
        public string Mobile { get; set; }

        /// <summary>
        /// Gets or sets the transaction status identifier.
        /// </summary>
        /// <value>
        /// The transaction status identifier.
        /// </value>
        public short TransactionStatusId { get; set; }

        /// <summary>
        /// Gets or sets from date.
        /// </summary>
        /// <value>
        /// From date.
        /// </value>
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// Gets or sets to date.
        /// </summary>
        /// <value>
        /// To date.
        /// </value>
        public DateTime? ToDate { get; set; }
    }
}