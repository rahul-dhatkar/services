using FinoBank.Cola.Manager.ViewModels;
using FluentValidation;

namespace FinoBank.Cola.Manager.ViewModelValidators
{
    /// <summary>
    /// Master ViewModel Validation
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{FinoBank.Cola.Manager.ViewModels.SampleViewModel}" />
    /// <seealso cref="AbstractValidator{StartupKitViewModel}" />
    public class MerchantSetupViewModelValidation : AbstractValidator<MerchantSetupViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SampleViewModelValidation" /> class.
        /// </summary>
        public MerchantSetupViewModelValidation()
        {
            ////Default Required
            //RuleFor(x => x.RefCode).NotEmpty();
            //RuleFor(x => x.Latitude).NotEmpty();
            //RuleFor(x => x.Longitude).NotEmpty();
            //RuleFor(x => x.MerchantId).NotEmpty();

            // length validation
            RuleFor(x => x.RefCode).MaximumLength(20).WithMessage("RefCode cannot be more than 20 digits.");

        }
    }
}