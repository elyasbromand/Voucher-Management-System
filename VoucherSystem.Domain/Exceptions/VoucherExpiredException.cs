namespace VoucherSystem.Domain.Exceptions;

public sealed class VoucherExpiredException : Exception
{
    public VoucherExpiredException(string code)
        : base($"Voucher '{code}' has expired and can no longer be redeemed.") { }
}

// I am keeping this the tradational way, not primary constructor.
// Because it is domain layer and the business logic should be explicitely clear.
