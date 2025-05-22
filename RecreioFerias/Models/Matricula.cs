using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecreioFerias.Models
{
    [Table("Matricula")]
    public class Matricula
    {
        [Key()]
        public int MatriculaId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataAtual => DateTime.Now;

        [Required(ErrorMessage ="Selecione um Aluno!")]
        public int AlunoId { get; set; }
        [ValidateNever]
        public Aluno Aluno { get; set; }

        [Required(ErrorMessage ="Selecione a Turma")]
        public int TurmaId { get; set; }
        [ValidateNever]
        public Turma Turma { get; set; }

      
        
    }
}
