using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecreioFerias.Models
{
    [Table("Frequencia")]
    public class Frequencia
    {
        [Key()]
        public int FrequenciaID { get; set; }

        [Required(ErrorMessage = "Selecione o Dia da Semana!")]
        public string DiaDaSemana { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataAtual => DateTime.Now;

        public char Presente { get; set; }

        [Required(ErrorMessage ="Selecione o Aluno!")]
        public int AlunoId { get; set; }
        [ValidateNever]
        public Aluno Aluno { get; set; }


    }
}
