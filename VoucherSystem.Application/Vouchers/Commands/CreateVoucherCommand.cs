using System;
using MediatR;
using VoucherSystem.Domain.Enums;

namespace VoucherSystem.Application.Vouchers.Commands;

public sealed class CreateVoucherCommand : IRequest<Guid>
{
    public Guid CampaignId { get; set; }
    public DiscountType DiscountType { get; set; }
    public decimal DiscountValue { get; set; }
    public int MaxUses { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public decimal? MinimumSpend { get; set; }
    public UserTier? AllowedUserTier { get; set; }
}
