using FinoBank.Cola.Manager.ViewModels;
using FluentValidation;

namespace FinoBank.Cola.Manager.ViewModelValidators
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{FinoBank.Cola.Manager.ViewModels.CustomerTransactionSummaryRequestViewModel}" />
    /// <seealso cref="FluentValidation.AbstractValidator{FinoBank.Cola.Manager.ViewModels.MerchantSearchRequestViewModel}" />
    public class CustomerTransactionSummaryRequestViewModelValidation : AbstractValidator<CustomerTransactionSummaryRequestViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerTransactionSummaryRequestViewModelValidation" /> class.
        /// </summary>
        public CustomerTransactionSummaryRequestViewModelValidation()
        {
            //Required
            RuleFor(x => x.CustomerType).NotEmpty();
            RuleFor(x => x.TransactionStatusId).NotNull();
            RuleFor(x => x.CustomerMobile).NotEmpty();
            RuleFor(x => x.StatementType).NotEmpty();

            //Condition
            //RuleFor(x => x.FromDate).NotEmpty().When(x => x.StatementType != 1);
            //RuleFor(x => x.ToDate).NotEmpty().When(x => x.StatementType != 1);
        }
    }
}