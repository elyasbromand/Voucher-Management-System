using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VoucherSystem.Domain.Entities;

namespace VoucherSystem.Infrastructure.Persistence.Configurations;

public class CampaignConfiguration : IEntityTypeConfiguration<Campaign>
{
    public void Configure(EntityTypeBuilder<Campaign> builder)
    {
        builder.ToTable("Campaigns");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnType("uuid");

        builder.Property(x => x.Name).HasColumnType("varchar(200)").HasMaxLength(200).IsRequired();
        builder.Property(x => x.StartDate).HasColumnType("timestamp with time zone").IsRequired();
        builder.Property(x => x.EndDate).HasColumnType("timestamp with time zone").IsRequired();
        builder.Property(x => x.BudgetCap).HasColumnType("numeric(18,2)");
        builder.Property(x => x.TotalDiscountIssued).HasColumnType("numeric(18,2)").IsRequired();
        builder.Property(x => x.Status).HasConversion<int>().HasColumnType("int").IsRequired();
        builder.Property(x => x.CreatedAt).HasColumnType("timestamp with time zone").IsRequired();
    }
}
