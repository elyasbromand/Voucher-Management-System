using FluentValidation;

namespace VoucherSystem.Application.Campaigns.Commands;

public class CreateCampaignCommandValidator : AbstractValidator<CreateCampaignCommand>
{
    public CreateCampaignCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate)
            .WithMessage("EndDate must be after StartDate.");
        RuleFor(x => x.BudgetCap).GreaterThan(0).When(x => x.BudgetCap.HasValue);
    }
}
