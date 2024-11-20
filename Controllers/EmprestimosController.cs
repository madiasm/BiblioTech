using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BiblioTech.Models;
using Microsoft.AspNetCore.Authorization;

namespace BiblioTech.Controllers
{
    public class EmprestimosController : Controller
    {
        private readonly Contexto _context;

        public EmprestimosController(Contexto context)
        {
            _context = context;
        }

        // GET: Emprestimos
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var contexto = _context.Emprestimos.Include(e => e.livro).Include(e => e.usuario);
            return View(await contexto.ToListAsync());
        }

        // GET: Emprestimos/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emprestimo = await _context.Emprestimos
                .Include(e => e.livro)
                .Include(e => e.usuario)
                .FirstOrDefaultAsync(m => m.emprestimoId == id);
            if (emprestimo == null)
            {
                return NotFound();
            }

            return View(emprestimo);
        }

        // GET: Emprestimos/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["livroId"] = new SelectList(_context.Livros.Where(l => l.status == 1), "livroId", "titulo"); // Exibe apenas livros disponíveis
            ViewData["usuarioId"] = new SelectList(_context.Usuarios, "usuarioId", "cpf");
            return View();
        }

        // POST: Emprestimos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("emprestimoId,usuarioId,livroId,dataEmprestimo,dataDevolucao")] Emprestimo emprestimo)
        {
            if (ModelState.IsValid)
            {
                // Verificar se o livro está disponível (status = 1)
                var livro = await _context.Livros.FirstOrDefaultAsync(l => l.livroId == emprestimo.livroId);

                if (livro != null && livro.status == 1) // Livro disponível
                {
                    // Marcar o livro como emprestado (status = 0)
                    livro.status = 0;
                    _context.Livros.Update(livro); // Atualiza o status do livro
                    await _context.SaveChangesAsync(); // Salva a alteração no status do livro

                    // Adiciona o empréstimo
                    _context.Add(emprestimo);
                    await _context.SaveChangesAsync();

                    // Redireciona para a página de listagem de empréstimos
                    return RedirectToAction(nameof(Index));
                }
            }

            // Se o modelo não for válido ou se o livro não estiver disponível, redireciona para a página de listagem de empréstimos (Index)
            return RedirectToAction(nameof(Index));
        }

        // GET: Emprestimos/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emprestimo = await _context.Emprestimos.FindAsync(id);
            if (emprestimo == null)
            {
                return NotFound();
            }
            ViewData["livroId"] = new SelectList(_context.Livros, "livroId", "titulo", emprestimo.livroId);
            ViewData["usuarioId"] = new SelectList(_context.Usuarios, "usuarioId", "cpf", emprestimo.usuarioId);
            return View(emprestimo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("emprestimoId,usuarioId,livroId,dataEmprestimo,dataDevolucao")] Emprestimo emprestimo)
        {
            if (id != emprestimo.emprestimoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var emprestimoAntigo = await _context.Emprestimos
                        .Include(e => e.livro)
                        .FirstOrDefaultAsync(e => e.emprestimoId == emprestimo.emprestimoId);

                    var livroAntigo = emprestimoAntigo?.livro; // Livro que estava emprestado
                    var novoLivro = await _context.Livros.FindAsync(emprestimo.livroId); // Novo livro selecionado

                    if (livroAntigo != null && novoLivro != null)
                    {
                        // Se o novo livro estiver disponível (status = 1)
                        if (novoLivro.status == 1)
                        {
                            // Se o livro antigo estava emprestado (status = 0), volta ao status disponível (status = 1)
                            if (livroAntigo.status == 0)
                            {
                                livroAntigo.status = 1;
                                _context.Livros.Update(livroAntigo);
                            }

                            // Marca o novo livro como emprestado (status = 0)
                            novoLivro.status = 0;
                            _context.Livros.Update(novoLivro);

                            // Atualiza o empréstimo com o novo livro
                            emprestimoAntigo.livroId = emprestimo.livroId;
                            _context.Update(emprestimoAntigo);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            // Se o novo livro não estiver disponível, remove o empréstimo
                            _context.Emprestimos.Remove(emprestimoAntigo);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    var existeEmprestimo = await _context.Emprestimos
                        .AnyAsync(e => e.emprestimoId == emprestimo.emprestimoId);

                    if (!existeEmprestimo)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["livroId"] = new SelectList(_context.Livros, "livroId", "titulo", emprestimo.livroId);
            ViewData["usuarioId"] = new SelectList(_context.Usuarios, "usuarioId", "cpf", emprestimo.usuarioId);
            return View(emprestimo);
        }

        // GET: Emprestimos/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emprestimo = await _context.Emprestimos
                .Include(e => e.livro)
                .Include(e => e.usuario)
                .FirstOrDefaultAsync(m => m.emprestimoId == id);
            if (emprestimo == null)
            {
                return NotFound();
            }

            return View(emprestimo);
        }

        // POST: Emprestimos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var emprestimo = await _context.Emprestimos.FindAsync(id);
            if (emprestimo != null)
            {
                var livro = await _context.Livros.FindAsync(emprestimo.livroId);
                if (livro != null)
                {
                    livro.status = 1; // Marca o livro como disponível
                    _context.Livros.Update(livro);
                }

                _context.Emprestimos.Remove(emprestimo);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
