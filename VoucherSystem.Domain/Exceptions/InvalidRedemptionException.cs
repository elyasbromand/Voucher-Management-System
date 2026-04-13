namespace VoucherSystem.Domain.Exceptions;

public sealed class InvalidRedemptionException : Exception
{
    // The reason should be passed when calling this exception
    public InvalidRedemptionException(string reason)
        : base(reason) { }
}
