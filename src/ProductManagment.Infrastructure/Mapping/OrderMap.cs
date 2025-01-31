using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductManagment.Domain.Entities;

namespace ProductManagment.Infrastructure.Mappings
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            #region Entidade Base
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).HasColumnName("id");
            builder.Property(x => x.CreatedAt).HasColumnName("created_at");
            builder.Property(x => x.UpdatedAt).HasColumnName("updated_at");
            #endregion

            #region Propriedades da Entidade
            builder.Property(o => o.ClientId).HasColumnName("client_id").IsRequired();
            builder.Property(o => o.OrderDate).HasColumnName("order_date").IsRequired();
            builder.Property(o => o.Status).HasColumnName("status").IsRequired();
            builder.Property(o => o.Total).HasColumnType("decimal(18,2)").HasColumnName("total").IsRequired();
            #endregion

            #region Relacionamentos
            builder.HasMany(o => o.Items).WithOne(i => i.Order).HasForeignKey(i => i.OrderId);
            #endregion
        }
    }
}
