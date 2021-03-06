// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Store.Pagamentos.Data;

namespace Store.Pagamentos.Data.Migrations
{
    [DbContext(typeof(PagamentoContext))]
    [Migration("20211124142744_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Store.Pagamentos.Business.Pagamento", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CvvCartao")
                        .IsRequired()
                        .HasColumnType("varchar(4)");

                    b.Property<string>("ExpiracaoCartao")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<Guid>("MyProperty")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NomeCartao")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<string>("NumeroCartao")
                        .IsRequired()
                        .HasColumnType("varchar(16)");

                    b.Property<Guid>("PedidoID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Status")
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ID");

                    b.ToTable("Pagamentos");
                });

            modelBuilder.Entity("Store.Pagamentos.Business.Transacao", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PagamentoID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PedidoID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("StatusTransacao")
                        .HasColumnType("int");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ID");

                    b.HasIndex("PagamentoID")
                        .IsUnique();

                    b.ToTable("Transacoes");
                });

            modelBuilder.Entity("Store.Pagamentos.Business.Transacao", b =>
                {
                    b.HasOne("Store.Pagamentos.Business.Pagamento", "Pagamento")
                        .WithOne("Transacao")
                        .HasForeignKey("Store.Pagamentos.Business.Transacao", "PagamentoID")
                        .IsRequired();

                    b.Navigation("Pagamento");
                });

            modelBuilder.Entity("Store.Pagamentos.Business.Pagamento", b =>
                {
                    b.Navigation("Transacao");
                });
#pragma warning restore 612, 618
        }
    }
}
