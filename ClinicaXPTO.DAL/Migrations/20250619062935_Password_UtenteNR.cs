using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CLinicaXPTO.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Password_UtenteNR : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "DataNascimento",
                table: "utentesNaoRegistados",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "utentesNaoRegistados",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "utentesNaoRegistados");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataNascimento",
                table: "utentesNaoRegistados",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }
    }
}
