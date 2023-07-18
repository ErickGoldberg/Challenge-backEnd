using Challenge_backEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace Challenge_backEnd.DataBase
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Defina as DbSet para suas entidades (modelos)
        public DbSet<Receita> Receitas { get; set; }
        public DbSet<Despesa> Despesas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure as configurações de mapeamento das entidades, se necessário
             modelBuilder.Entity<Receita>().ToTable("Receitas");
             modelBuilder.Entity<Despesa>().ToTable("Despesas");
      
        }
    }
}
