using FinoBank.Cola.Manager.ViewModels;
using FluentValidation;

namespace FinoBank.Cola.Manager.ViewModelValidators
{
    /// <summary>
    /// Master ViewModel Validation
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{FinoBank.Cola.Manager.ViewModels.SampleViewModel}" />
    /// <seealso cref="AbstractValidator{StartupKitViewModel}" />
    public class SampleViewModelValidation : AbstractValidator<SampleViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SampleViewModelValidation" /> class.
        /// </summary>
        public SampleViewModelValidation()
        {
            //Default Required
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.CreatedBy).NotEmpty().When(x => x.Id == 0);
            RuleFor(x => x.ModifiedBy).NotEmpty().When(x => x.Id > 0);

            // length validation
            RuleFor(x => x.Name).MaximumLength(200).WithMessage("Please enter up to 200 characters only.");
            RuleFor(x => x.Description).MaximumLength(1000).WithMessage("Please enter up to 1000 characters only.");

            //Special validation
            RuleFor(x => x.Name).Matches("^[a-zA-Z0-9-]+( [a-zA-Z0-9-]+)*$").When(x => x.Name != null).WithMessage("Please enter alphanumeric values allowed only.");
            RuleFor(x => x.Description).Matches("^[0-9a-zA-Z ]+$").When(x => x.Description != null).WithMessage("Please enter alphanumeric values allowed only.");
        }
    }
}