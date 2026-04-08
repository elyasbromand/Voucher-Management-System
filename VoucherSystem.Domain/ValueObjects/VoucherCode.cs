namespace VoucherSystem.Domain.ValueObjects;

public sealed class VoucherCode
{
    public string Value { get; }

    private VoucherCode(string value)
    {
        Value = value;
    }

    public static VoucherCode Create(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("Voucher code cannot be empty.");

        var normalized = code.Trim().ToUpperInvariant();

        if (normalized.Length < 6 || normalized.Length > 20)
            throw new ArgumentException("Voucher code must be between 6 and 20 characters.");

        return new VoucherCode(normalized);
    }

    public static VoucherCode Generate()
    {
        // Use today's date + 5 chars of Guid for better readability and uniqueness
        var code = Guid.NewGuid().ToString("N")[..12].ToUpperInvariant();
        return new VoucherCode(code);
    }

    public override string ToString() => Value;

    public override bool Equals(object? obj) => obj is VoucherCode other && Value == other.Value;

    public override int GetHashCode() => Value.GetHashCode();
}
