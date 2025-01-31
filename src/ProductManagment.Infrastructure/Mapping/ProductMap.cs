using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductManagment.Domain.Entities;

namespace ProductManagment.Infrastructure.Mappings
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            #region Entidade Base
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(x => x.CreatedAt).HasColumnName("created_at");
            builder.Property(x => x.UpdatedAt).HasColumnName("updated_at");
            #endregion

            #region Propriedades da Entidade
            builder.Property(p => p.StockId).HasColumnName("stock_id").IsRequired();
            builder.Property(p => p.Name).HasMaxLength(150).HasColumnName("name").IsRequired();
            builder.Property(p => p.Description).HasMaxLength(500).HasColumnName("description").IsRequired();
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)").HasColumnName("price").IsRequired();
            builder.Property(p => p.Category).HasMaxLength(100).HasColumnName("category").IsRequired();
            builder.Property(p => p.SKU).HasMaxLength(50).HasColumnName("sku").IsRequired();
            #endregion

            #region Relacionamentos
            builder.HasOne(p => p.Stock).WithOne(s => s.Product).HasForeignKey<Product>(p => p.StockId);
            #endregion
        }
    }
}
