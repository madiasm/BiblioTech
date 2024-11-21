using System.Data;
using BiblioTech.Models.Consulta;
using BiblioTech.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BiblioTech.Extra;

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



        public IActionResult AgruparDados()
        {
            // Obter empréstimos agrupados por autor
            var emprestimosPorAutor = from emprestimo in contexto.Emprestimos
                                       .Include(e => e.livro)
                                       .ThenInclude(l => l.autor)
                                      where emprestimo.livro != null && emprestimo.livro.autor != null
                                      group emprestimo by emprestimo.livro.autor.nome into grupoAutor
                                      orderby grupoAutor.Key
                                      select new DadosAgrupadosModel
                                      {
                                          Autor = grupoAutor.Key,
                                          QuantidadeEmprestimos = grupoAutor.Count(),
                                          Genero = null,
                                          QuantidadeLivros = null
                                      };

            // Obter empréstimos de livros agrupados por gênero
            var livrosPorGenero = from emprestimo in contexto.Emprestimos
                                  .Include(e => e.livro)
                                  .ThenInclude(l => l.genero)
                                  where emprestimo.livro != null && emprestimo.livro.genero != null
                                  group emprestimo by emprestimo.livro.genero.assunto into grupoGenero
                                  orderby grupoGenero.Key
                                  select new DadosAgrupadosModel
                                  {
                                      Autor = null,
                                      QuantidadeEmprestimos = null,
                                      Genero = grupoGenero.Key,
                                      QuantidadeLivros = grupoGenero.Count()
                                  };

            // Combinar os dois conjuntos de dados (Autores e Gêneros de livros emprestados)
            var resultado = emprestimosPorAutor.Concat(livrosPorGenero).ToList();

            return View(resultado);
        }


        public IActionResult AgruparEmprestimoAnoMes()
        {
            IEnumerable<EmprestimoAnoMes> lstEmpAnoMes = from item in contexto.Emprestimos.ToList()

                                                         let ano = item.dataEmprestimo.Year
                                                         let mes = item.dataEmprestimo.Month
                                                         group item by new { ano, mes }

                                                    into grupo

                                                         orderby grupo.Key.ano, grupo.Key.mes
                                                         select new EmprestimoAnoMes
                                                         {
                                                             ano = grupo.Key.ano,
                                                             mes = grupo.Key.mes,
                                                             quantidade = grupo.Count()
                                                         };

            return View(lstEmpAnoMes);
        }

        public IActionResult Pivot()
        {

            IEnumerable<EmprestimoAnoMes> lstAtendAnoMes = from item in contexto.Emprestimos
                                    .ToList()
                                                            let ano = item.dataEmprestimo.Year
                                                            let mes = item.dataEmprestimo.Month
                                                            group item by new { ano, mes }
                                   into grupo
                                                            orderby grupo.Key.ano, grupo.Key.mes
                                                            select new EmprestimoAnoMes
                                                            {
                                                                ano = grupo.Key.ano,
                                                                mes = grupo.Key.mes,
                                                                quantidade = grupo.Count()
                                                            };

            //Gerar Pivot
            var PivotTableInsArea = lstAtendAnoMes.ToList().ToPivotTable(
                    pivo => pivo.mes, //coluna
                    pivo => pivo.ano, //linha
                    pivos => (pivos.Any() ? pivos.Sum(x => Convert.ToSingle(x.quantidade)) : 0)); //valor das células

            //Converter DataTable do Pivot para Lista, permitir que o asp net core, imprima depois
            List<PivotEmprestimo> lista = new List<PivotEmprestimo>();
            lista = (from DataRow linha in PivotTableInsArea.Rows
                     select new PivotEmprestimo()
                     {
                         ano = linha[0].ToString(),
                         janeiro = Convert.ToSingle(linha[1]),
                         fevereiro = Convert.ToSingle(linha[2]),
                         marco = Convert.ToSingle(linha[3]),
                         abril = Convert.ToSingle(linha[4]),
                         maio = Convert.ToSingle(linha[5]),
                         junho = Convert.ToSingle(linha[6]),
                         julho = Convert.ToSingle(linha[7]),
                         agosto = Convert.ToSingle(linha[8]),
                         setembro = Convert.ToSingle(linha[9]),
                         outubro = Convert.ToSingle(linha[10]),
                         novembro = Convert.ToSingle(linha[11]),
                         dezembro = Convert.ToSingle(linha[12]),
                         total = Convert.ToSingle(linha[1]) + Convert.ToSingle(linha[2]) + Convert.ToSingle(linha[3]) + Convert.ToSingle(linha[4]) + Convert.ToSingle(linha[5]) + Convert.ToSingle(linha[6]) +
                                 Convert.ToSingle(linha[7]) + Convert.ToSingle(linha[8]) + Convert.ToSingle(linha[9]) + Convert.ToSingle(linha[10]) + Convert.ToSingle(linha[11]) + Convert.ToSingle(linha[12]),
                     }).ToList();

            return View(lista);
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
