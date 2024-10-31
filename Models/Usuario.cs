using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiblioTech.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        [Display(Name = "Usuario: ")]
        public int usuarioId { get; set; }

        [Display(Name = "Nome: ")]
        [Required(ErrorMessage = "Campo Nome é obrigatório")]
        [StringLength(35, ErrorMessage = "Tamanho máximo 35 caracteres")]
        public string nome { get; set; }

        [Display(Name = "CPF: ")]
        [Required(ErrorMessage = "Campo Nome é obrigatório")]
        [StringLength(11, ErrorMessage = "Tamanho máximo 11 caracteres")]
        public string cpf { get; set; }

        [Display(Name = "Data de Registro")]
        [Required(ErrorMessage = "Campo Data de Registro é obrigatório")]
        public DateOnly dataRegistro { get; set; }
    }
}