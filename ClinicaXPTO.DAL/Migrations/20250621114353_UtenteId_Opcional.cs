using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CLinicaXPTO.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UtenteId_Opcional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Utentes_UtenteId",
                table: "Pedidos");

            migrationBuilder.AlterColumn<int>(
                name: "UtenteId",
                table: "Pedidos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Utentes_UtenteId",
                table: "Pedidos",
                column: "UtenteId",
                principalTable: "Utentes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Utentes_UtenteId",
                table: "Pedidos");

            migrationBuilder.AlterColumn<int>(
                name: "UtenteId",
                table: "Pedidos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Utentes_UtenteId",
                table: "Pedidos",
                column: "UtenteId",
                principalTable: "Utentes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
