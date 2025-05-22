using System.Diagnostics;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using RecreioFerias.Data;
using RecreioFerias.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace RecreioFerias.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        [ServiceFilter(typeof(RecreioFerias.Filters.AutenticacaoFilter))]
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
           
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Operador operador)
        {
            // Remover a validação de CompararSenha
            ModelState.Remove(nameof(Operador.CompararSenha));

            if (ModelState.IsValid)
            {
                // Verificar se o usuário está ativo
                var consulta = await _context.Operador
                    .FirstOrDefaultAsync(o => o.Login.Contains(operador.Login) && o.Senha.Contains(operador.Senha));

                if (consulta != null && consulta.Situacao)
                {
                    // Autenticação bem-sucedida
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, operador.Login)
            };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    HttpContext.Session.SetString("UsuarioLogado", operador.Login);
                    return RedirectToAction("Index", "Home");
                }
                else if (consulta != null && !consulta.Situacao)
                {
                    // Usuário inativo
                    HttpContext.Session.Clear();
                    Response.Cookies.Delete(".AspNetCore.Session");
                    ModelState.AddModelError("", "Usuário inativo. Entre em contato com o administrador.");
                }
                else
                {
                    // Credenciais inválidas
                    ModelState.AddModelError("", "Login ou senha inválidos.");
                }
            }

            return View(operador);
        }




        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            Console.WriteLine("Logout chamado");
            HttpContext.Session.Clear();

            Response.Cookies.Append(".AspNetCore.Session", "", new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(-1)
            });

            // Remove o cookie de autenticação
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Redireciona para a página de login
            return RedirectToAction("Login", "Home");
        }


    }
}
