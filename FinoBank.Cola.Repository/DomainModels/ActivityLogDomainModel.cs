using Contesto.V2.Core.Infrastructure.Data.Base;

namespace FinoBank.Cola.Repository.DomainModels
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="Contesto.V2.Core.Infrastructure.Data.Base.BaseDomainMasterModel{System.Int64}" />
    public class ActivityLogDomainModel : BaseDomainModel<long>
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
    }
}