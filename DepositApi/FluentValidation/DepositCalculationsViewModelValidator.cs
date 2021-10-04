using DepositApi.Models;
using FluentValidation;

namespace DepositApi.FluentValidation
{
    public class DepositCalculationsViewModelValidator : AbstractValidator<DepositCalculationsViewModel>
    {
        public DepositCalculationsViewModelValidator()
        {
            RuleFor(d => d.DepositId).NotNull();
        }
    }
}
