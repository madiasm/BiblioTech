using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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
    }

}