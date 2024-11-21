using System;

namespace BiblioTech.Models
{
    public class DadosAgrupadosModel
    {
        public int id { get; set; }
        public string? Autor { get; set; } // Nome do autor
        public int? QuantidadeEmprestimos { get; set; } // Quantidade de empréstimos
        public string? Genero { get; set; } // Nome do gênero
        public int? QuantidadeLivros { get; set; } // Quantidade de livros
    }
}
