using Microsoft.EntityFrameworkCore;
using sistemaAdministracion.Models;

namespace sistemaAdministracion.Data
{
    public class sistemaAdministracionContext : DbContext
    {
        public sistemaAdministracionContext(DbContextOptions<sistemaAdministracionContext> options)
            : base(options)
        {
        }

        // DbSets para cada entidad principal
        public DbSet<Eleccion> Elecciones { get; set; }
        public DbSet<Seccion> Secciones { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<CargoSeccion> CargoSecciones { get; set; }
        public DbSet<SeccionEleccion> SeccionElecciones { get; set; }
        public DbSet<Recinto> Recintos { get; set; }
        public DbSet<MesaElectoral> MesasElectorales { get; set; }
        public DbSet<Jurado> Jurados { get; set; }
        public DbSet<Votante> Votantes { get; set; }
        public DbSet<Partido> Partidos { get; set; }
        public DbSet<Candidatura> Candidaturas { get; set; }
        public DbSet<Papeleta> Papeletas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Claves compuestas para tablas intermedias
            modelBuilder.Entity<CargoSeccion>()
                .HasKey(cs => new { cs.CargoId, cs.SeccionId });

            modelBuilder.Entity<SeccionEleccion>()
                .HasKey(se => new { se.EleccionId, se.SeccionId });

            // Relación Papeleta - Candidatura (muchos a muchos si aplica)
            modelBuilder.Entity<Papeleta>()
                .HasMany(p => p.Candidatos)
                .WithMany(); // Si necesitas personalizar esta relación, se puede extender aquí
        }
    }
}
