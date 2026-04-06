using VoucherSystem.Domain.Enums;
using VoucherSystem.Domain.ValueObjects;

namespace VoucherSystem.Domain.Entities;

public class Voucher
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public VoucherCode Code { get; private set; }
    public DiscountType DiscountType { get; private set; }
    public decimal DiscountValue { get; private set; }
    public int MaxUses { get; private set; }
    public int CurrentUses { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public VoucherStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }

    // Private constructor
    // private Voucher() { }

    public static Voucher Create(
        string name,
        DiscountType discountType,
        decimal discountValue,
        int maxUses,
        DateTime expiresAt
    )
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Voucher name is required.");

        if (discountValue <= 0)
            throw new ArgumentException("Discount value must be greater than zero.");

        if (discountType == DiscountType.Percentage && discountValue > 100)
            throw new ArgumentException("Percentage discount cannot exceed 100.");

        if (maxUses < 1)
            throw new ArgumentException("Max uses must be at least 1.");

        if (expiresAt <= DateTime.UtcNow)
            throw new ArgumentException("Expiry date must be in the future.");

        return new Voucher
        {
            Id = Guid.NewGuid(),
            Name = name,
            Code = VoucherCode.Generate(),
            DiscountType = discountType,
            DiscountValue = discountValue,
            MaxUses = maxUses,
            CurrentUses = 0,
            ExpiresAt = expiresAt,
            Status = VoucherStatus.Active,
            CreatedAt = DateTime.UtcNow,
        };
    }

    public bool IsValid()
    {
        return Status == VoucherStatus.Active
            && ExpiresAt > DateTime.UtcNow
            && CurrentUses < MaxUses;
    }

    public void RecordUse()
    {
        if (!IsValid())
            throw new InvalidOperationException("This voucher cannot be used.");

        CurrentUses++;

        if (CurrentUses >= MaxUses)
            Status = VoucherStatus.Exhausted;
    }

    public void Cancel()
    {
        if (Status != VoucherStatus.Active)
            throw new InvalidOperationException("Only active vouchers can be cancelled.");

        Status = VoucherStatus.Cancelled;
    }
}
