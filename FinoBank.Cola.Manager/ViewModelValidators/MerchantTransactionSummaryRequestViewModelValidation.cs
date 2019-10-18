using FinoBank.Cola.Manager.ViewModels;
using FluentValidation;

namespace FinoBank.Cola.Manager.ViewModelValidators
{
   
    public class MerchantTransactionSummaryRequestViewModelValidation : AbstractValidator<MerchantTransactionSummaryRequestViewModel>
    {
        public MerchantTransactionSummaryRequestViewModelValidation()
        {
            //Required
            RuleFor(x => x.RefCode).NotEmpty();
            RuleFor(x => x.TransactionStatusId).NotNull();
        }
    }
}