using Contesto.V2.Core.Common.ViewModel.Base;

/// <summary>
///
/// </summary>
namespace FinoBank.Cola.Manager.ViewModels
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="Contesto.V2.Core.Common.Manager.Base.BaseGridPagingViewModel" />
    public class MerchantSearchRequestViewModel : BaseGridPagingViewModel
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
        /// Gets or sets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        public int Amount { get; set; }

        /// <summary>
        /// Gets or sets the current latitude.
        /// </summary>
        /// <value>
        /// The current latitude.
        /// </value>
        public double CurrentLatitude { get; set; }

        /// <summary>
        /// Gets or sets the current longitude.
        /// </summary>
        /// <value>
        /// The current longitude.
        /// </value>
        public double CurrentLongitude { get; set; }

        /// <summary>
        /// Gets or sets the by rating.
        /// </summary>
        /// <value>
        /// The by rating.
        /// </value>
        public int ByMerchantTypeId { get; set; }

        /// <summary>
        /// Gets or sets the by distance.
        /// </summary>
        /// <value>
        /// The by distance.
        /// </value>
        public int ByTransactionTypeId { get; set; }

        /// <summary>
        /// Gets or sets the type of the by banking.
        /// </summary>
        /// <value>
        /// The type of the by banking.
        /// </value>
        public int ByWithdrawalTypeId { get; set; }

        /// <summary>
        /// Gets or sets the by only branches or merchant.
        /// </summary>
        /// <value>
        /// The by only branches or merchant.
        /// </value>
        public int? Distance { get; set; }
    }
}