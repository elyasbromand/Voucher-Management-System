using System;

namespace VoucherSystem.Application.DTOs;

public sealed class VoucherDto
{
    public Guid Id { get; set; }
    public string Code { get; set; } = null!;
    public Guid CampaignId { get; set; }
    public string DiscountType { get; set; } = null!;
    public decimal DiscountValue { get; set; }
    public string Status { get; set; } = null!;
    public int MaxUses { get; set; }
    public int TimesUsed { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public decimal? MinimumSpend { get; set; }
    public string? AllowedUserTier { get; set; }
    public DateTime CreatedAt { get; set; }
}
