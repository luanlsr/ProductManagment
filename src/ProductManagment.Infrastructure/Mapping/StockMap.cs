using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductManagment.Domain.Entities;

namespace ProductManagment.Infrastructure.Mappings
{
    public class StockMap : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.ToTable("Stocks");

            #region Entidade Base
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).HasColumnName("id");
            builder.Property(x => x.CreatedAt).HasColumnName("created_at");
            builder.Property(x => x.UpdatedAt).HasColumnName("updated_at");
            #endregion

            #region Propriedades da Entidade
            builder.Property(s => s.ProductId).HasColumnName("product_id").IsRequired();
            builder.Property(s => s.Quantity).HasColumnName("quantity").IsRequired();
            #endregion

            #region Relacionamentos
            builder.HasOne(s => s.Product).WithOne(p => p.Stock).HasForeignKey<Stock>(s => s.ProductId);
            #endregion
        }
    }
}
