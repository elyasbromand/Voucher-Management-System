using VoucherSystem.Domain.Enums;
using VoucherSystem.Domain.Exceptions;
using VoucherSystem.Domain.ValueObjects;

namespace VoucherSystem.Domain.Entities;

public sealed class Voucher
{
    public Guid Id { get; private set; }
    public VoucherCode Code { get; private set; } = null!;
    public Guid CampaignId { get; private set; }
    public DiscountType DiscountType { get; private set; }
    public decimal DiscountValue { get; private set; }
    public VoucherStatus Status { get; private set; }
    public int MaxUses { get; private set; }
    public int TimesUsed { get; private set; }
    public DateTime? ExpiresAt { get; private set; }
    public decimal? MinimumSpend { get; private set; }
    public UserTier? AllowedUserTier { get; private set; }
    public DateTime CreatedAt { get; private set; }

    // EF Core needs a parameterless constructor — keep it private
    private Voucher() { }

    public static Voucher Create(
        Guid campaignId,
        DiscountType discountType,
        decimal discountValue,
        int maxUses,
        DateTime? expiresAt = null,
        decimal? minimumSpend = null,
        UserTier? allowedUserTier = null
    )
    {
        if (discountValue <= 0)
            throw new ArgumentException(
                "Discount value must be greater than zero.",
                nameof(discountValue)
            );

        if (discountType == DiscountType.Percentage && discountValue > 100)
            throw new ArgumentException(
                "Percentage discount cannot exceed 100.",
                nameof(discountValue)
            );

        if (maxUses < 1)
            throw new ArgumentException("Max uses must be at least 1.", nameof(maxUses));

        if (expiresAt.HasValue && expiresAt.Value <= DateTime.UtcNow)
            throw new ArgumentException("Expiry date must be in the future.", nameof(expiresAt));

        return new Voucher
        {
            Id = Guid.NewGuid(),
            Code = VoucherCode.Generate(),
            CampaignId = campaignId,
            DiscountType = discountType,
            DiscountValue = discountValue,
            Status = VoucherStatus.Active,
            MaxUses = maxUses,
            TimesUsed = 0,
            ExpiresAt = expiresAt,
            MinimumSpend = minimumSpend,
            AllowedUserTier = allowedUserTier,
            CreatedAt = DateTime.UtcNow,
        };
    }

    public bool IsValid(UserTier userTier, decimal cartTotal)
    {
        if (Status != VoucherStatus.Active)
            return false;
        if (ExpiresAt.HasValue && ExpiresAt.Value < DateTime.UtcNow)
            return false;
        if (TimesUsed >= MaxUses)
            return false;
        if (AllowedUserTier.HasValue && userTier < AllowedUserTier.Value)
            return false;
        if (MinimumSpend.HasValue && cartTotal < MinimumSpend.Value)
            return false;
        return true;
    }

    public decimal CalculateDiscount(decimal cartTotal)
    {
        return DiscountType switch
        {
            DiscountType.Percentage => Math.Round(cartTotal * (DiscountValue / 100m), 2),
            DiscountType.FixedAmount => Math.Min(DiscountValue, cartTotal), // can't discount more than cart
            DiscountType.FreeItem => DiscountValue,
            DiscountType.Cashback => Math.Round(cartTotal * (DiscountValue / 100m), 2),
            _ => throw new InvalidOperationException($"Unknown discount type: {DiscountType}"),
        };
    }

    public void RecordUse()
    {
        if (Status == VoucherStatus.Expired)
            throw new VoucherExpiredException(Code.Value);

        if (Status != VoucherStatus.Active)
            throw new MaxUsesExceededException(Code.Value);

        TimesUsed++;

        if (TimesUsed >= MaxUses)
            Status = VoucherStatus.Exhausted;
    }

    public void Cancel()
    {
        if (Status == VoucherStatus.Cancelled)
            return; // idempotent

        Status = VoucherStatus.Cancelled;
    }

    public void MarkExpired()
    {
        if (Status == VoucherStatus.Active)
            Status = VoucherStatus.Expired;
    }
}
