using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class OperadoresController : Controller
    {
        private readonly AppDbContext _context;

        public OperadoresController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Operadores
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
            var consulta = from s in _context.Operador select s;

            if (!string.IsNullOrWhiteSpace(pesquisa))
            {
                int id = 0;
                int.TryParse(pesquisa, out id);
                consulta = consulta.Where(t => t.Login.Contains(pesquisa) || t.Situacao.Equals(pesquisa));
                consulta = consulta.OrderBy(s => s.Login);

            }
            int pageSize = 4;
            int pageNumber = (page ?? 1);

            return View(consulta.ToPagedList(pageNumber, pageSize));
        }

        // GET: Operadores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operador = await _context.Operador
                .FirstOrDefaultAsync(o => o.OperadorId == id);

            if (operador == null)
            {
                return NotFound();
            }

            return View(operador);
        }


        // GET: Operadores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Operadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OperadorId,Login,Senha,Situacao")] Operador operador)
        {
            // Remover a validação de CompararSenha
            ModelState.Remove(nameof(Operador.CompararSenha));

            if (GetOperadorCadastrado(operador.Login))
            {


                if (ModelState.IsValid)
                {
                    _context.Add(operador);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                ModelState.AddModelError("", "O Login já está cadastrado.");
            }
            return View(operador);
        }

        // GET: Operadores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operador = await _context.Operador.FindAsync(id);
            if (operador == null)
            {
                return NotFound();
            }
            return View(operador);
        }

        // POST: Operadores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Operador operador)
        {
            if (id != operador.OperadorId)
            {
                return NotFound();
            }

            var operadorExistente = await _context.Operador.FindAsync(id);

            if (operadorExistente == null)
            {
                return NotFound();
            }

            // Se a senha não foi alterada, mantenha a senha existente
            if (string.IsNullOrWhiteSpace(operador.Senha))
            {
                operador.Senha = operadorExistente.Senha;
            }

            operadorExistente.Login = operador.Login;
            operadorExistente.Situacao = operador.Situacao;
            operadorExistente.Senha = operador.Senha;

            try
            {
                _context.Update(operadorExistente);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Operador.Any(e => e.OperadorId == id))
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


        // GET: Operadores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operador = await _context.Operador
                .FirstOrDefaultAsync(m => m.OperadorId == id);
            if (operador == null)
            {
                return NotFound();
            }

            return View(operador);
        }

        // POST: Operadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var operador = await _context.Operador.FindAsync(id);
            if (operador != null)
            {
                _context.Operador.Remove(operador);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OperadorExists(int id)
        {
            return _context.Operador.Any(e => e.OperadorId == id);
        }

        public bool GetOperadorCadastrado(string Login)
        {
            var operadorCadastrado = _context.Operador.FirstOrDefault(t => t.Login.Contains(Login));
            if (operadorCadastrado != null)
            {
                return false;
            }

            return true;
        }
    }
}
