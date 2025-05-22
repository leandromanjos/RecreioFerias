using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RecreioFerias.Migrations
{
    /// <inheritdoc />
    public partial class CriandoBanco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aluno",
                columns: table => new
                {
                    AlunoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NomeSocial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RG = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    DataExpedicaoRG = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CPF = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    CertidaoNascimento = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sexo = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    NomeResponsavel1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomeResponsavelSocial1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelefoneResponsavel1 = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    TelefoneResponsavel1Outro = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    DocResponsavel1 = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    NomeResponsavel2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelefoneResponsavel2 = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    DocResponsavel2 = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    NomeResponsavel3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelefoneResponsavel3 = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    DocResponsavel3 = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    NomeResponsavel4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelefoneResponsavel4 = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    DocResponsavel4 = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    CEP = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    Logradouro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Complemento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Escola = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RedeMunicipal = table.Column<bool>(type: "bit", nullable: false),
                    AgrupamentoTurmaAno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EOL = table.Column<int>(type: "int", nullable: false),
                    OpcaoDeficiencia = table.Column<bool>(type: "bit", nullable: false),
                    OpcaoProblemaSaude = table.Column<bool>(type: "bit", nullable: false),
                    OpcaoRestricaoAlimentar = table.Column<bool>(type: "bit", nullable: false),
                    OpcaoRestricaoMedicamento = table.Column<bool>(type: "bit", nullable: false),
                    OpcaoMedicamento = table.Column<bool>(type: "bit", nullable: false),
                    OpcaoConvenioMedico = table.Column<bool>(type: "bit", nullable: false),
                    Deficiencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProblemaSaude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RestricaoAlimentar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RestricaoMedicamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Medicamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConvenioMedico = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Piscina = table.Column<bool>(type: "bit", nullable: false),
                    VoltaSozinho = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aluno", x => x.AlunoId);
                });

            migrationBuilder.CreateTable(
                name: "CorTurma",
                columns: table => new
                {
                    CorTurmaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorTurma", x => x.CorTurmaId);
                });

            migrationBuilder.CreateTable(
                name: "Operador",
                columns: table => new
                {
                    OperadorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Situacao = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operador", x => x.OperadorId);
                });

            migrationBuilder.CreateTable(
                name: "Frequencia",
                columns: table => new
                {
                    FrequenciaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiaDaSemana = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Presente = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    AlunoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frequencia", x => x.FrequenciaID);
                    table.ForeignKey(
                        name: "FK_Frequencia_Aluno_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Aluno",
                        principalColumn: "AlunoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Turma",
                columns: table => new
                {
                    TurmaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuantidadeAlunos = table.Column<int>(type: "int", maxLength: 3, nullable: false),
                    IdadeMaxima = table.Column<int>(type: "int", nullable: false),
                    IdadeMinima = table.Column<int>(type: "int", nullable: false),
                    CorTurmaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turma", x => x.TurmaId);
                    table.ForeignKey(
                        name: "FK_Turma_CorTurma_CorTurmaId",
                        column: x => x.CorTurmaId,
                        principalTable: "CorTurma",
                        principalColumn: "CorTurmaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Matricula",
                columns: table => new
                {
                    MatriculaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlunoId = table.Column<int>(type: "int", nullable: false),
                    TurmaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matricula", x => x.MatriculaId);
                    table.ForeignKey(
                        name: "FK_Matricula_Aluno_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Aluno",
                        principalColumn: "AlunoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Matricula_Turma_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turma",
                        principalColumn: "TurmaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CorTurma",
                columns: new[] { "CorTurmaId", "Descricao" },
                values: new object[,]
                {
                    { 1, "Verde" },
                    { 2, "Azul" },
                    { 3, "Roxo" },
                    { 4, "Laranja" },
                    { 5, "Amarelo" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Frequencia_AlunoId",
                table: "Frequencia",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_Matricula_AlunoId",
                table: "Matricula",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_Matricula_TurmaId",
                table: "Matricula",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_Turma_CorTurmaId",
                table: "Turma",
                column: "CorTurmaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Frequencia");

            migrationBuilder.DropTable(
                name: "Matricula");

            migrationBuilder.DropTable(
                name: "Operador");

            migrationBuilder.DropTable(
                name: "Aluno");

            migrationBuilder.DropTable(
                name: "Turma");

            migrationBuilder.DropTable(
                name: "CorTurma");
        }
    }
}
