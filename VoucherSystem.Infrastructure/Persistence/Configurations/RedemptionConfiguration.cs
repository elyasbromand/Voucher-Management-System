using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VoucherSystem.Domain.Entities;
using VoucherSystem.Domain.Enums;

namespace VoucherSystem.Infrastructure.Persistence.Configurations;

public class RedemptionConfiguration : IEntityTypeConfiguration<Redemption>
{
    public void Configure(EntityTypeBuilder<Redemption> builder)
    {
        builder.ToTable("Redemptions");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnType("uuid");

        builder.Property(x => x.VoucherId).HasColumnType("uuid").IsRequired();
        builder.Property(x => x.UserId).HasColumnType("uuid").IsRequired();
        builder.Property(x => x.RedeemedAt).HasColumnType("timestamp with time zone").IsRequired();
        builder.Property(x => x.CartTotal).HasColumnType("numeric(18,2)").IsRequired();
        builder.Property(x => x.DiscountApplied).HasColumnType("numeric(18,2)").IsRequired();
        builder
            .Property(x => x.IpAddress)
            .HasColumnType("varchar(45)")
            .HasMaxLength(45)
            .IsRequired();
        builder.Property(x => x.Result).HasConversion<int>().HasColumnType("int").IsRequired();
    }
}
