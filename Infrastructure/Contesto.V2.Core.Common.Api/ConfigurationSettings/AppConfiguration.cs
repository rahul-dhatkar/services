namespace Contesto.V2.Core.Common.Api.ConfigurationSettings
{
    /// <summary>
    /// App Configuration
    /// </summary>
    public class AppConfiguration
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public string Version { get; set; }
        /// <summary>
        /// Gets or sets the terms of service.
        /// </summary>
        /// <value>
        /// The terms of service.
        /// </value>
        public string TermsOfService { get; set; }
        /// <summary>
        /// Gets or sets the contact.
        /// </summary>
        /// <value>
        /// The contact.
        /// </value>
        public Contact Contact { get; set; }
        /// <summary>
        /// Gets or sets the license.
        /// </summary>
        /// <value>
        /// The license.
        /// </value>
        public License License { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is antiforgery.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is antiforgery; otherwise, <c>false</c>.
        /// </value>
        public bool IsAntiforgeryOn { get; set; }
    }

    /// <summary>
    /// Contract
    /// </summary>
    public class Contact
    {

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string Url { get; set; }
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }
    }

    /// <summary>
    /// Framework One License
    /// </summary>
    public class License
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string Url { get; set; }
    }
}
