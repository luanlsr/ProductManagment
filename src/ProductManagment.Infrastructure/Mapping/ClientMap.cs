using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductManagment.Domain.Entities;

namespace ProductManagment.Infrastructure.Mappings
{
    public class ClientMap : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clients");

            #region Entidade Base
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("id");
            builder.Property(x => x.CreatedAt).HasColumnName("created_at");
            builder.Property(x => x.UpdatedAt).HasColumnName("updated_at");
            #endregion

            #region Propriedades da Entidade
            builder.Property(c => c.UserId).HasColumnName("user_id").IsRequired();
            builder.Property(c => c.Name).HasMaxLength(100).HasColumnName("name").IsRequired();
            builder.Property(c => c.Email).HasMaxLength(100).HasColumnName("email").IsRequired();
            builder.Property(c => c.Phone).HasMaxLength(20).HasColumnName("phone");
            builder.Property(c => c.Document).HasMaxLength(14).HasColumnName("document").IsRequired();
            builder.Property(c => c.RegistrationDate).HasColumnName("registration_date").IsRequired();
            #endregion

            #region Relacionamentos
            builder.HasMany(c => c.Orders).WithOne(o => o.Client).HasForeignKey(o => o.ClientId);
            #endregion
        }
    }
}
