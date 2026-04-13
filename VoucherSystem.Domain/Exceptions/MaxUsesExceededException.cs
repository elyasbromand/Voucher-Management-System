namespace VoucherSystem.Domain.Exceptions;

public sealed class MaxUsesExceededException : Exception
{
    public MaxUsesExceededException(string code)
        : base($"Voucher '{code}' has reached its maximum number of uses.") { }
}
