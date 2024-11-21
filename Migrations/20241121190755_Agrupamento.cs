using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiblioTech.Migrations
{
    /// <inheritdoc />
    public partial class Agrupamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "livro",
                table: "EmprestAutLiv");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "livro",
                table: "EmprestAutLiv",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
