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
        }
        //Add models here
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<ReservaEntity> Reservas { get; set; }
        public DbSet<FranjaHorariaEntity> FranjasHorarias { get; set; }
        public DbSet<DiaEntity> DiasNoLectivos { get; set; }
        public DbSet<UsuarioEntity> Usuarios { get; set; }



    }
}