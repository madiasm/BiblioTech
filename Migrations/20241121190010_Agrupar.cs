using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiblioTech.Migrations
{
    /// <inheritdoc />
    public partial class Agrupar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmprestAutLiv",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    autor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    livro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    quantidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmprestAutLiv", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmprestAutLiv");
        }
    }
}
