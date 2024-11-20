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

            string[] nomeLivro = {
                "1984", "O Senhor dos Anéis", "Orgulho e Preconceito", "Dom Quixote", "O Pequeno Príncipe",
                "Moby Dick", "A Revolução dos Bichos", "Cem Anos de Solidão", "Os Miseráveis", "Harry Potter e a Pedra Filosofal",
                "A Metamorfose", "O Grande Gatsby", "O Apanhador no Campo de Centeio", "Jane Eyre", "O Morro dos Ventos Uivantes",
                "Crime e Castigo", "Guerra e Paz", "Admirável Mundo Novo", "Ulisses", "O Sol é Para Todos",
                "A Divina Comédia", "As Aventuras de Sherlock Holmes", "Frankenstein", "Drácula", "Alice no País das Maravilhas",
                "Os Irmãos Karamazov", "O Conde de Monte Cristo", "E o Vento Levou", "Senhor das Moscas", "Coração das Trevas",
                "O Jardim Secreto", "Persuasão", "A Letra Escarlate", "Os Três Mosqueteiros", "As Vinhas da Ira",
                "A Sangue Frio", "O Processo", "O Livro das Mil e Uma Noites", "A História Sem Fim", "O Nome da Rosa",
                "O Diário de Anne Frank", "Fahrenheit 451", "O Estrangeiro", "Beloved", "O Homem Invisível",
                "O Hobbit", "Memórias Póstumas de Brás Cubas", "Capitães da Areia", "Dom Casmurro", "Grande Sertão: Veredas"
            };

            contexto.Database.ExecuteSqlRaw("delete from Livros");
            contexto.Database.ExecuteSqlRaw("DBCC CHECKIDENT('Livros', RESEED, 0)");

            Random rnd = new Random();
            for (int i = 0; i < 50; i++)
            {
                Livro livro = new Livro();
                //livro.titulo = "Livro " + i.ToString();
                livro.titulo = nomeLivro[i].ToString();
                livro.generoId = rnd.Next(1, 5);
                livro.autorId = rnd.Next(1, 4);
                livro.publicacao = rnd.Next(1970, 2006);
                livro.status = 1;

                contexto.Livros.Add(livro);
            }

            contexto.SaveChanges();

            return View(contexto.Livros.Include(a => a.genero).Include(b => b.autor).OrderBy(o => o.autor).ThenBy(d => d.publicacao).ToList());
        }


        public IActionResult Usuarios()
        {

            string[] nomesFemininos = {
                "Ana", "Beatriz", "Carla", "Diana", "Elisa",
                "Fernanda", "Gabriela", "Helena", "Isabela", "Juliana",
                "Karina", "Larissa", "Maria", "Natália", "Olívia",
                "Paula", "Raquel", "Sofia", "Tatiana", "Valéria",
                "Vanessa", "Yasmin", "Zoe", "Clara", "Emanuela"
            };

            string[] nomesMasculinos = {
                "Alexandre", "Bruno", "Carlos", "Daniel", "Eduardo",
                "Fernando", "Gabriel", "Henrique", "Igor", "João",
                "Lucas", "Mateus", "Nicolas", "Otávio", "Paulo",
                "Rafael", "Samuel", "Thiago", "Victor", "Wesley",
                "Yuri", "Zeca", "Caio", "Diego", "Fábio"
            };

            contexto.Database.ExecuteSqlRaw("delete from Usuarios");
            contexto.Database.ExecuteSqlRaw("DBCC CHECKIDENT('Usuarios', RESEED, 0)");

            Random rnd = new Random();
            for (int i = 0; i < 50; i++)
            {
                Usuario usuario = new Usuario();
                usuario.nome = (i % 2 == 0) ? nomesMasculinos[i / 2] : nomesFemininos[i / 2];
                usuario.cpf = rnd.Next(1000, 9999).ToString();

                DateOnly startDate = new DateOnly(2018, 1, 1);
                usuario.dataRegistro = startDate.AddDays(rnd.Next(0, 1825));


                contexto.Usuarios.Add(usuario);
            }

            contexto.SaveChanges();

            return View(contexto.Usuarios.OrderBy(d=>d.dataRegistro).ToList());
        }
    }
}
