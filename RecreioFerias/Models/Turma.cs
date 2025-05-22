using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecreioFerias.Models
{
    [Table("Turma")]
    public class Turma
    {
        [Key()]
        public int TurmaId { get; set; }

        [Required(ErrorMessage ="Preencha a Cor da Turma!")]
        [Display(Name ="Cor da Turma: ")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Preencha a Quantidade Máxima de Alunos!")]
        [Display(Name = "Quantidade de Alunos:")]
        
        public int QuantidadeAlunos { get; set; }
        
        [Required(ErrorMessage ="Preencha a Idade Máxima!")]
        [Display(Name ="Idade Máxima:")]
        public int IdadeMaxima { get; set; }

        [Required(ErrorMessage ="Prencha a Idade Mínima!")]
        [Display(Name ="Idade Mínima:")]
        public int IdadeMinima { get; set; }

        // FaixaEtaria pega a IdadeMinima e IdadeMaxima e retorna uma string formatada
        public string FaixaEtaria => $"{IdadeMinima} - {IdadeMaxima}";
              
        
    }
}
