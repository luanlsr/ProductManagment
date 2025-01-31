using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductManagment.Domain.Entities;

namespace ProductManagment.Infrastructure.Mappings
{
    public class OrderItemMap : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");

            #region Entidade Base
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).HasColumnName("id");
            builder.Property(x => x.CreatedAt).HasColumnName("created_at");
            builder.Property(x => x.UpdatedAt).HasColumnName("updated_at");
            #endregion

            #region Propriedades da Entidade
            builder.Property(i => i.OrderId).HasColumnName("order_id").IsRequired();
            builder.Property(i => i.ProductId).HasColumnName("product_id").IsRequired();
            builder.Property(i => i.Quantity).HasColumnName("quantity").IsRequired();
            builder.Property(i => i.UnitPrice).HasColumnType("decimal(18,2)").HasColumnName("unit_price").IsRequired();
            #endregion

            #region Relacionamentos
            builder.HasOne(i => i.Order).WithMany(o => o.Items).HasForeignKey(i => i.OrderId);
            builder.HasOne(i => i.Product).WithMany().HasForeignKey(i => i.ProductId);
            #endregion
        }
    }
}
