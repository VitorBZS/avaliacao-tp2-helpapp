using HelpApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpApp.Infra.Data.EntitiesConfiguration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(200).IsRequired();
            builder.Property(p => p.Price).HasPrecision(10, 2);
            builder.HasOne(e => e.Category).WithMany(e => e.Products)
                .HasForeignKey(e => e.CategoryId);

            builder.HasData(
                new Product(1, "Caderno", "Caderno Espiral 100 folhas", 10.00m, 40, "cadernoImage.png")
                {
                    CategoryId = 1
                },
                new Product(2, "Borracha", "Borracha branca pequena", 3.00m, 90, "borrachaImage.png")
                {
                    CategoryId = 1
                },
                new Product(3, "Smartphone", "Smartphone 256GB", 1300.00m, 5, "smartphoneImage.png")
                {
                    CategoryId = 2
                }
            );
        }
    }
}
