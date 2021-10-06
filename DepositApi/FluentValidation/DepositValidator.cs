using DepositApi.Models;
using FluentValidation;

namespace DepositApi.FluentValidation
{
    public class DepositValidator : AbstractValidator<DepositModel>
    {
        public DepositValidator()
        {
            RuleFor(d => d.Amount).NotNull();
            RuleFor(d => d.Term).NotNull();
            RuleFor(d => d.Percent).NotNull();
        }
    }
}
