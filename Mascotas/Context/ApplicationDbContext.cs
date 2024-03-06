using Mascotas.Context.Configuration;
using Mascotas.Models;
using Microsoft.EntityFrameworkCore;

namespace Mascotas.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Mascota> Mascotas { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new MascotaEntityTypeConfiguration().Configure(modelBuilder.Entity<Mascota>());
        }
    }
}
