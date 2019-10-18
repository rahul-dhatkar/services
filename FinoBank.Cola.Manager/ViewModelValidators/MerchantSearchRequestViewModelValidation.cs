using FinoBank.Cola.Manager.ViewModels;
using FluentValidation;

namespace FinoBank.Cola.Manager.ViewModelValidators
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{FinoBank.Cola.Manager.ViewModels.MerchantSearchRequestViewModel}" />
    public class MerchantSearchRequestViewModelValidation : AbstractValidator<MerchantSearchRequestViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MerchantSearchRequestViewModelValidation" /> class.
        /// </summary>
        public MerchantSearchRequestViewModelValidation()
        {
            //Required
            RuleFor(x => x.CurrentLatitude).NotEmpty();
            RuleFor(x => x.CurrentLongitude).NotEmpty();
            RuleFor(x => x.Amount).NotEmpty();
            RuleFor(x => x.ByTransactionTypeId).NotEmpty();

            //Condition
            RuleFor(x => x.CustomerMobile).NotEmpty().When(x => x.CustomerRefCode == null);
            RuleFor(x => x.ByWithdrawalTypeId).NotEmpty().When(x => x.ByTransactionTypeId == 2);
        }
    }
}