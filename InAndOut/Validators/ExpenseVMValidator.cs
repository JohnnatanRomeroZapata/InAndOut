using FluentValidation;
using InAndOut.Models.ViewModels;

namespace InAndOut.Validators
{
    public class ExpenseVMValidator : AbstractValidator<ExpenseVM>
    {
        public ExpenseVMValidator()
        {
            RuleFor(expense => expense.TheExpense.ExpenseName).NotNull().WithMessage("The expense name is required");
            RuleFor(expense => expense.TheExpense.Ammount)
                .NotNull().WithMessage("The ammount is reuired")
                .GreaterThan(0).WithMessage("The ammount must be greater than 0");
            RuleFor(expense => expense.TheExpense.ExpenseTypeId).NotNull().WithMessage("The expense type must be included");
        }
    }
}
