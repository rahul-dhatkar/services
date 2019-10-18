using System;

namespace FinoBank.Cola.Manager.ViewModels
{
    /// <summary>
    ///
    /// </summary>
    public class UserTokenViewModel
    {

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the access token hash.
        /// </summary>
        /// <value>
        /// The access token.
        /// </value>
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the access token expires date time.
        /// </summary>
        /// <value>
        /// The access token expires date time.
        /// </value>
        public DateTimeOffset AccessTokenExpiresDateTime { get; set; }

        /// <summary>
        /// Gets or sets the refresh token identifier hash.
        /// </summary>
        /// <value>
        /// The refresh token identifier.
        /// </value>
        public string RefreshToken { get; set; }

        /// <summary>
        /// Gets or sets the refresh token identifier hash source.
        /// </summary>
        /// <value>
        /// The refresh token identifier hash source.
        /// </value>
        public string RefreshTokenSource { get; set; }

        /// <summary>
        /// Gets or sets the refresh token expires date time.
        /// </summary>
        /// <value>
        /// The refresh token expires date time.
        /// </value>
        public DateTimeOffset RefreshTokenExpiresDateTime { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>
        public bool IsDeleted { get; set; }

    }
}