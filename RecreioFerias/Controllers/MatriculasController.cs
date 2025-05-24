using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using RecreioFerias.Data;
using RecreioFerias.Models;
using X.PagedList.Extensions;
using static RecreioFerias.Controllers.HomeController;

namespace RecreioFerias.Controllers
{
    [ServiceFilter(typeof(RecreioFerias.Filters.AutenticacaoFilter))]
    public class MatriculasController : Controller
    {
        private readonly AppDbContext _context;

        public MatriculasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Matriculas
        public async Task<IActionResult> Index(string pesquisa, string filtro, int? page)
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
            var consulta = from s in _context.Matricula
                           .Include(m => m.Aluno)
                           .Include(m => m.Turma)
                           select s;

            if (!string.IsNullOrWhiteSpace(pesquisa))
            {
                int id = 0;
                int.TryParse(pesquisa, out id);
                consulta = consulta.Where(t => t.MatriculaId == id || t.Aluno.Nome.Contains(pesquisa) || t.Turma.Descricao.Contains(pesquisa));
               

            }
            consulta = consulta.OrderByDescending(s => s.MatriculaId);
            int pageSize = 4;
            int pageNumber = (page ?? 1);
           
            return View(consulta.ToPagedList(pageNumber, pageSize));
        }

        // GET: Matriculas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matricula = await _context.Matricula
                .Include(m => m.Aluno)
                .Include(m => m.Turma)
                .FirstOrDefaultAsync(m => m.MatriculaId == id);
            if (matricula == null)
            {
                return NotFound();
            }

