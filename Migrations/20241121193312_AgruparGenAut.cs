using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiblioTech.Migrations
{
    /// <inheritdoc />
    public partial class AgruparGenAut : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmprestAutLiv");

            migrationBuilder.CreateTable(
                name: "DadosAgrupadosModels",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Autor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuantidadeEmprestimos = table.Column<int>(type: "int", nullable: true),
                    Genero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuantidadeLivros = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DadosAgrupadosModels", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DadosAgrupadosModels");

            migrationBuilder.CreateTable(
                name: "EmprestAutLiv",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    autor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    quantidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmprestAutLiv", x => x.id);
                });
        }
    }
}
