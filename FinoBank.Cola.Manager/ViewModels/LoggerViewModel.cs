using System;

namespace FinoBank.Cola.Manager.ViewModels
{
    /// <summary>
    ///
    /// </summary>
    public class LoggerViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the event identifier.
        /// </summary>
        /// <value>
        /// The event identifier.
        /// </value>
        public byte? EventId { get; set; }

        /// <summary>
        /// Gets or sets the log level.
        /// </summary>
        /// <value>
        /// The log level.
        /// </value>
        public string LogLevel { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the inner exception message.
        /// </summary>
        /// <value>
        /// The inner exception message.
        /// </value>
        public string InnerExceptionMessage { get; set; }

        /// <summary>
        /// Gets or sets the stack trace.
        /// </summary>
        /// <value>
        /// The stack trace.
        /// </value>
        public string StackTrace { get; set; }

        /// <summary>
        /// Gets or sets the log date time.
        /// </summary>
        /// <value>
        /// The log date time.
        /// </value>
        public DateTime LogDateTime { get; set; }
    }
}