using FluentValidation;
using InAndOut.Models;

namespace InAndOut.Validators
{
    public class ExpenseTypeValidator : AbstractValidator<ExpenseType>
    {
        public ExpenseTypeValidator()
        {
            RuleFor(expenseType => expenseType.ExpenseTypeName).NotNull().WithMessage("The Type Name is Required");
        }
    }
}
