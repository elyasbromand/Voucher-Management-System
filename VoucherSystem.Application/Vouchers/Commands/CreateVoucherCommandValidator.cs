using System;
using FluentValidation;

namespace VoucherSystem.Application.Vouchers.Commands;

public class CreateVoucherCommandValidator : AbstractValidator<CreateVoucherCommand>
{
    public CreateVoucherCommandValidator()
    {
        RuleFor(x => x.CampaignId).NotEmpty();
        RuleFor(x => x.DiscountValue).GreaterThan(0);
        RuleFor(x => x.MaxUses).GreaterThanOrEqualTo(1);
        RuleFor(x => x.ExpiresAt)
            .Must(d => d == null || d > DateTime.UtcNow)
            .WithMessage("ExpiresAt must be in the future if provided.");
    }
}
