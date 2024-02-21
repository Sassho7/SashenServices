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
            modelBuilder.Entity<User>().HasData(
              new User
              {
                  Id = 1,
                  Username = "exampleuser1",
                  PasswordHash = "Test123!",
                  Email = "user1@example.com",
                  PhoneNumber = "1234567890",
                  JoinDate = DateTime.Now,
                  IsEmployee = false,
                  IsDeleted = false
              },
              new User
              {
                  Id = 2,
                  Username = "exampleuser2",
                  PasswordHash = "Test123!",
                  Email = "user2@example.com",
                  PhoneNumber = "9876543210",
                  JoinDate = DateTime.Now,
                  IsEmployee = false,
                  IsDeleted = false
              }
          // Add more users if needed
          );

            // Seed vehicles
            modelBuilder.Entity<Vehicle>().HasData(
                new Vehicle
                {
                    Id = 1,
                    CarMake = "Toyota",
                    CarModel = "Camry",
                    CarYear = 2018,
                    CarVin = "ABC123456DEF78901",
                    CarLicencePlate = "ABC123",
                    userId = 1,
                    IsDeleted = false
                },
                new Vehicle
                {
                    Id = 2,
                    CarMake = "Honda",
                    CarModel = "Accord",
                    CarYear = 2019,
                    CarVin = "DEF987654ABC12345",
                    CarLicencePlate = "XYZ789",
                    userId = 2,
                    IsDeleted = false
                }
                // Add more vehicles if needed
            );

            // Seed services
            modelBuilder.Entity<Service>().HasData(
                new Service
                {
                    ServiceId = 1,
                    Name = "Oil Change",
                    Price = 50.00,
                    UserId = 1,
                    isDeleted = false
                },
                new Service
                {
                    ServiceId = 2,
                    Name = "Tire Rotation",
                    Price = 30.00,
                    UserId = 2,
                    isDeleted = false
                }
                // Add more services if needed
            );
        }
    }
}
