namespace FinoBank.Cola.Manager.ViewModels
{
    /// <summary>
    /// Access Token ViewModel
    /// </summary>
    public class AccessTokenViewModel
    {
        /// <summary>
        /// Gets or sets the application code.
        /// </summary>
        /// <value>
        /// The application code.
        /// </value>
        public string ApplicationCode { get; set; }

        /// <summary>
        /// Gets or sets the user reference code.
        /// </summary>
        /// <value>
        /// The user reference code.
        /// </value>
        public string RefCode { get; set; }

        /// <summary>
        /// Gets or sets the type of the user.
        /// </summary>
        /// <value>
        /// The type of the user.
        /// </value>
        public string UserType { get; set; }

        /// <summary>
        /// Gets or sets the customer mobile.
        /// </summary>
        /// <value>
        /// The customer mobile.
        /// </value>
        public string Mobile { get; set; }

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
    }
}