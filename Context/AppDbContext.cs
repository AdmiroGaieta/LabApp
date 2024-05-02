using Microsoft.EntityFrameworkCore;
using LabApp_.Models;

namespace LabApp_.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<School> Schools { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<School>().HasKey(s => s.Id);
            modelBuilder.Entity<School>().Property(s => s.Name).IsRequired();
            modelBuilder.Entity<School>().Property(s => s.Email).IsRequired();
            modelBuilder.Entity<School>().Property(s => s.NumberOfClassrooms).IsRequired();
            modelBuilder.Entity<School>().Property(s => s.Province).IsRequired();
        }

        public void EnsureSchoolsTableCreated()
        {
            // Verifica se a tabela Schools existe, se não existir, a cria
            if (!Database.GetPendingMigrations().Any())
            {
                if (!Database.GetAppliedMigrations().Any())
                {
                    // Aplique todas as migrações
                    Database.Migrate();
                }
                else
                {
                    // Se não houver migrações pendentes, mas ainda assim a tabela não existe
                    // Cria manualmente a tabela Schools
                    Database.EnsureCreated();
                }
            }
        }
    }
}
