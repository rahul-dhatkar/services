using Contesto.V2.Core.Common.Manager.Base;
using Contesto.V2.Core.Common.ViewModel.Base;
using System;

namespace FinoBank.Cola.Manager.ViewModels
{
    /// <summary>
    /// Master Summary Request ViewModel
    /// </summary>
    /// <seealso cref="Contesto.V2.Core.Common.Manager.Base.BaseGridPagingViewModel" />
    /// <seealso cref="BaseGridPagingViewModel" />
    public class CustomerTransactionSummaryRequestViewModel : BaseGridPagingViewModel
    {
        /// <summary>
        /// Gets or sets the type of the customer.
        /// </summary>
        /// <value>
        /// The type of the customer.
        /// </value>
        public string CustomerType { get; set; }

        /// <summary>
        /// Gets or sets the customer reference code.
        /// </summary>
        /// <value>
        /// The customer reference code.
        /// </value>
        public string CustomerRefCode { get; set; }

        /// <summary>
        /// Gets or sets the customer mobile.
        /// </summary>
        /// <value>
        /// The customer mobile.
        /// </value>
        public string CustomerMobile { get; set; }

        /// <summary>
        /// Gets or sets the transaction status identifier.
        /// </summary>
        /// <value>
        /// The transaction status identifier.
        /// </value>
        public int TransactionStatusId { get; set; }

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

        /// <summary>
        /// Gets or sets the Statement Type.
        /// </summary>
        /// <value>
        /// The statement type.
        /// </value>
        public int StatementType { get; set; }
    }
}