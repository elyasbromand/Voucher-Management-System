using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VoucherSystem.Domain.Entities;

namespace VoucherSystem.Infrastructure.Persistence.Configurations;

public class FraudFlagConfiguration : IEntityTypeConfiguration<FraudFlag>
{
    public void Configure(EntityTypeBuilder<FraudFlag> builder)
    {
        builder.ToTable("FraudFlags");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnType("uuid");

        builder.Property(x => x.UserId).HasColumnType("uuid").IsRequired();
        builder.Property(x => x.VoucherId).HasColumnType("uuid");
        builder
            .Property(x => x.Reason)
            .HasColumnType("varchar(500)")
            .HasMaxLength(500)
            .IsRequired();
        builder.Property(x => x.RiskScore).HasColumnType("int").IsRequired();
        builder.Property(x => x.FlaggedAt).HasColumnType("timestamp with time zone").IsRequired();
        builder.Property(x => x.Resolved).HasColumnType("boolean").IsRequired();
        builder.Property(x => x.ResolvedAt).HasColumnType("timestamp with time zone");
    }
}
