using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CLinicaXPTO.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Table_PedidosMarcacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidoMarcacao_Utentes_UtenteId",
                table: "PedidoMarcacao");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PedidoMarcacao",
                table: "PedidoMarcacao");

            migrationBuilder.RenameTable(
                name: "PedidoMarcacao",
                newName: "Pedidos");

            migrationBuilder.RenameIndex(
                name: "IX_PedidoMarcacao_UtenteId",
                table: "Pedidos",
                newName: "IX_Pedidos_UtenteId");

            migrationBuilder.AddColumn<string>(
                name: "EstadoMarcacao",
                table: "Pedidos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pedidos",
                table: "Pedidos",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ActoClinico",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PedidoMarcacaoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActoClinico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActoClinico_Pedidos_PedidoMarcacaoId",
                        column: x => x.PedidoMarcacaoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActoClinico_PedidoMarcacaoId",
                table: "ActoClinico",
                column: "PedidoMarcacaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Utentes_UtenteId",
                table: "Pedidos",
                column: "UtenteId",
                principalTable: "Utentes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Utentes_UtenteId",
                table: "Pedidos");

            migrationBuilder.DropTable(
                name: "ActoClinico");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pedidos",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "EstadoMarcacao",
                table: "Pedidos");

            migrationBuilder.RenameTable(
                name: "Pedidos",
                newName: "PedidoMarcacao");

            migrationBuilder.RenameIndex(
                name: "IX_Pedidos_UtenteId",
                table: "PedidoMarcacao",
                newName: "IX_PedidoMarcacao_UtenteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PedidoMarcacao",
                table: "PedidoMarcacao",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoMarcacao_Utentes_UtenteId",
                table: "PedidoMarcacao",
                column: "UtenteId",
                principalTable: "Utentes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
