namespace VoucherSystem.Domain.Enums;

public enum RedemptionResult
{
    Success,
    Expired,
    Exhausted,
    TierMismatch, // like a gold user attempt to aquire a platinum voucher
    SpendTooLow, // like a user paid $30 for $50 voucher.
    FraudBlocked,
}
