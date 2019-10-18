using Contesto.V2.Core.Common.Manager.Base;
using Contesto.V2.Core.Common.ViewModel.Base;

namespace FinoBank.Cola.Manager.ViewModels
{
    /// <summary>
    ///
    /// </summary>
    public class CustomerViewModel : BaseViewModel<long>
    {
        /// <summary>
        /// Gets or sets the reference code.
        /// </summary>
        /// <value>
        /// The reference code.
        /// </value>
        public string RefCode { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }

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
        /// Gets or sets a value indicating whether this instance is verified.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is verified; otherwise, <c>false</c>.
        /// </value>
        public bool IsVerified { get; set; }
    }
}