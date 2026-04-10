using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VoucherSystem.Domain.Entities;
using VoucherSystem.Domain.ValueObjects;

namespace VoucherSystem.Infrastructure.Persistence.Configurations;

public class VoucherConfiguration : IEntityTypeConfiguration<Voucher>
{
    public void Configure(EntityTypeBuilder<Voucher> builder)
    {
        builder.ToTable("Vouchers");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.Name).IsRequired().HasMaxLength(200);

        builder
            .Property(v => v.Code)
            .IsRequired()
            .HasConversion(code => code!.Value, value => VoucherCode.Create(value))
            .HasMaxLength(20);

        builder.HasIndex(v => v.Code).IsUnique();

        builder.Property(v => v.DiscountType).IsRequired();
        builder.Property(v => v.DiscountValue).HasColumnType("decimal(18,2)");
        builder.Property(v => v.MaxUses).IsRequired();
        builder.Property(v => v.CurrentUses).IsRequired();
        builder.Property(v => v.ExpiresAt).IsRequired();
        builder.Property(v => v.Status).IsRequired();
        builder.Property(v => v.CreatedAt).IsRequired();
    }
}
