using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Catalogo.Domain.Models;

namespace Store.Catalogo.Data.Mapping
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {   
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(c => c.ID);

            builder.Property(c => c.Nome)
                   .IsRequired()
                   .HasColumnType("varchar(250)");

            builder.Property(c => c.Descricao)
                   .IsRequired()
                   .HasColumnType("varchar(500)");

            builder.Property(c => c.Imagem)
                   .IsRequired()
                   .HasColumnType("varchar(500)");

            builder.OwnsOne(c => c.Dimensoes, d =>
            {
                d.Property(c => c.Altura)
                 .HasColumnName("Altura")
                 .HasColumnType("int");

                d.Property(c => c.Altura)
                 .HasColumnName("Largura")
                 .HasColumnType("int");

                d.Property(c => c.Altura)
                 .HasColumnName("Profundidade")
                 .HasColumnType("int");
            });

            builder.ToTable("Produtos");
        }
    }
}
