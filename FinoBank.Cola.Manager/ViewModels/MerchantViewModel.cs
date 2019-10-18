using Contesto.V2.Core.Common.Manager.Base;
using Contesto.V2.Core.Common.ViewModel.Base;

namespace FinoBank.Cola.Manager.ViewModels
{
    /// <summary>
    ///
    /// </summary>
    public class MerchantViewModel : BaseMasterViewModel<long>
    {
        /// <summary>
        /// Gets or sets the reference code.
        /// </summary>
        /// <value>
        /// The reference code.
        /// </value>
        public string RefCode { get; set; }

        /// <summary>
        /// Gets or sets the merchant type identifier.
        /// </summary>
        /// <value>
        /// The merchant type identifier.
        /// </value>
        public byte MerchantTypeId { get; set; }

        /// <summary>
        /// Gets or sets the address line1.
        /// </summary>
        /// <value>
        /// The address line1.
        /// </value>
        public string AddressLine1 { get; set; }

        /// <summary>
        /// Gets or sets the address line2.
        /// </summary>
        /// <value>
        /// The address line2.
        /// </value>
        public string AddressLine2 { get; set; }

        /// <summary>
        /// Gets or sets the district.
        /// </summary>
        /// <value>
        /// The district.
        /// </value>
        public string District { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the pin code.
        /// </summary>
        /// <value>
        /// The pin code.
        /// </value>
        public string PinCode { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the telephone.
        /// </summary>
        /// <value>
        /// The telephone.
        /// </value>
        public string Telephone { get; set; }

        /// <summary>
        /// Gets or sets the extension.
        /// </summary>
        /// <value>
        /// The extension.
        /// </value>
        public string Extension { get; set; }

        /// <summary>
        /// Gets or sets the fax.
        /// </summary>
        /// <value>
        /// The fax.
        /// </value>
        public string Fax { get; set; }

        /// <summary>
        /// Gets or sets the mobile number.
        /// </summary>
        /// <value>
        /// The mobile number.
        /// </value>
        public string MobileNumber { get; set; }

        /// <summary>
        /// Gets or sets the deposit cash balance.
        /// </summary>
        /// <value>
        /// The deposit cash balance.
        /// </value>
        public decimal? DepositCashBalance { get; set; }

        /// <summary>
        /// Gets or sets the withdraw cash balance.
        /// </summary>
        /// <value>
        /// The withdraw cash balance.
        /// </value>
        public decimal? WithdrawCashBalance { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        public double? Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        public double? Longitude { get; set; }

        /// <summary>
        /// Gets or sets the rating.
        /// </summary>
        /// <value>
        /// The rating.
        /// </value>
        public decimal? Rating { get; set; }

        /// <summary>
        /// Gets or sets the distance.
        /// </summary>
        /// <value>
        /// The distance.
        /// </value>
        public decimal? Distance { get; set; }

        /// <summary>
        /// Gets or sets the operating hours.
        /// </summary>
        /// <value>
        /// The operating hours.
        /// </value>
        public bool IsOnline { get; set; }

        /// <summary>
        /// Gets or sets the operating hours.
        /// </summary>
        /// <value>
        /// The operating hours.
        /// </value>
        public string WithdrawalTypes { get; set; }
    }
}

