using CalendarApp.Data.Map;
using CalendarApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CalendarApp.Data
{
    public class CalendarDbContext : DbContext
    {
        public CalendarDbContext(DbContextOptions<CalendarDbContext> options) : base(options) 
        { 
        }

        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<EventoModel> Eventos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new EventoMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
