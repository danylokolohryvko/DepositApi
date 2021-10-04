using FluentValidation;

namespace DepositApi.Models
{
    public class GetDepositsModel
    {
        public int? StartIndex { get; set; }
        
        public int? Count { get; set; }
    }

    public class GetDepositsValidator : AbstractValidator<GetDepositsModel>
    {
        public GetDepositsValidator()
        {
            RuleFor(d => d.StartIndex).NotNull();
            RuleFor(d => d.Count).NotNull();
        }
    }
}
