using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SmartGarage.Models;
using System.Numerics;

namespace SmartGarage.Data
{
    public class SGDbContext : DbContext
    {
        public SGDbContext(DbContextOptions<SGDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Service> Services { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(e =>
            {
                e.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(20);

                e.Property(u => u.PasswordHash)
                .IsRequired();

                e.Property(u => u.Email)
                .IsRequired();

                e.Property(u => u.PhoneNumber)
                .IsRequired()
                .HasMaxLength(10);
            });

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.PhoneNumber)
                .IsUnique();

            modelBuilder.Entity<Vehicle>(e =>
            {
                e.Property(v => v.CarLicencePlate)
                .IsRequired()
                .HasMaxLength(50)
                .HasAnnotation("MinLength", 2);

                e.Property(v => v.CarVin)
                .IsRequired()
                .HasMaxLength(50)
                .HasAnnotation("MinLength", 2);

                e.Property(v => v.CarYear)
                .IsRequired()
                .HasMaxLength(50)
                .HasAnnotation("MinLength", 2);
            });
            
            modelBuilder.Entity<Service>(e =>
            {
                e.Property(s => s.Name)
                .IsRequired();

                e.Property(p => p.Price)
                .IsRequired();
            });

        }
    }
}
