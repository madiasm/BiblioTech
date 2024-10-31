using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiblioTech.Models
{
    [Table("Autores")]
    public class Autor
    {
        [Key]
        [Display(Name = "Autor: ")]
        public int autorId { get; set; }

        [Display(Name = "Nome: ")]
        [Required(ErrorMessage = "Campo Nome é obrigatório")]
        [StringLength(35, ErrorMessage = "Tamanho máximo 35 caracteres")]
        public string nome { get; set; }

        [Display(Name = "Ano de Nascimento: ")]
        [Required(ErrorMessage = "Campo Ano de Nascimento é obrigatório")]
        public int nascimento { get; set; }
    }
}