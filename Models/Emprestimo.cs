using BiblioTech.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiblioTech.Models
{
    [Table("Emprestimos")]
    public class Emprestimo
    {
        [Key]
        [Display(Name = "Emprestimo: ")]
        public int emprestimoId { get; set; }



        [Display(Name = "Usuario: ")]
        public Usuario usuario { get; set; }

        [Display(Name = "Usuario: ")]
        public int usuarioId { get; set; }



        [Display(Name = "Livro: ")]
        public Livro livro { get; set; }

        [Display(Name = "Livro: ")]
        public int livroId { get; set; }



        [Display(Name = "Data de Emprestimo")]
        [Required(ErrorMessage = "Campo Data de emprestimo é obrigatório")]
        public DateTime dataEmprestimo { get; set; }

        [Display(Name = "Data de Devolução")]
        [Required(ErrorMessage = "Campo Data de devolução é obrigatório")]
        public DateTime dataDevolucao { get; set; }


    }
}