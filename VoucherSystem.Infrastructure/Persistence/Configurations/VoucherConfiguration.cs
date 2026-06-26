using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VoucherSystem.Domain.Entities;
using VoucherSystem.Domain.ValueObjects;

namespace VoucherSystem.Infrastructure.Persistence.Configurations;

public class VoucherConfiguration : IEntityTypeConfiguration<Voucher>
{
    public void Configure(EntityTypeBuilder<Voucher> builder)
    {
        builder.ToTable("Vouchers");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnType("uuid");

        var converter = new ValueConverter<VoucherCode, string>(
            v => v.Value,
            v => VoucherCode.Create(v)
        );

        builder
            .Property(x => x.Code)
            .HasConversion(converter)
            .HasColumnName("Code")
            .HasColumnType("varchar(20)")
            .HasMaxLength(20)
            .IsRequired();

        builder.HasIndex(x => x.Code).IsUnique();

        builder.Property(x => x.CampaignId).HasColumnType("uuid").IsRequired();
        builder.Property(x => x.DiscountType).HasConversion<int>().HasColumnType("int");
        builder.Property(x => x.DiscountValue).HasColumnType("numeric(18,2)").IsRequired();
        builder.Property(x => x.Status).HasConversion<int>().HasColumnType("int").IsRequired();
        builder.Property(x => x.MaxUses).HasColumnType("int").IsRequired();
        builder.Property(x => x.TimesUsed).HasColumnType("int").IsRequired();
        builder.Property(x => x.ExpiresAt).HasColumnType("timestamp with time zone");
        builder.Property(x => x.MinimumSpend).HasColumnType("numeric(18,2)");
        builder.Property(x => x.AllowedUserTier).HasConversion<int?>().HasColumnType("int");
        builder.Property(x => x.CreatedAt).HasColumnType("timestamp with time zone").IsRequired();
    }
}
