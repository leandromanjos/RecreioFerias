using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecreioFerias.Data;
using RecreioFerias.Models;
using X.PagedList.Extensions;
using static RecreioFerias.Controllers.HomeController;

namespace RecreioFerias.Controllers
{
    [ServiceFilter(typeof(RecreioFerias.Filters.AutenticacaoFilter))]
    public class AlunosController : Controller
    {
        private readonly AppDbContext _context;

        public AlunosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Alunos
        public IActionResult Index(string pesquisa, string filtro, int? page)
        {

            if (pesquisa != null)
            {
                page = 1;
            }
            else
            {
                pesquisa = filtro;
            }


            ViewBag.filtro = pesquisa;
            var consulta = from s in _context.Aluno select s;

            var alunosMatriculados = _context.Matricula.Select(m => m.AlunoId).ToList();
            ViewBag.AlunosMatriculados = alunosMatriculados;

            if (!string.IsNullOrWhiteSpace(pesquisa))
            {
                int id = 0;
                int.TryParse(pesquisa, out id);
                consulta = consulta.Where(t => t.AlunoId == id || t.Nome.Contains(pesquisa));
                

            }

            consulta = consulta.OrderByDescending(s => s.AlunoId);
            int pageSize = 4;
            int pageNumber = (page ?? 1);


            return View(consulta.ToPagedList(pageNumber, pageSize));
        }

     //public async Task<IActionResult> VerificaAluno(string Nome, DateOnly DataNascimento)
     //   {
     //       // Verificar se o Aluno já está cadastrado
     //       var alunoExistente = await _context.Aluno
     //           .FirstOrDefaultAsync(a => a.Nome == Nome && a.DataNascimento == DataNascimento);

     //       if (alunoExistente != null)
     //       {
     //           ModelState.AddModelError("", "O aluno já está cadastrado.");
     //           return RedirectToAction("Create");
     //       }
     //       return RedirectToAction("Create");

     //   }

        // GET: Alunos/Create
        public IActionResult Create()
        {
            ViewBag.List = Aluno.ListaSexo;
            return View();
        }

        // POST: Alunos/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AlunoId,Nome,NomeSocial,RG,DataExpedicaoRG,CPF,CertidaoNascimento,DataNascimento,Sexo,NomeResponsavel1,NomeResponsavelSocial1,TelefoneResponsavel1,TelefoneResponsavel1Outro,DocResponsavel1,NomeResponsavel2,TelefoneResponsavel2,DocResponsavel2,NomeResponsavel3,TelefoneResponsavel3,DocResponsavel3,NomeResponsavel4,TelefoneResponsavel4,DocResponsavel4,CEP,Logradouro,Numero,Complemento,Bairro,Cidade,Estado,Email,Escola,RedeMunicipal,AgrupamentoTurmaAno,EOL,OpcaoDeficiencia,OpcaoProblemaSaude,OpcaoRestricaoAlimentar,OpcaoRestricaoMedicamento,OpcaoMedicamento,OpcaoConvenioMedico,Deficiencia,ProblemaSaude,RestricaoAlimentar,RestricaoMedicamento,Medicamento,ConvenioMedico,Piscina,VoltaSozinho")] Aluno aluno)
        {
            ViewBag.List = Aluno.ListaSexo;
            // Transformar o nome para maiúsculo
            aluno.Nome = aluno.Nome.ToUpper();

            if (GetAlunoCadastrado(aluno.Nome, aluno.DataNascimento))
            {
                if (ModelState.IsValid)
                {


                    _context.Add(aluno);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                ModelState.AddModelError("", "O aluno já está cadastrado.");
            }
            return View(aluno);
        }

        // GET: Alunos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.List = Aluno.ListaSexo;
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _context.Aluno.FindAsync(id);
        
           

            if (aluno == null)
            {
                return NotFound();
            }
            return View(aluno);
        }

        // POST: Alunos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AlunoId,Nome,NomeSocial,RG,DataExpedicaoRG,CPF,CertidaoNascimento,DataNascimento,Sexo,NomeResponsavel1,NomeResponsavelSocial1,TelefoneResponsavel1,TelefoneResponsavel1Outro,DocResponsavel1,NomeResponsavel2,TelefoneResponsavel2,DocResponsavel2,NomeResponsavel3,TelefoneResponsavel3,DocResponsavel3,NomeResponsavel4,TelefoneResponsavel4,DocResponsavel4,CEP,Logradouro,Numero,Complemento,Bairro,Cidade,Estado,Email,Escola,RedeMunicipal,AgrupamentoTurmaAno,EOL,OpcaoDeficiencia,OpcaoProblemaSaude,OpcaoRestricaoAlimentar,OpcaoRestricaoMedicamento,OpcaoMedicamento,OpcaoConvenioMedico,Deficiencia,ProblemaSaude,RestricaoAlimentar,RestricaoMedicamento,Medicamento,ConvenioMedico,Piscina,VoltaSozinho")] Aluno aluno)
        {
            ViewBag.List = Aluno.ListaSexo;
            if (id != aluno.AlunoId)
            {
                return NotFound();
            }
            // Transformar o nome para maiúsculo
            aluno.Nome = aluno.Nome.ToUpper();

            //if (GetAlunoCadastrado(aluno.Nome, aluno.DataNascimento))
            //{
            //    ModelState.AddModelError("", "O aluno já está cadastrado.");
            //}
            if (ModelState.IsValid)
            {
                try
                {
                    var trackedAluno = _context.Aluno.Local.FirstOrDefault(a => a.AlunoId == aluno.AlunoId);
                    if (trackedAluno != null)
                    {
                        _context.Entry(trackedAluno).CurrentValues.SetValues(aluno);
                    }
                    else
                    {
                        _context.Update(aluno);
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlunoExists(aluno.AlunoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(aluno);
        }

        // GET: Alunos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _context.Aluno
                .FirstOrDefaultAsync(m => m.AlunoId == id);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        // POST: Alunos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aluno = await _context.Aluno.FindAsync(id);
            if (aluno != null)
            {
                _context.Aluno.Remove(aluno);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlunoExists(int id)
        {
            return _context.Aluno.Any(e => e.AlunoId == id);
        }

        public bool GetAlunoCadastrado(string Nome, DateOnly DataNascimento)
        {
            var alunoCadastrado = _context.Aluno.FirstOrDefault(t => t.Nome.Contains(Nome) && t.DataNascimento == DataNascimento);
            if (alunoCadastrado != null)
            {
                return false;
            }



            return true;
        }
    }
}
