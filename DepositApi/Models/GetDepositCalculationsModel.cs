using FluentValidation;

namespace DepositApi.Models
{
    public class GetDepositCalculationsModel
    {
        public int? DepositId { get; set; }
    }

    public class GetDepositCalculationsValidator : AbstractValidator<GetDepositCalculationsModel>
    {
        public GetDepositCalculationsValidator()
        {
            RuleFor(d => d.DepositId).NotNull();
        }
    }
}
