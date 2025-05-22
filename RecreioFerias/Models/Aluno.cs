using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecreioFerias.Models
{
    [Table("Aluno")]
    public class Aluno
    {
        [Key()]
        public int AlunoId { get; set; }

        [Display(Name = "Nome do Aluno:")]
        [Required(ErrorMessage = "Campo nome deve ser preenchido.")]
        [MaxLength(100, ErrorMessage = "Campo nome deve ter no maxímo de 255 caracteres.")]
        [MinLength(3, ErrorMessage = "Campo nome deve ter no mínimo de 3 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Display(Name = "Nome Social:")]
        [MaxLength(100, ErrorMessage = "Campo nome deve ter no maxímo de 255 caracteres.")]
        [MinLength(3, ErrorMessage = "Campo nome deve ter no mínimo de 3 caracteres.")]
        public string? NomeSocial { get; set; }

        [Display(Name = "RG:")]
        [MaxLength(12, ErrorMessage = "Campo deve ter no maxímo de 12 caracteres.")]
        [MinLength(9, ErrorMessage = "Campo deve ter no mínimo de 9 caracteres.")]
        public string? RG { get; set; }

        [Display(Name = "Data de Expedição RG:")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateOnly? DataExpedicaoRG { get; set; }

        [Display(Name = "CPF:")]
        [MaxLength(14, ErrorMessage = "Campo deve ter no maxímo de 14 caracteres.")]
        [MinLength(14, ErrorMessage = "Campo deve ter no mínimo de 14 caracteres.")]
        public string? CPF { get; set; }

        [Display(Name = "Certidão de Nascimento:")]
        [MaxLength(40, ErrorMessage = "Campo deve ter no maxímo de 40 caracteres.")]
        [MinLength(32, ErrorMessage = "Campo deve ter no mínimo de 32 caracteres.")]
        public string? CertidaoNascimento { get; set; } = string.Empty;

       
        [Display(Name = "Data de Nascimento:")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]   
        public DateOnly DataNascimento { get; set; }

        [Display(Name = "Sexo:")]
        [Required(ErrorMessage = "Selecione o Sexo.")]
        [MaxLength(20, ErrorMessage = "Campo Genero deve ter no maxímo de 20 caracteres.")]
        public string? Sexo { get; set; }


        [Display(Name = "Responsavel 1:")]
        [Required(ErrorMessage = "Campo nome deve ser preenchido.")]
        public string NomeResponsavel1 { get; set; } = string.Empty;

        [Display(Name = "Social :")]
        public string? NomeResponsavelSocial1 { get; set; }

        [Display(Name = "Telefone Responsavel 1:")]
        [MaxLength(15, ErrorMessage = "Campo Telefone Celular deve ter no maxímo de 14 caracteres.")]
        [MinLength(14, ErrorMessage = "Campo Telefone Celular deve ter no mínimo de 12 caracteres.")]
        [RegularExpression(@"\(\d{2}\) \d{5}-\d{4}", ErrorMessage = "Formato de telefone inválido.")]
        public string TelefoneResponsavel1 { get; set; } = string.Empty;


        [Display(Name = "Outro:")]
        [MaxLength(15, ErrorMessage = "Campo Telefone Celular deve ter no maxímo de 14 caracteres.")]
        [MinLength(14, ErrorMessage = "Campo Telefone Celular deve ter no mínimo de 12 caracteres.")]
        [RegularExpression(@"\(\d{2}\) \d{5}-\d{4}", ErrorMessage = "Formato de telefone inválido.")]
        public string? TelefoneResponsavel1Outro { get; set; }

        [Display(Name = "RG / CPF:")]
        [MaxLength(14, ErrorMessage = "Campo deve ter no maxímo de 14 caracteres.")]
        public string? DocResponsavel1 { get; set; }

        [Display(Name = "Responsavel 2:")]
        public string? NomeResponsavel2 { get; set; }

        [Display(Name = "Telefone Responsavel 2:")]
        [MaxLength(15, ErrorMessage = "Campo Telefone Celular deve ter no maxímo de 14 caracteres.")]
        [MinLength(14, ErrorMessage = "Campo Telefone Celular deve ter no mínimo de 12 caracteres.")]
        [RegularExpression(@"\(\d{2}\) \d{5}-\d{4}", ErrorMessage = "Formato de telefone inválido.")]
        public string? TelefoneResponsavel2 { get; set; }


        [Display(Name = "RG / CPF:")]
        [MaxLength(14, ErrorMessage = "Campo deve ter no maxímo de 14 caracteres.")]
        public string? DocResponsavel2 { get; set; }


        [Display(Name = "Responsavel 3:")]
        public string? NomeResponsavel3 { get; set; }

        [Display(Name = "Telefone Responsavel 3:")]
        [MaxLength(15, ErrorMessage = "Campo Telefone Celular deve ter no maxímo de 14 caracteres.")]
        [MinLength(14, ErrorMessage = "Campo Telefone Celular deve ter no mínimo de 12 caracteres.")]
        [RegularExpression(@"\(\d{2}\) \d{5}-\d{4}", ErrorMessage = "Formato de telefone inválido.")]
        public string? TelefoneResponsavel3 { get; set; }


        [Display(Name = "RG / CPF:")]
        [MaxLength(14, ErrorMessage = "Campo deve ter no maxímo de 14 caracteres.")]
        public string? DocResponsavel3 { get; set; }


        [Display(Name = "Responsavel 4:")]
        public string? NomeResponsavel4 { get; set; }

        [Display(Name = "Telefone Responsavel 4:")]
        [MaxLength(15, ErrorMessage = "Campo Telefone Celular deve ter no maxímo de 14 caracteres.")]
        [MinLength(14, ErrorMessage = "Campo Telefone Celular deve ter no mínimo de 12 caracteres.")]
        [RegularExpression(@"\(\d{2}\) \d{5}-\d{4}", ErrorMessage = "Formato de telefone inválido.")]
        public string? TelefoneResponsavel4 { get; set; }


        [Display(Name = "RG / CPF:")]
        [MaxLength(14, ErrorMessage = "Campo deve ter no maxímo de 14 caracteres.")]
        public string? DocResponsavel4 { get; set; }



        [Display(Name = "CEP:")]
        [MaxLength(8, ErrorMessage = "Campo CEP deve ter no máximo 8 caracteres.")]
        [Required(ErrorMessage ="Preencha o CEP!")]
        public string CEP { get; set; } = string.Empty;

        [Display(Name = "Endereço:")]
        public string Logradouro { get; set; } = string.Empty;
       
        [Display(Name = "Numero:")] 
        public string Numero { get; set; } = string.Empty;

        [Display(Name = "Complemento:")]
        public string? Complemento { get; set; }

        [Display(Name = "Bairro:")]
        public string Bairro { get; set; } = string.Empty;

        [Display(Name = "Cidade:")]
        public string Cidade { get; set; } = string.Empty;

        [Display(Name = "Estado:")]
        public string Estado { get; set; } = string.Empty;

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; } 

        [Display(Name = "Escola:")]
        [Required(ErrorMessage = "Preencha o Nome da Escola!")]
        public string Escola { get; set; } = string.Empty;

        [Required(ErrorMessage = "Selecione se o Aluno é da Rede Municipal ou Não Estudante da RME!")]
        public bool RedeMunicipal { get; set; }
        [Required(ErrorMessage ="Selecione a Turma do Aluno!")]
        public string AgrupamentoTurmaAno { get; set; } = string.Empty;

        [Display(Name = "Código EOL:")]
        public int? EOL { get; set; }

   
        [Required(ErrorMessage = "Selecione se o Aluno tem Deficiência")]
        public bool OpcaoDeficiencia { get; set; }

        [Required(ErrorMessage = "Selecione se o Aluno tem Algum Problema de Saúde")]
        public bool OpcaoProblemaSaude { get; set; }


  
        [Required(ErrorMessage = "Selecione se o Aluno tem Alguma Restrição Alimentar.")]
        public bool OpcaoRestricaoAlimentar { get; set; }

        
        [Required(ErrorMessage = "Selecione se o Aluno tem Alguma Restrição a algum Medicamento.")]
        public bool OpcaoRestricaoMedicamento { get; set; }

        [Required(ErrorMessage = "Selecione se o Aluno usa Medicamento Contínuo.")]
        public bool OpcaoMedicamento { get; set; }

        [Required(ErrorMessage = "Selecione se o Aluno Possui Convênio Médico")]
        public bool OpcaoConvenioMedico { get; set; }

        public string? Deficiencia { get; set; }

        public string? ProblemaSaude { get; set; }


        public string? RestricaoAlimentar { get; set; }


        public string? RestricaoMedicamento { get; set; }


        public string? Medicamento { get; set; }


        public string? ConvenioMedico { get; set; }


        [Required(ErrorMessage = "Selecione se o Aluno pode Usar a Piscina")]
        public bool Piscina { get; set; }

        
        [Required(ErrorMessage = "Selecione se o Aluno pode Voltar Sozinho")]
        public bool VoltaSozinho { get; set; }

        


        public static List<string> ListaSexo =>
                new List<string>
                {
                    "Masculino", "Feminino", "Outro","Prefiro Não Informar"
                };

        public int CalcularIdade(DateOnly dataNascimento)
        {
            DateTime hoje = DateTime.Today;
            TimeOnly horaPadrao = TimeOnly.MinValue; // Define a hora como 00:00:00

            // Conversão para DateTime
            DateTime dataCompleta = dataNascimento.ToDateTime(horaPadrao);


            int idade = hoje.Year - dataCompleta.Year;
            if (dataCompleta > hoje.AddYears(-idade))
            {
                idade--;
            }
            return idade;
        }

        public int IdadeCalculada => DateTime.Now.Year - DataNascimento.Year -
        (DateTime.Now.DayOfYear < DataNascimento.DayOfYear ? 1 : 0);




    }
}
