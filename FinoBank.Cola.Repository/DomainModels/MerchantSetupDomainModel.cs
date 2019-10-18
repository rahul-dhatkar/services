using System.Collections.Generic;

namespace FinoBank.Cola.Repository.DomainModels
{
    public class MerchantSetupDomainModel
    {
        /// <summary>
        /// Gets or sets the reference code.
        /// </summary>
        /// <value>
        /// The reference code.
        /// </value>
        public string RefCode { get; set; }

        /// <summary>
        /// Gets or sets the merchant identifier.
        /// </summary>
        /// <value>
        /// The merchant identifier.
        /// </value>
        public int MerchantId { get; set; }

        /// <summary>
        /// Gets or sets the deposit cash balance.
        /// </summary>
        /// <value>
        /// The deposit cash balance.
        /// </value>
        public double DepositCashBalance { get; set; }

        /// <summary>
        /// Gets or sets the withdraw cash balance.
        /// </summary>
        /// <value>
        /// The withdraw cash balance.
        /// </value>
        public double WithdrawCashBalance { get; set; }

        /// <summary>
        /// Gets or sets the is online.
        /// </summary>
        /// <value>
        /// The is online.
        /// </value>
        public bool IsOnline { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        public double Longitude { get; set; }

        /// <summary>
        /// Gets or sets the modified by.
        /// </summary>
        /// <value>
        /// The modified by.
        /// </value>
        public string ModifiedBy { get; set; }
        public List<MerchantSetUpWithdrawalType> WithdrawalTypes { get; set; }
    }

    public class MerchantSetUpWithdrawalType
    {
        public short Id { get; set; }
        public bool Value { get; set; }

    }
}
  