            return View(matricula);
        }

  

     

        // GET: Matriculas/Create
        public ActionResult Create(string id)
        {
           
           

            int idAluno = 0;
            int.TryParse(id, out idAluno);


            var consulta = _context.Turma
                
                .FirstOrDefault();


            var consulta2 = _context.Aluno
                .Where(t => t.AlunoId == idAluno)
                .FirstOrDefault(t => t.AlunoId == idAluno);

            Matricula matricula = new Matricula();
            matricula.Aluno = consulta2;
            matricula.Turma = consulta;
            matricula.AlunoId = idAluno;

            ViewBag.FaixaEtaria = matricula.Turma?.FaixaEtaria;
            ViewData["TurmaDescricao"] = new SelectList(_context.Turma, "TurmaId", "Descricao");
            ViewBag.Vagas = VagasDisponiveis(consulta);

            // Recupera a mensagem de erro do TempData, se existir
            if (TempData["MensagemErro"] != null)
            {
                ViewBag.MensagemErro = TempData["MensagemErro"];
            }


            return View(matricula);
        }

        // POST: Matriculas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MatriculaId,AlunoId, TurmaId")] Matricula matricula)
        {
            if(!GetAlunoMatriculado(matricula.AlunoId,matricula.TurmaId))
            {
                TempData["MensagemErro"] = "Aluno já está matriculado.";
                return RedirectToAction("Create", new { id = matricula.AlunoId });
            }

            if (!VerificarQuantidadeAlunos(matricula.TurmaId))
            {
                TempData["MensagemErro"] = "A turma está cheia. Não é possível adicionar mais alunos.";
                return RedirectToAction("Create", new { id = matricula.AlunoId });
            }

            if(!VerificarIdadeAlunoMatricula(matricula.AlunoId, matricula.TurmaId))
            {
                TempData["MensagemErro"] = "O Aluno não atende os requisitos de idade para a turma selecionada.";
                return RedirectToAction("Create", new { id = matricula.AlunoId });
            }


            if (ModelState.IsValid)
                {
                    _context.Add(matricula);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }


            ViewData["TurmaDescricao"] = new SelectList(_context.Turma, "TurmaId", "Descricao");
            return View(matricula);
        }

        // GET: Matriculas/Edit/5
        public async Task<IActionResult> Edit(int? MatriculaId, int? TurmaId, int? AlunoId)
        {
            if (MatriculaId == null && TurmaId == null)
            {
                return NotFound();
            }
            var matricula = _context.Matricula
           .Include(m => m.Turma)
           .FirstOrDefault(m => m.MatriculaId == MatriculaId);

            if (matricula == null)
            {
                return NotFound();
            }
            var consulta = _context.Turma
                            .Where(s => s.TurmaId == TurmaId);
            Turma t = _context.Turma.FirstOrDefault(s => s.TurmaId == TurmaId);

            //var matricula = await _context.Matricula.FindAsync(MatriculaId);
            if (matricula == null && consulta == null)
            {
                return NotFound();
            }
            if (t == null)
            {
                TempData["MensagemErro"] = "A turma selecionada não foi encontrada.";
                ViewBag.Vagas = 0; // Or handle appropriately
            }
            else
            {
                ViewBag.Vagas = VagasDisponiveis(t);
            }

            ViewBag.FaixaEtaria = matricula.Turma?.FaixaEtaria; 
            //ViewBag.Vagas = VagasDisponiveis(t);
            //ViewBag.FaixaEtaria = GetFaixaEtaria(t.TurmaId);
            ViewData["TurmaDescricao"] = new SelectList(_context.Turma, "TurmaId", "Descricao");

            var consulta2 = _context.Aluno
                .Where(t => t.AlunoId == AlunoId)
                .FirstOrDefault(t => t.AlunoId == AlunoId);

            Matricula ma = new Matricula();
            ma.Aluno = consulta2;
            ma.Turma = t;
            ma.AlunoId = (int)AlunoId;

            // Recupera a mensagem de erro do TempData, se existir
            if (TempData["MensagemErro"] != null)
            {
                ViewBag.MensagemErro = TempData["MensagemErro"];
            }
            return View(matricula);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int MatriculaId, [Bind("MatriculaId,AlunoId,TurmaId")] Matricula matricula)
        {
            if (MatriculaId != matricula.MatriculaId)
            {
                return NotFound();
            }

          

            if (!VerificarQuantidadeAlunos(matricula.TurmaId))
            {
                TempData["MensagemErro"] = "A turma está cheia. Não é possível adicionar mais alunos.";
                return RedirectToAction("Edit", new { MatriculaId = matricula.MatriculaId, TurmaId = matricula.TurmaId, AlunoId = matricula.AlunoId });

            }

            if (!VerificarIdadeAlunoMatricula(matricula.AlunoId, matricula.TurmaId))
            {
                TempData["MensagemErro"] = "O Aluno não atende os requisitos de idade para a turma selecionada.";
                return RedirectToAction("Edit", new { MatriculaId = matricula.MatriculaId, TurmaId = matricula.TurmaId, AlunoId = matricula.AlunoId });

            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(matricula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatriculaExists(matricula.MatriculaId))
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
            ViewData["AlunoId"] = new SelectList(_context.Aluno, "AlunoId", "AgrupamentoTurmaAno", matricula.AlunoId);
            ViewData["TurmaId"] = new SelectList(_context.Turma, "TurmaId", "TurmaId", matricula.TurmaId);
            return View(matricula);
        }

        // GET: Matriculas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matricula = await _context.Matricula
                .Include(m => m.Aluno)
                .Include(m => m.Turma)
                .FirstOrDefaultAsync(m => m.MatriculaId == id);
            if (matricula == null)
            {
                return NotFound();
            }

            return View(matricula);
        }

        // POST: Matriculas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var matricula = await _context.Matricula.FindAsync(id);
            if (matricula != null)
            {
                _context.Matricula.Remove(matricula);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatriculaExists(int id)
        {
            return _context.Matricula.Any(e => e.MatriculaId == id);
        }

        public bool VerificarIdadeAlunoMatricula(int Alunoid, int TurmaId)
        {
            var aluno = _context.Aluno.FirstOrDefault(e => e.AlunoId == Alunoid);
            if (aluno == null)
            {
                ModelState.AddModelError("", "O Aluno não foi localizado");
                return false;
            }
            var t = _context.Turma.FirstOrDefault(e => e.TurmaId == TurmaId);
            if (t == null)
            {
                ModelState.AddModelError("", "A Turma não foi localizada");
                return false;
            }
            int idadeAluno = aluno.CalcularIdade(aluno.DataNascimento);
            if (idadeAluno < t.IdadeMinima || idadeAluno > t.IdadeMaxima)
            {
                ModelState.AddModelError("", "O Aluno não atende os requisitos de idade para a turma selecionada");
                return false;
            }
            return true;
        }


        public bool VerificarQuantidadeAlunos(int turmaId)
        {
            
            var turma = _context.Turma.Any(e => e.TurmaId == turmaId);
            if (turma == null)
            {
                ModelState.AddModelError("", "A Turma não foi localizada");

                return false;

            }

           Turma t = _context.Turma.FirstOrDefault(e => e.TurmaId == turmaId);
            // Verificar a quantidade máxima de alunos permitida
            int quantidadeMaxima = t.QuantidadeAlunos;

            // Contar quantos alunos já estão matriculados na turma

            int quantidadeAlunos = _context.Matricula
                .Count(m => m.TurmaId == turmaId);

            
            ViewBag.Turma = turma;
            ViewBag.QuantidadeAlunos = quantidadeAlunos;

                      

            // Calcular as vagas disponíveis
            int vagasDisponiveis = quantidadeMaxima - quantidadeAlunos;

            // Garantir que não haja número negativo de vagas
            vagasDisponiveis = Math.Max(0, vagasDisponiveis);

            if (vagasDisponiveis > 0) 
            {
                ViewBag.Turma = turma;
                ViewBag.VagasDisponiveis = vagasDisponiveis;
                return true; 
            }


           

            return false;
        }
       private int VagasDisponiveis(Turma turma) 
        {
            
            int quantidadeMaxima = turma.QuantidadeAlunos;

            // Contar quantos alunos já estão matriculados na turma

            int quantidadeAlunos = _context.Matricula
                .Count(m => m.TurmaId == turma.TurmaId);

            // Calcular as vagas disponíveis
            int vagasDisponiveis = quantidadeMaxima - quantidadeAlunos;

            // Garantir que não haja número negativo de vagas
            vagasDisponiveis = Math.Max(0, vagasDisponiveis);

            //passando para a viewbag
           
            return vagasDisponiveis;
        }

        [HttpGet]
        public JsonResult GetVagasDisponiveis(int turmaId)
        {
            var turma = _context.Turma.FirstOrDefault(t => t.TurmaId == turmaId);
            if (turma == null)
            {
                return Json(new { sucesso = false, mensagem = "Turma não encontrada." });
            }

            int quantidadeAlunos = _context.Matricula.Count(m => m.TurmaId == turmaId);
            int vagasDisponiveis = Math.Max(0, turma.QuantidadeAlunos - quantidadeAlunos);

            return Json(new { sucesso = true, vagas = vagasDisponiveis });
        }


       
        public bool GetAlunoMatriculado(int AlunoId, int TurmaId)
        {
            var alunoMatriculado = _context.Matricula.FirstOrDefault(t => t.AlunoId == AlunoId && t.TurmaId == TurmaId);
            if (alunoMatriculado != null)
            {
                return false;
            }



            return true;
        }

        public async Task<IActionResult> PesquisarAluno(string pesquisa, string filtro, int? page)
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

            var alunosMatriculados = _context.Matricula.Select(m => m.AlunoId);

            if (!string.IsNullOrEmpty(pesquisa))
            {

                var alunos = await _context.Aluno
               .Where(a => a.Nome.Contains(pesquisa))
               .ToListAsync();

                if (alunos.Count == 0)
                {
                    ViewBag.MensagemErro = "Nenhum aluno encontrado com esse nome.";    
                }
                if (TempData["MensagemErro"] != null)
                {
                    ViewBag.MensagemErro = TempData["MensagemErro"];
                }
                int pag = 4;
                int pageNu = (page ?? 1);
                return View(alunos.ToPagedList(pageNu, pag));
            }

            //filtra alunos matriculados.
            var consulta = await _context.Aluno
            .Where(a => !alunosMatriculados.Contains(a.AlunoId))
            .OrderByDescending(a => a.AlunoId)
            .ToListAsync();

            int pageSize = 4;
            int pageNumber = (page ?? 1);


            return View(consulta.ToPagedList(pageNumber, pageSize));
           
           
        }

        [HttpGet]
        public IActionResult GetFaixaEtaria(int turmaId)
        {
            var turma = _context.Turma.FirstOrDefault(t => t.TurmaId == turmaId);
            if (turma != null)
            {
                return Json(new { sucesso = true, faixaEtaria = turma.FaixaEtaria });
            }
            return Json(new { sucesso = false, mensagem = "Turma não encontrada." });
        }

    }
}
