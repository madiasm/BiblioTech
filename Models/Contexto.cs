using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using BiblioTech.Models;
using BiblioTech.Models.Consulta;

namespace BiblioTech.Models
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options) { }

        public DbSet<Autor> Autores { get; set; }

        public DbSet<Emprestimo> Emprestimos { get; set; }

        public DbSet<Genero> Generos { get; set; }

        public DbSet<Livro> Livros { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<DadosAgrupadosModel> DadosAgrupadosModels { get; set; }
        public DbSet<BiblioTech.Models.EmprestimoAnoMes> EmprestimoAnoMes { get; set; }
        public DbSet<BiblioTech.Models.Consulta.PivotEmprestimo> PivotEmprestimo { get; set; }
    }

}