using Contesto.V2.Core.Common.Api.Base;
using FinoBank.Cola.Manager.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FinoBank.Cola.Api.Controllers
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="Contesto.V2.Core.Common.Api.Base.BaseApiController" />
    [Route("api/v1/loggerService")]
    public class LoggerServiceController : BaseApiController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<LoggerServiceController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerServiceController" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public LoggerServiceController(ILogger<LoggerServiceController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <param name="environment">The environment.</param>
        /// <returns></returns>
        [HttpGet("{environment}/getAll")]
        public ActionResult Get(string environment)
        {
            _logger.LogCritical("LogCritical Message");
            _logger.LogError("LogError Message");
            _logger.LogInformation("LogInformation Message");
            _logger.LogTrace("LogTrace Message");
            _logger.LogWarning("LogWarning Message");
            return Ok();
        }

        /// <summary>
        /// Posts the specified model.
        /// </summary>
        /// <param name="environment">The environment.</param>
        /// <param name="model">The model.</param>
        [HttpPost("{environment}")]
        public void Post(string environment, [FromBody] LoggerViewModel model)
        {
        }
    }
}