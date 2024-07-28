using Microsoft.EntityFrameworkCore;

namespace Beltek.Project.Models
{
    public class OkulDbContext: DbContext
    {

        public DbSet<Ogrenci> Ogrenciler { get; set; }
        public DbSet<Sinif> Siniflar { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.;Database=OkulDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Sinif>().HasKey(o => o.SinifId);
            modelBuilder.Entity<Ogrenci>().HasKey(o => o.OgrenciId);
            modelBuilder.Entity<Ogrenci>().Property(o => o.Isim).HasColumnType("varchar").HasMaxLength(30).IsRequired();
            modelBuilder.Entity<Ogrenci>().Property(o => o.Soyisim).HasColumnType("varchar").HasMaxLength(15).IsRequired();
            modelBuilder.Entity<Ogrenci>().HasOne(s => s.Sinif).WithMany(c => c.Ogrenciler).HasForeignKey(o => o.SinifId);
            modelBuilder.Entity<Sinif>().Property(o => o.SinifAdi).HasColumnType("varchar").HasMaxLength(30).IsRequired();
        }

    }
}
