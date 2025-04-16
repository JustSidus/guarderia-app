using Microsoft.EntityFrameworkCore;
using GuarderiaApp.Models;

namespace GuarderiaApp.Data
{
    public class GuarderiaDbContext : DbContext
    {
        public GuarderiaDbContext(DbContextOptions<GuarderiaDbContext> options) : base(options) { }

        public DbSet<Niño> Niños { get; set; }
        public DbSet<PersonaAutorizada> PersonasAutorizadas { get; set; }
        public DbSet<MenuConsumido> MenusConsumidos { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Plato> Platos { get; set; }
        public DbSet<Ingrediente> Ingredientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relación entre Niño y PersonasAutorizadas
            modelBuilder.Entity<PersonaAutorizada>()
                .HasOne(pa => pa.Niño)
                .WithMany(n => n.PersonasAutorizadas)
                .HasForeignKey(pa => pa.NiñoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación entre MenúConsumido y Niño
            modelBuilder.Entity<MenuConsumido>()
                .HasOne(mc => mc.Niño)
                .WithMany(n => n.MenusConsumidos)
                .HasForeignKey(mc => mc.NiñoId);

            // Relación entre MenúConsumido y Menú
            modelBuilder.Entity<MenuConsumido>()
                .HasOne(mc => mc.Menu)
                .WithMany()
                .HasForeignKey(mc => mc.MenuId);
        }
    }
}
