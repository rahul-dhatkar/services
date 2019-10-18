namespace FinoBank.Cola.Manager.ViewModels
{
    /// <summary>
    ///
    /// </summary>
    public class ActivityLogViewModel
    {
        /// <summary>
        /// Gets or sets the action code.
        /// </summary>
        /// <value>
        /// The action code.
        /// </value>
        public string ActionCode { get; set; }

        /// <summary>
        /// Gets or sets the new json data.
        /// </summary>
        /// <value>
        /// The new json data.
        /// </value>
        public string NewJsonData { get; set; }

        /// <summary>
        /// Gets or sets the old json data.
        /// </summary>
        /// <value>
        /// The old json data.
        /// </value>
        public string OldJsonData { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public string CreatedBy { get; set; }
    }
}