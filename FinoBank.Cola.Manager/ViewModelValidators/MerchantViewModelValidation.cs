using FinoBank.Cola.Manager.ViewModels;
using FluentValidation;

namespace FinoBank.Cola.Manager.ViewModelValidators
{
    /// <summary>
    /// Master ViewModel Validation
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{FinoBank.Cola.Manager.ViewModels.SampleViewModel}" />
    /// <seealso cref="AbstractValidator{StartupKitViewModel}" />
    public class MerchantViewModelValidation : AbstractValidator<MerchantViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SampleViewModelValidation" /> class.
        /// </summary>
        public MerchantViewModelValidation()
        {
            //Default Required
            RuleFor(x => x.RefCode).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.MerchantTypeId).NotEmpty();
            RuleFor(x => x.CreatedBy).NotEmpty().When(x => x.Id == 0);
            RuleFor(x => x.CreatedDateTime).NotEmpty().When(x => x.Id == 0);
            RuleFor(x => x.IsActive).NotNull();
            RuleFor(x => x.IsDeleted).NotNull();

            // length validation
            RuleFor(x => x.RefCode).MaximumLength(20).WithMessage("RefCode mucannot be more than 20 digits.");
            RuleFor(x => x.Name).MaximumLength(100).WithMessage("Name cannot be more than 100 characters.");
            RuleFor(x => x.PinCode).MaximumLength(6).WithMessage("Pincode must be 6 digits.");
            RuleFor(x => x.Telephone).MaximumLength(10).WithMessage("Telephone number must be 10 digits.");
            RuleFor(x => x.Extension).MaximumLength(10).WithMessage("Extension number must be 10 digits.");
            RuleFor(x => x.Fax).MaximumLength(10).WithMessage("Fax number must be 10 digits.");
            RuleFor(x => x.MobileNumber).MaximumLength(10).WithMessage("Mobile Number number must be 10 digits.");

            //Special validation
            RuleFor(x => x.Name).Matches("^[a-zA-Z0-9-]+( [a-zA-Z0-9-]+)*$").When(x => x.Name != null).WithMessage("Please enter alphanumeric values allowed only.");

        }
    }
}