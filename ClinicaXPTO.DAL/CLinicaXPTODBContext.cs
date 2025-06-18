using CLinicaXPTO.Model;
using Microsoft.EntityFrameworkCore;

namespace ClinicaXPTO.DAL
{
    public class CLinicaXPTODBContext : DbContext
    {
        public CLinicaXPTODBContext(DbContextOptions<CLinicaXPTODBContext> options) : base(options) 
        { }
        public DbSet<Utente>Utentes { get; set; }
        public DbSet<PedidoMarcacao> Pedidos { get; set; } 

        public DbSet<ActoClinico> Actos { get; set; }

        public DbSet<Profissional> profissionais { get; set; }

        public DbSet<UtenteNaoRegistado>utentesNaoRegistados { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Utente>()
                .Property(u => u.Tipo)
                .HasConversion<string>();


            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PedidoMarcacao>()
                .Property(p => p.EstadoMarcacao)
                .HasConversion<string>();

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ActoClinico>()
                .Property(t => t._TipoConsulta)
                .HasConversion<string>();

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ActoClinico>()
                .Property(t => t._Subsistema)
                .HasConversion<string>();

        }

    }
}
