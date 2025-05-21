using CLinicaXPTO.Model;
using Microsoft.EntityFrameworkCore;

namespace ClinicaXPTO.DAL
{
    public class CLinicaXPTODBContext : DbContext
    {
        public CLinicaXPTODBContext(DbContextOptions<CLinicaXPTODBContext> options) : base(options) 
        { }
        public DbSet<Utente>Utentes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Utente>()
                .Property(u => u.Tipo)
                .HasConversion<string>();

        }

    }
}
