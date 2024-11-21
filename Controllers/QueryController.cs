using BiblioTech.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BiblioTech.Controllers
{
    public class QueryController : Controller
    {
        private readonly Contexto contexto;

        public QueryController(Contexto context)
        {
            contexto = context;
        }

        public IActionResult Livros(string titulo) 
        {
            List<Livro> listaLivro = new List<Livro>();


            if (titulo == null) 
            {
                listaLivro = contexto.Livros
                  .Include(a => a.genero)
                  .Include(b => b.autor)
                  .OrderBy(o => o.autor)
                  .ThenBy(o => o.publicacao)
                  .ToList();
            }
            else
            {
                listaLivro = contexto.Livros
                 .Include(a => a.genero)
                 .Include(b => b.autor)
                 .Where(c => c.titulo.Contains(titulo))
                 .OrderBy(o => o.autor)
                 .ThenBy(o => o.publicacao)
                 .ToList();
            }
            

            return View(listaLivro);
        }

        public IActionResult Autores(string nome)
        {
            List<Autor> listaAutor = new List<Autor>();


            if (nome == null)
            {
                listaAutor = contexto.Autores
                  .OrderBy(o => o.nome)
                  .ToList();
            }
            else
            {
                listaAutor = contexto.Autores
                 .Where(c => c.nome.Contains(nome))
                 .OrderBy(o => o.nome)
                 .ToList();
            }


            return View(listaAutor);
        }



        public IActionResult Pesquisa()
        {
            return View();
        }

        public IActionResult PesquisaAutores()
        {
            return View();
        }
    }
}
