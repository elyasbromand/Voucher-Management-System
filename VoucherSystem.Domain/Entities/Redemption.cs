using VoucherSystem.Domain.Enums;

namespace VoucherSystem.Domain.Entities;

public sealed class Redemption
{
    public Guid Id { get; private set; }
    public Guid VoucherId { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime RedeemedAt { get; private set; }
    public decimal CartTotal { get; private set; }
    public decimal DiscountApplied { get; private set; }
    public string IpAddress { get; private set; } = null!;
    public RedemptionResult Result { get; private set; }

    private Redemption() { }

    public static Redemption Create(
        Guid voucherId,
        Guid userId,
        decimal cartTotal,
        decimal discountApplied,
        string ipAddress,
        RedemptionResult result
    )
    {
        if (string.IsNullOrWhiteSpace(ipAddress))
            throw new ArgumentException("IP address cannot be empty.", nameof(ipAddress));

        return new Redemption
        {
            Id = Guid.NewGuid(),
            VoucherId = voucherId,
            UserId = userId,
            RedeemedAt = DateTime.UtcNow,
            CartTotal = cartTotal,
            DiscountApplied = discountApplied,
            IpAddress = ipAddress,
            Result = result,
        };
    }
}
