using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecreioFerias.Models
{
    [Table("Operador")]
    public class Operador
    {
        [Key()]
        public int OperadorId { get; set; }

        [Required(ErrorMessage ="Preencha o Login:")]
        [Display(Name ="Login:")]
        [MaxLength(20,ErrorMessage ="Campo deve ter no máximo de 20 caracteres.")]
        [MinLength(3,ErrorMessage ="Campo deve ter no mínimo de 3 caracteres.")]
        public string Login { get; set; }

        [Required(ErrorMessage ="Preencha a Senha:")]
        [Display(Name ="Senha:")]
        [DataType(DataType.Password)]
        [MaxLength(8, ErrorMessage ="Senha deve ter no máximo de 8 caracteres.")]
        [MinLength(4,ErrorMessage ="Senha deve ter no mínimo de 4 caracteres.")]
        public string Senha { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Confirme a senha.")]
        [Display(Name = "Confirmar Senha:")]
        [DataType(DataType.Password)]
        [Compare("Senha", ErrorMessage = "As senhas não coincidem.")]
        public string CompararSenha { get; set; }

        [Required(ErrorMessage ="Selecione a Situação:")]
        [Display(Name ="Situação")]
        public bool Situacao { get; set; }
    }
}
