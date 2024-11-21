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
            List<Livro> lista = new List<Livro>();


            if (titulo == null) 
            {
                lista = contexto.Livros
                  .Include(a => a.genero)
                  .Include(b => b.autor)
                  .OrderBy(o => o.autor)
                  .ThenBy(o => o.publicacao)
                  .ToList();
            }
            else
            {
                lista = contexto.Livros
                 .Include(a => a.genero)
                 .Include(b => b.autor)
                 .Where(c => c.titulo.Contains(titulo))
                 .OrderBy(o => o.autor)
                 .ThenBy(o => o.publicacao)
                 .ToList();
            }
            

            return View(lista);
        }

        public IActionResult Pesquisa()
        {
            return View();
        }
    }
}
