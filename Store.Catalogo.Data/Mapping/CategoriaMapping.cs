using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Catalago.Domain.Models;

namespace Store.Catalogo.Data.Mapping
{
    public class CategoriaMapping : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.HasKey(c => c.ID);

            builder.Property(c => c.Nome)
                   .IsRequired()
                   .HasColumnType("varchar(250)");

            // 1 : N => Categorias : Produtos

            builder.HasMany(c => c.Produtos)
                   .WithOne(p => p.Categoria)
                   .HasForeignKey(p => p.CategoriaID);

            builder.ToTable("Categorias");
        }
    }
}
