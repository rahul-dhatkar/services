using AutoMapper;
using Contesto.V2.Core.Common.Api.Base;
using Contesto.V2.Core.Infrastructure.Security.JwtBearerTokenService.Dtos;
using Contesto.V2.Core.Infrastructure.Security.JwtBearerTokenService.Interfaces;
using FinoBank.Cola.Manager.Interfaces;
using FinoBank.Cola.Manager.ViewModels;
using FinoBank.Cola.Manager.ViewModelValidators;
using FinoBank.Cola.Repository.Uom.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FinoBank.Cola.Api.Controllers
{
    /// <summary>
    /// Access Token Controller
    /// </summary>
    /// <seealso cref="Contesto.V2.Core.Common.Api.Base.BaseApiController" />
    [Route("api/v1/accessToken")]
    public class AccessTokenController : BaseApiController
    {
     
        private readonly ICommandCustomerManagerService _commandCustomerManagerService;
        private readonly IMapper _mapperService;
        private readonly IUnitOfWork _unitOfWorkService;
       // private readonly ITokenStoreService _tokenStoreService;


        /// <summary>
        /// Initializes a new instance of the <see cref="AccessTokenController"/> class.
        /// </summary>
        /// <param name="tokenStoreService">The token store service.</param>
        public AccessTokenController (ICommandCustomerManagerService commandCustomerManagerService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            //_tokenStoreService = tokenStoreService;
            _commandCustomerManagerService = commandCustomerManagerService;
            _mapperService = mapper;
            _unitOfWorkService = unitOfWork;
        }

        /// <summary>
        /// 3
        /// Puts the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [IgnoreAntiforgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> Post([FromBody] AccessTokenViewModel model)
        {
            var results = GetValidationResult(model, new AccessTokenViewModelValidation());
            long dbId = 0;
            if (!results.IsValid)
            {
                return BadRequest(BuildValidationErrorMessage(results));
            }

            if (model.ApplicationCode == "Bpay")
            {
                var customerViewModel = _mapperService.Map<CustomerViewModel>(model);
                var result = await _commandCustomerManagerService.CheckAndCreateCustomer(customerViewModel).ConfigureAwait(false);
                dbId = result.Data.ResponseValue;
            }
            else
            {
                var merchant = await _unitOfWorkService.QueryMerchantSummaryRepository.GetMerchantDetailsByRefCode(model.RefCode).ConfigureAwait(false);
                if (merchant != null)
                    dbId = merchant.Id;
                else
                    return BadRequest("Unauthorized merchant");
            }

            return await BuildToken(dbId, model);
        }

        private async Task<ActionResult> BuildToken(long dbId, AccessTokenViewModel model)
        {
            if (dbId > 0)
            {
                var user = new UserDomainModel()
                {
                    Id = dbId,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.Mobile,
                    UserId = model.Mobile,
                    Roles = new System.Collections.Generic.List<string>() { model.UserType }
                };

                return Accepted("Cola_Menu", new { AccessToken = Guid.NewGuid().ToString(), RefreshToken = Guid.NewGuid().ToString() });
                //var (accessToken, refreshToken, claims) = await _tokenStoreService.CreateJwtTokens(user, refreshTokenSource: null).ConfigureAwait(false);
                // return Accepted("Cola_Menu", new { AccessToken = accessToken, RefreshToken = refreshToken });
            }
            else
            {
                return BadRequest("Invalid request");
            }
        }
    }
}