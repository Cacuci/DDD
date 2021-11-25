using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Pagamentos.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pagamentos",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MyProperty = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PedidoID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "varchar(100)", nullable: true),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NomeCartao = table.Column<string>(type: "varchar(250)", nullable: false),
                    NumeroCartao = table.Column<string>(type: "varchar(16)", nullable: false),
                    ExpiracaoCartao = table.Column<string>(type: "varchar(10)", nullable: false),
                    CvvCartao = table.Column<string>(type: "varchar(4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagamentos", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Transacoes",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PedidoID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PagamentoID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StatusTransacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transacoes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Transacoes_Pagamentos_PagamentoID",
                        column: x => x.PagamentoID,
                        principalTable: "Pagamentos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transacoes_PagamentoID",
                table: "Transacoes",
                column: "PagamentoID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transacoes");

            migrationBuilder.DropTable(
                name: "Pagamentos");
        }
    }
}
