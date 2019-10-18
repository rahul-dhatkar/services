using Contesto.V2.Core.Common.Api.Base;
using FinoBank.Cola.Manager.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FinoBank.Cola.Api.Controllers
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="Contesto.V2.Core.Common.Api.Base.BaseApiController" />
    [Route("api/v1/master")]
    public class MasterController : BaseApiController
    {
        /// <summary>
        /// The query master data manager service
        /// </summary>
        public IQueryMasterDataManagerService _queryMasterDataManagerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MasterController" /> class.
        /// </summary>
        /// <param name="queryMasterDataManagerService">The query master data manager service.</param>
        public MasterController(IQueryMasterDataManagerService queryMasterDataManagerService)
        {
            _queryMasterDataManagerService = queryMasterDataManagerService;
        }

        /// <summary>
        /// Gets the withdrawal type master.
        /// </summary>
        /// <returns></returns>
        [HttpGet("withdrawalType")]
        public async Task<ActionResult> GetWithdrawalTypeMaster()
        {
            var result = await _queryMasterDataManagerService.GetWithdrawalTypeMaster().ConfigureAwait(false);

            if (!result.Success)
                return BadRequest(result.ErrorMessages);
            if (result != null && result.Success)
                return Ok(result);

            return BadRequest(result.ErrorMessages);
        }

        /// <summary>
        /// Gets the withdrawal type master.
        /// </summary>
        /// <returns></returns>
        [HttpGet("transactionType")]
        public async Task<ActionResult> GetTransactionTypeMaster()
        {
            var result = await _queryMasterDataManagerService.GetTransactionTypeMaster().ConfigureAwait(false);

            if (!result.Success)
                return BadRequest(result.ErrorMessages);
            if (result != null && result.Success)
                return Ok(result);

            return BadRequest(result.ErrorMessages);
        }

        /// <summary>
        /// Gets the withdrawal type master.
        /// </summary>
        /// <returns></returns>
        [HttpGet("transactionStatus")]
        public async Task<ActionResult> GetTransactionStatusMaster()
        {
            var result = await _queryMasterDataManagerService.GetTransactionStatusMaster().ConfigureAwait(false);

            if (!result.Success)
                return BadRequest(result.ErrorMessages);
            if (result != null && result.Success)
                return Ok(result);

            return BadRequest(result.ErrorMessages);
        }

        /// <summary>
        /// Gets the withdrawal type master.
        /// </summary>
        /// <returns></returns>
        [HttpGet("merchantType")]
        public async Task<ActionResult> GetMerchantTypeMaster()
        {
            var result = await _queryMasterDataManagerService.GetMerchantTypeMaster().ConfigureAwait(false);

            if (!result.Success)
                return BadRequest(result.ErrorMessages);
            if (result != null && result.Success)
                return Ok(result);

            return BadRequest(result.ErrorMessages);
        }
    }
}