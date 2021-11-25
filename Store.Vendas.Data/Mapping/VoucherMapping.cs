using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Vendas.Domain.Models;

namespace Store.Catalogo.Data.Mappings
{
    public class VoucherMapping : IEntityTypeConfiguration<Voucher>
    {
        public void Configure(EntityTypeBuilder<Voucher> builder)
        {
            builder.HasKey(c => c.ID);


            builder.Property(c => c.Codigo)
                .IsRequired()
                .HasColumnType("varchar(100)");

            // 1 : N => Voucher : Pedidos
            builder.HasMany(c => c.Pedidos)
                .WithOne(c => c.Voucher)
                .HasForeignKey(c => c.VoucherID);

            builder.ToTable("Vouchers");
        }
    }
}