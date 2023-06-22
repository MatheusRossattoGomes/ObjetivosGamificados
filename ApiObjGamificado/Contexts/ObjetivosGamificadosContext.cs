using Domain;
using Maps;
using Microsoft.EntityFrameworkCore;
namespace Contexts
{
    public class ObjetivosGamificadosContext : DbContext
    {
        public ObjetivosGamificadosContext(DbContextOptions<ObjetivosGamificadosContext> options)
            : base(options)
        {
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Objetivos> Objetivos { get; set; }
        public DbSet<Aula> Aulas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new UsuarioMap());
            modelBuilder
                .ApplyConfiguration(new ObjetivosMap());
            modelBuilder
                .ApplyConfiguration(new AulaMap());
        }
    }
}