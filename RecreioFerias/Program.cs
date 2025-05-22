using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using RecreioFerias.Data;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);


// Adicione suporte para sessões
builder.Services.AddDistributedMemoryCache();

builder.Services.AddAntiforgery(options =>
{
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

builder.Services.AddSession(options =>
{
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tempo de expiração da sessão
//    options.Cookie.HttpOnly = true;
//    options.Cookie.IsEssential = true;
//});



builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Home/Login"; // Caminho para a página de login
        options.LogoutPath = "/Home/Logout"; // Caminho para logout
       // options.Cookie.HttpOnly = true; // Garante que o cookie só seja acessível via HTTP
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Garante que o cookie seja transmitido apenas via HTTPS
        options.Cookie.SameSite = SameSiteMode.Strict; // Protege contra ataques CSRF
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Tempo de expiração do cookie
    });





builder.Services.AddAuthorization();

// Add services to the container.
builder.Services.AddControllersWithViews();

//Passando para a classe AppDbContext a String de Conexao
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//registrando o filtro de autenticação
builder.Services.AddScoped<RecreioFerias.Filters.AutenticacaoFilter>();

var app = builder.Build();

// Use sessões
app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
