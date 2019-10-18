using AutoMapper;
using Contesto.V2.Core.Common.Manager.Base;
using Contesto.V2.Core.Common.Manager.Helpers;
using Contesto.V2.Core.Common.Manager.Results;
using Contesto.V2.Core.Infrastructure.Data;
using FinoBank.Cola.Manager.Interfaces;
using FinoBank.Cola.Manager.ViewModels;
using FinoBank.Cola.Repository.DomainModels;
using FinoBank.Cola.Repository.Uom.Interfaces;
using System;
using System.Threading.Tasks;

namespace FinoBank.Cola.Manager.Commands
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="Contesto.V2.Core.Common.Manager.Base.BaseManager" />
    /// <seealso cref="FinoBank.Cola.Manager.Interfaces.ICommandTransactionRequestsManagerService" />
    public class CommandTransactionRequestsManagerService : BaseManager, ICommandTransactionRequestsManagerService
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandTransactionRequestsManagerService" /> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public CommandTransactionRequestsManagerService(IMapper mapper, IUnitOfWork unitOfWork) :
            base(mapper, null, null)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Creates the master.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<OperationResult<TransactionViewModel>> CreateTransactionRequests(TransactionViewModel model)
        {
            var details = MappService.Map<TransactionRequestsDomainModel>(model);

            int result = await _unitOfWork.CommandTransactionRequestsRepository.Create(details).ConfigureAwait(false);

            if(model.MerchantId > 0)
            {
                var dbResult = await _unitOfWork.QueryTransactionResultRepository.GetTransactionDetailsById(result).ConfigureAwait(false);

                return ResponseBuilderHelper<TransactionViewModel>.Instance.BuildSucessResult(new TransactionViewModel()
                {
                    Id = dbResult.Item1.Id,
                    ReferenceNumber = dbResult.Item1.ReferenceNumber,
                    MerchantId = dbResult.Item1.MerchantId,
                    CustomerId = dbResult.Item1.CustomerId,
                    CustomerLatitude = dbResult.Item1.CustomerLatitude,
                    CustomerLongitude = dbResult.Item1.CustomerLongitude,
                    MerchantLatitude = dbResult.Item1.MerchantLatitude,
                    MerchantLongitude = dbResult.Item1.MerchantLongitude,
                    TransactionTypeId = dbResult.Item1.TransactionTypeId,
                    WithdrawalTypeId = dbResult.Item1.WithdrawalTypeId,
                    RequestedDateTime = dbResult.Item1.RequestedDateTime,
                    RequestedAmount = dbResult.Item1.RequestedAmount,
                    RequestCompletedDateTime = dbResult.Item1.RequestCompletedDateTime,
                    TransactionStatusId = dbResult.Item1.TransactionStatusId,
                    ActualAmount = dbResult.Item1.ActualAmount,
                    Remarks = dbResult.Item1.Remarks,

                    Validity = dbResult.Item1.Validity,
                    MerchantName = dbResult.Item1.MerchantName,
                    MerchantAddress = dbResult.Item1.MerchantAddress,
                    MerchantType = dbResult.Item1.MerchantType,
                    MerchantMobile = dbResult.Item1.MerchantMobile,
                    TransactionStatus = dbResult.Item1.TransactionStatus,
                    TransactionType = dbResult.Item1.TransactionType,
                    WithdrawalType = dbResult.Item1.WithdrawalType,
                    CustomerMobile = dbResult.Item1.CustomerMobile,
                    CustomerType = dbResult.Item1.CustomerType
                });
            }
            else
            {
                return ResponseBuilderHelper<TransactionViewModel>.Instance.BuildSucessResult(new TransactionViewModel()
                {
                    Id = result
                });
            }
        }

        /// <summary>
        /// Updates the master.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<OperationResult<CommandSuccessResultViewModel>> UpdateTransactionRequests(TransactionStatusUpdateViewModel model)
        {
            var details = MappService.Map<TransactionRequestsDomainModel>(model);

            var result = await _unitOfWork.CommandTransactionRequestsRepository.Update(details).ConfigureAwait(false);

            return ResponseBuilderHelper<CommandSuccessResultViewModel>.Instance.BuildSucessResult(new CommandSuccessResultViewModel() { ResponseValue = result });
        }
    }
}