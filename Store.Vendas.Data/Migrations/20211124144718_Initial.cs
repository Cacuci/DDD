using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Vendas.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "MinhaSequencia",
                startValue: 1000L);

            migrationBuilder.CreateTable(
                name: "Vouchers",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Codigo = table.Column<string>(type: "varchar(100)", nullable: false),
                    Percentual = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ValorDesconto = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    TipoDescontoVoucher = table.Column<int>(type: "int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataUtilizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataValidade = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    Utilizado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vouchers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Codigo = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR MinhaSequencia"),
                    ClienteID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VoucherID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VoucherUtilizado = table.Column<bool>(type: "bit", nullable: false),
                    Desconto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DadaCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PedidoStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Pedidos_Vouchers_VoucherID",
                        column: x => x.VoucherID,
                        principalTable: "Vouchers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PedidoItems",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PedidoID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProdutoID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProdutoNome = table.Column<string>(type: "varchar(250)", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    ValorUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoItems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PedidoItems_Pedidos_PedidoID",
                        column: x => x.PedidoID,
                        principalTable: "Pedidos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PedidoItems_PedidoID",
                table: "PedidoItems",
                column: "PedidoID");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_VoucherID",
                table: "Pedidos",
                column: "VoucherID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidoItems");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Vouchers");

            migrationBuilder.DropSequence(
                name: "MinhaSequencia");
        }
    }
}
