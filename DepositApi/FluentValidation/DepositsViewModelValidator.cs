using DepositApi.Models;
using FluentValidation;

namespace DepositApi.FluentValidation
{
    public class DepositsViewModelValidator : AbstractValidator<DepositsViewModel>
    {
        public DepositsViewModelValidator()
        {
            RuleFor(d => d.StartIndex).NotNull();
            RuleFor(d => d.Count).NotNull();
        }
    }
}
