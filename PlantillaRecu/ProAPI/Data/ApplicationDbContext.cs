using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RestAPI.Models.Entity;

namespace RestAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EntidadN1N2>()
            .HasKey(n1n2 => new { n1n2.EntidadN1Id, n1n2.EntidadN2Id });

            // Configurar las relaciones
            modelBuilder.Entity<EntidadN1N2>()
                .HasOne(ec => ec.EN1)
                .WithMany(e => e.EntidadesN2)
                .HasForeignKey(ec => ec.EntidadN1Id);

            modelBuilder.Entity<EntidadN1N2>()
                .HasOne(ec => ec.EN2)
                .WithMany(c => c.EntidadesN1)
                .HasForeignKey(ec => ec.EntidadN2Id);
        }
        //Add models here
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<ReservaEntity> Reservas { get; set; }
        public DbSet<FranjaHorariaEntity> FranjasHorarias { get; set; }
        public DbSet<DiaEntity> DiasNoLectivos { get; set; }
        public DbSet<UsuarioEntity> Usuarios { get; set; }
        public DbSet<EntidadN1Entity> EntidadN1 { get; set; }
        public DbSet<EntidadN2Entity> EntidadN2 { get; set; }
        public DbSet<EntidadN1N2> EntidadN1N2s { get; set; }



    }
}