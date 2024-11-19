using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BiblioTech.Models;
using Microsoft.AspNetCore.Authorization;

namespace BiblioTech.Controllers
{
    public class DadosController : Controller
    {
        private readonly Contexto contexto;

        public DadosController(Contexto context)
        {
            contexto = context;
        }

        public IActionResult Livros ()
        {

            string[] nomeLivro = {"1984", "O Senhor dos Anéis", "Orgulho e Preconceito", "Dom Quixote", "O Pequeno Príncipe", "Moby Dick", "A Revolução dos Bichos", "Cem Anos de Solidão", "Os Miseráveis", "Harry Potter e a Pedra Filosofal"};

            contexto.Database.ExecuteSqlRaw("delete from Livros");
            contexto.Database.ExecuteSqlRaw("DBCC CHECKIDENT('Livros', RESEED, 0)");

            Random rnd = new Random();
            for (int i = 0; i < 100; i++)
            {
                Livro livro = new Livro();
                //livro.titulo = "Livro " + i.ToString();
                livro.titulo = nomeLivro[rnd.Next(0, 10)].ToString();
                livro.generoId = rnd.Next(1, 5);
                livro.autorId = rnd.Next(1, 4);
                livro.publicacao = rnd.Next(1970, 2006);
                livro.status = 1;

                contexto.Livros.Add(livro);
            }

            contexto.SaveChanges();

            return View(contexto.Livros.Include(a=>a.genero).Include(b=>b.autor).ToList());
        }
    }
}
