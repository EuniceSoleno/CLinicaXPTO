using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CLinicaXPTO.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Criarcao_do_Banco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Utentes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCompleto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataNascimento = table.Column<DateOnly>(type: "date", nullable: false),
                    Genero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fotografia = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Telemovel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Morada = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utentes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PedidoMarcacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UtenteId = table.Column<int>(type: "int", nullable: false),
                    DataAgendada = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataPedido = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IntervaloData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Observacoes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoMarcacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PedidoMarcacao_Utentes_UtenteId",
                        column: x => x.UtenteId,
                        principalTable: "Utentes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PedidoMarcacao_UtenteId",
                table: "PedidoMarcacao",
                column: "UtenteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidoMarcacao");

            migrationBuilder.DropTable(
                name: "Utentes");
        }
    }
}
