using Microsoft.EntityFrameworkCore;
using RezervasyonApp.Models;

namespace RezervasyonApp.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Mekan>()
                .HasOne(m => m.Kullanici)
                .WithMany()
                .HasForeignKey(m => m.KullaniciId)
                .OnDelete(DeleteBehavior.Restrict); // 🔥 Bu satır kritik
        }

        public DbSet<Mekan> Mekanlar { get; set; }

        public DbSet<Kullanici> Kullanicilar { get; set; }

        public DbSet<Rezervasyon> Rezervasyonlar { get; set; }

    }
}
