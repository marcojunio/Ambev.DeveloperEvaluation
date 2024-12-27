using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(c => c.Id);
       
        builder.Property(c => c.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(c => c.Name).IsRequired().HasMaxLength(50);
        builder.Property(c => c.StockQuantity).IsRequired();
        builder.Property(c => c.UnitPrice).IsRequired().HasPrecision(18,2);
        builder.Property(u => u.CreatedAt).HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
        builder.Property(u => u.UpdatedAt).HasColumnType("timestamp with time zone");
        
        builder.Property(c => c.UserId).IsRequired();
        
        builder
            .HasOne(c => c.User)
            .WithMany(c => c.Products)
            .HasForeignKey(c => c.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}