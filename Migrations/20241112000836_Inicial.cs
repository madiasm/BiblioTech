using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiblioTech.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autores",
                columns: table => new
                {
                    autorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                    nascimento = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autores", x => x.autorId);
                });

            migrationBuilder.CreateTable(
                name: "Generos",
                columns: table => new
                {
                    generoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    assunto = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generos", x => x.generoId);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    usuarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                    cpf = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    dataRegistro = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.usuarioId);
                });

            migrationBuilder.CreateTable(
                name: "Livros",
                columns: table => new
                {
                    livroId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    autorId = table.Column<int>(type: "int", nullable: false),
                    generoId = table.Column<int>(type: "int", nullable: false),
                    publicacao = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livros", x => x.livroId);
                    table.ForeignKey(
                        name: "FK_Livros_Autores_autorId",
                        column: x => x.autorId,
                        principalTable: "Autores",
                        principalColumn: "autorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Livros_Generos_generoId",
                        column: x => x.generoId,
                        principalTable: "Generos",
                        principalColumn: "generoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Emprestimos",
                columns: table => new
                {
                    emprestimoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usuarioId = table.Column<int>(type: "int", nullable: false),
                    livroId = table.Column<int>(type: "int", nullable: false),
                    dataEmprestimo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dataDevolucao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emprestimos", x => x.emprestimoId);
                    table.ForeignKey(
                        name: "FK_Emprestimos_Livros_livroId",
                        column: x => x.livroId,
                        principalTable: "Livros",
                        principalColumn: "livroId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Emprestimos_Usuarios_usuarioId",
                        column: x => x.usuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "usuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Emprestimos_livroId",
                table: "Emprestimos",
                column: "livroId");

            migrationBuilder.CreateIndex(
                name: "IX_Emprestimos_usuarioId",
                table: "Emprestimos",
                column: "usuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Livros_autorId",
                table: "Livros",
                column: "autorId");

            migrationBuilder.CreateIndex(
                name: "IX_Livros_generoId",
                table: "Livros",
                column: "generoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Emprestimos");

            migrationBuilder.DropTable(
                name: "Livros");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Autores");

            migrationBuilder.DropTable(
                name: "Generos");
        }
    }
}
