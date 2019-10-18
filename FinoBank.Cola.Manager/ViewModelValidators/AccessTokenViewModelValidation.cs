using FinoBank.Cola.Manager.ViewModels;
using FluentValidation;

namespace FinoBank.Cola.Manager.ViewModelValidators
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{FinoBank.Cola.Manager.ViewModels.AccessTokenViewModel}" />
    public class AccessTokenViewModelValidation : AbstractValidator<AccessTokenViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccessTokenViewModelValidation"/> class.
        /// </summary>
        public AccessTokenViewModelValidation()
        {
            //Default Required

            RuleFor(x => x.ApplicationCode).NotEmpty();
            RuleFor(x => x.RefCode).NotEmpty().When(x => x.UserType == "F" && x.ApplicationCode == "Bpay" || x.ApplicationCode != "Bpay");
            RuleFor(x => x.Mobile).NotEmpty();
        }
    }
}