namespace VoucherSystem.Domain.Entities;

public sealed class FraudFlag
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid? VoucherId { get; private set; }
    public string Reason { get; private set; } = null!;
    public int RiskScore { get; private set; } // 0–100
    public DateTime FlaggedAt { get; private set; }
    public bool Resolved { get; private set; }
    public DateTime? ResolvedAt { get; private set; }

    private FraudFlag() { }

    public static FraudFlag Create(
        Guid userId,
        string reason,
        int riskScore,
        Guid? voucherId = null
    )
    {
        if (string.IsNullOrWhiteSpace(reason))
            throw new ArgumentException("Fraud flag reason cannot be empty.", nameof(reason));

        if (riskScore < 0 || riskScore > 100)
            throw new ArgumentOutOfRangeException(
                nameof(riskScore),
                "Risk score must be between 0 and 100."
            );

        return new FraudFlag
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            VoucherId = voucherId,
            Reason = reason,
            RiskScore = riskScore,
            FlaggedAt = DateTime.UtcNow,
            Resolved = false,
        };
    }

    public void Resolve()
    {
        Resolved = true;
        ResolvedAt = DateTime.UtcNow;
    }
}
