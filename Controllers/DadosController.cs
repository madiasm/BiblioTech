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
                "O Hobbit", "Memórias Póstumas de Brás Cubas", "Capitães da Areia", "Dom Casmurro", "Grande Sertão: Veredas",
                "O Pêndulo de Foucault", "O Senhor das Moscas", "O Médico e o Monstro", "O Velho e o Mar", "O Último dos Moicanos",
                "O Conde de Monte Cristo", "Os Pilares da Terra", "O Príncipe", "A Arte da Guerra", "O Hobbit",
                "O Silmarillion", "A Revolução Francesa", "O Cão dos Baskerville", "O Tesouro de Praga", "O Poder do Agora",
                "O Código Da Vinci", "Harry Potter e a Câmara Secreta", "Harry Potter e o Prisioneiro de Azkaban", "O Último Reino", "O Silêncio dos Inocentes",
                "A Sombra do Vento", "A Menina que Roubava Livros", "O Mestre e Margarida", "A Guerra dos Tronos", "O Príncipe",
                "O Rei Leão", "A Ilha do Tesouro", "A Lenda do Cavaleiro Sem Cabeça", "A Trilogia do Senhor dos Anéis", "As Crônicas de Nárnia",
                "A Torre Negra", "As Aventuras de Huckleberry Finn", "A Caverna", "O Evangelho Segundo Jesus Cristo", "O Rei Arthur e os Cavaleiros da Távola Redonda",
                "A Luta pela Liberdade", "O Manual de Sobrevivência do Apocalipse Zumbi", "Memórias de uma Gueixa", "O Chamado de Cthulhu", "O Mundo Perdido",
                "A Busca do Graal", "O Império do Sol", "O Monge e o Executivo", "Os Meninos da Rua Paulo", "O Cavaleiro Andante",
                "O Esplendor de Deus", "A Assombração da Casa da Colina", "O Grande Inquisidor", "O Estrangeiro", "A História da Eternidade",
                "A Revolução dos Bichos", "O Retrato de Dorian Gray", "O Pequeno Príncipe", "A Ilha do Medo", "O Espelho",
                "A Mão que Cria o Olho", "O Príncipe Caspian", "A Cidade do Sol", "O Segredo", "O Mundo do Sofrimento",
                "O Lado Sombrio do Coração", "A Origem das Espécies", "O Enigma do Quatro", "O Ciclo da Herança", "A Verdade sobre o Caso Harry Quebert",
                "O Amor nos Tempos de Cólera", "O Fim da Eternidade", "O Capítulo da Morte", "O Inocente", "O Rei do Inverno",
                "O Vendedor de Sonhos", "O Planeta dos Macacos", "A Viagem do Elefante", "O Corredor Polaco", "O Caminho das Águas",
                "O Último Temptor", "O Veneno da Madrugada", "O Coração das Sombras", "O Vingador", "O Ladrão de Raios",
                "O Monstro", "O Retrato da Senhora", "O Castelo", "A Bíblia", "O Segredo de Emma Corrigan",
                "O Testamento", "A Ronda", "O Dia do Cervo", "O Príncipe das Sombras", "O Filho do Homem", "A Abissal",
                "O Buraco da Agulha", "O Levante", "O Mistério do Círculo de Ouro", "A Cartomante", "O Amor é Para os Corajosos",
                "O Rumo da Tempestade", "O Sétimo Selo", "A Moeda", "O Anjo da Morte", "A Festa de Babette", "O Ponto de Vista",
                "A Terra das Sombras", "O Jardim das Aflições", "O Tempo e o Vento", "O Mistério de Pemberley", "A Lenda de Sleepy Hollow",
                "O Casamento", "O Amor de Clarice", "O Sábio", "A História da Arte", "O Feiticeiro de Oz", "O Guardião",
                "O Caminho do Guerreiro Pacífico", "O Jogo do Anjo", "A Guerra das Mulheres", "O Senhor do Mundo", "O Príncipe do Egito"
            };


            contexto.Database.ExecuteSqlRaw("delete from Livros");
            contexto.Database.ExecuteSqlRaw("DBCC CHECKIDENT('Livros', RESEED, 0)");

            Random rnd = new Random();
            for (int i = 0; i < 150; i++)
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


        public IActionResult Emprestimos()
        {

            contexto.Database.ExecuteSqlRaw("delete from Emprestimos");
            contexto.Database.ExecuteSqlRaw("UPDATE Livros SET status = 1 WHERE status = 0");
            contexto.Database.ExecuteSqlRaw("DBCC CHECKIDENT('Emprestimos', RESEED, 0)");

            Random rnd = new Random();


            for (int i = 1; i <= 50; i++)
            {
                Emprestimo emprestimo = new Emprestimo();
                emprestimo.usuarioId = i;
                emprestimo.livroId = i;


                emprestimo.dataEmprestimo = Convert.ToDateTime("01/01/2023").AddDays(rnd.Next(0, 730)).AddHours(rnd.Next(0, 24)).AddMinutes(rnd.Next(0, 60)).AddSeconds(rnd.Next(0, 60));
                emprestimo.dataDevolucao = emprestimo.dataEmprestimo.AddDays(7);



                contexto.Emprestimos.Add(emprestimo);

                var livro = contexto.Livros.FirstOrDefault(l => l.livroId == emprestimo.livroId);
                if (livro != null)
                {
                    livro.status = 0;
                }
            }

            contexto.SaveChanges();

            return View(contexto.Emprestimos.Include(u=>u.usuario).Include(l=>l.livro).OrderBy(a=>a.dataEmprestimo).ToList());
        }
    }
}
