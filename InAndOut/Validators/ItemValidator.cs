using FluentValidation;
using InAndOut.Models;

namespace InAndOut.Validators
{
    public class ItemValidator : AbstractValidator<Item>
    {
        public ItemValidator()
        {
            RuleFor(item => item.ItemName).NotNull().WithMessage("The Name is required");
            RuleFor(item => item.Borrower).NotNull().WithMessage("The Borrower is required");
            RuleFor(item => item.Lender).NotNull().WithMessage("The Lender is required");
        }
    }
}
