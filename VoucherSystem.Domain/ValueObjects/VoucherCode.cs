namespace VoucherSystem.Domain.ValueObjects;

using System.Security.Cryptography;

public sealed class VoucherCode : IEquatable<VoucherCode>
{
    public string Value { get; }

    private VoucherCode(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Creates a VoucherCode from an existing raw string (e.g. from user input or DB).
    /// Normalises to uppercase and trims whitespace.
    /// </summary>
    public static VoucherCode Create(string rawCode)
    {
        if (string.IsNullOrWhiteSpace(rawCode))
            throw new ArgumentException("Voucher code cannot be empty.", nameof(rawCode));

        var normalised = rawCode.Trim().ToUpperInvariant();

        if (normalised.Length < 4 || normalised.Length > 20)
            throw new ArgumentException(
                "Voucher code must be between 4 and 20 characters.",
                nameof(rawCode)
            );

        return new VoucherCode(normalised);
    }

    /// <summary>
    /// Generates a new cryptographically random voucher code.
    /// Format: 3 letters + dash + 4 alphanumeric + dash + 3 letters  e.g. "XKT-A3F9-MQZ"
    /// </summary>
    public static VoucherCode Generate()
    {
        const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";

        string Part(int length)
        {
            Span<byte> bytes = stackalloc byte[length];
            RandomNumberGenerator.Fill(bytes);
            return new string(bytes.ToArray().Select(b => chars[b % chars.Length]).ToArray());
        }

        var code = $"{Part(3)}-{Part(4)}-{Part(3)}";
        return new VoucherCode(code);
    }

    // Value equality: two VoucherCodes with the same Value are equal
    public bool Equals(VoucherCode? other) => other is not null && Value == other.Value;

    public override bool Equals(object? obj) => obj is VoucherCode other && Equals(other);

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Value;

    public static bool operator ==(VoucherCode? left, VoucherCode? right) =>
        left?.Equals(right) ?? right is null;

    public static bool operator !=(VoucherCode? left, VoucherCode? right) => !(left == right);
}
