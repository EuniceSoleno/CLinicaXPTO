using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CLinicaXPTO.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Analise_da_actual_arquitetura : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActoClinico_Pedidos_PedidoMarcacaoId",
                table: "ActoClinico");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActoClinico",
                table: "ActoClinico");

            migrationBuilder.RenameTable(
                name: "ActoClinico",
                newName: "Actos");

            migrationBuilder.RenameIndex(
                name: "IX_ActoClinico_PedidoMarcacaoId",
                table: "Actos",
                newName: "IX_Actos_PedidoMarcacaoId");

            migrationBuilder.AddColumn<string>(
                name: "_Subsistema",
                table: "Actos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "_TipoConsulta",
                table: "Actos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "idProfissional",
                table: "Actos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Actos",
                table: "Actos",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "profissionais",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCompleto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Especialidade = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profissionais", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Actos_Pedidos_PedidoMarcacaoId",
                table: "Actos",
                column: "PedidoMarcacaoId",
                principalTable: "Pedidos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actos_Pedidos_PedidoMarcacaoId",
                table: "Actos");

            migrationBuilder.DropTable(
                name: "profissionais");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Actos",
                table: "Actos");

            migrationBuilder.DropColumn(
                name: "_Subsistema",
                table: "Actos");

            migrationBuilder.DropColumn(
                name: "_TipoConsulta",
                table: "Actos");

            migrationBuilder.DropColumn(
                name: "idProfissional",
                table: "Actos");

            migrationBuilder.RenameTable(
                name: "Actos",
                newName: "ActoClinico");

            migrationBuilder.RenameIndex(
                name: "IX_Actos_PedidoMarcacaoId",
                table: "ActoClinico",
                newName: "IX_ActoClinico_PedidoMarcacaoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActoClinico",
                table: "ActoClinico",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ActoClinico_Pedidos_PedidoMarcacaoId",
                table: "ActoClinico",
                column: "PedidoMarcacaoId",
                principalTable: "Pedidos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
