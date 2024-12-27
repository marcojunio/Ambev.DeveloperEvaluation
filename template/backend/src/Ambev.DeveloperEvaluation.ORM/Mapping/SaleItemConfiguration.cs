using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
{
    public void Configure(EntityTypeBuilder<SaleItem> builder)
    {
        builder.ToTable("SaleItems");

        builder.HasKey(c => c.Id);
       
        builder.Property(c => c.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");
        
        builder.Property(c => c.Quantity).IsRequired();
        builder.Property(c => c.Total).IsRequired().HasPrecision(18, 2);
        builder.Property(c => c.UnitPrice).IsRequired().HasPrecision(18, 2);
        builder.Property(c => c.Discount).HasPrecision(18, 2);
        builder.Property(c => c.UserId).IsRequired();
        builder.Property(c => c.SaleId).IsRequired();
        builder.Property(c => c.ProductId).IsRequired();
        builder.Property(u => u.CreatedAt).HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
        builder.Property(u => u.UpdatedAt).HasColumnType("timestamp with time zone");
        
        builder
            .HasOne(c => c.User)
            .WithMany(c => c.SaleItems)
            .HasForeignKey(c => c.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .HasOne(c => c.Sale)
            .WithMany(c => c.Items)
            .HasForeignKey(c => c.SaleId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}