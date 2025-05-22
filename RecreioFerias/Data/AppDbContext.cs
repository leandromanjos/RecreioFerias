using Microsoft.EntityFrameworkCore;
using RecreioFerias.Models;

namespace RecreioFerias.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
        }
        
        public DbSet<Matricula> Matricula { get; set; }
        public DbSet<Frequencia> Frequencia { get; set; }
        public DbSet<Turma> Turma { get; set; }
       
        public DbSet<Aluno> Aluno { get; set; }
        public DbSet<Operador> Operador { get; set; }

       
    }
}
