using SmartGarage.Models;
using SmartGarage.Models.Enums;
using SmartGarage.Services;
using Microsoft.EntityFrameworkCore;

namespace SmartGarage.Data;

public class SmartGarageDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Mechanic> Mechanics { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }

    private readonly IHashingService hashingService;

    public SmartGarageDbContext(DbContextOptions<SmartGarageDbContext> options, IHashingService hashingServices) : base(options)
    {
        this.hashingService = hashingServices;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().HasData(
            new List<User>
            {
                new("Gosho", "Potrebitel", 4, 0888888111) { Id = 4, FirstName = "Gosho", LastName = "Potrebitel", Role = UserRole.User, Status = UserStatus.Active},
                new("Tisho", "Potrebitel", 5, 0888888222) { Id = 5, FirstName = "Tisho", LastName = "Potrebitel", Role = UserRole.User, Status = UserStatus.Active},
                new("Ivan", "Potrebitel", 6, 0888888333) { Id = 6, FirstName = "Ivan", LastName = "Potrebitel", Role = UserRole.User, Status = UserStatus.Active},
                new("Stoqn", "Potrebitel", 7, 0888888444) { Id = 7, FirstName = "Stoqn", LastName = "Potrebitel", Role = UserRole.User, Status = UserStatus.Active},

            }
        );

        modelBuilder.Entity<Mechanic>().HasData(
            new List<Mechanic>
            {
                new("Tosho", 0888888777, UserStatus.Active) { Id = 2, FirstName = "Tosho", LastName = "Mehanik", Status = UserStatus.Active},
                new("Pesho", 0888888555, UserStatus.Active) { Id = 3, FirstName = "Pesho", LastName = "Mehanik", Status = UserStatus.Active },
            }
        );

        modelBuilder.Entity<Vehicle>().HasData(
            new List<Vehicle>
            {
                new() {CarMake = "Suzuki", CarModel = "Grand Vitara", CarYear = 2014, CarVin = "JTS456789JKAWCEVE", CarLicencePlate = "CA8308PC", CarSystemId = 1000, Id = 4 },
                new() {CarMake = "Toyota", CarModel = "Land Cruiser", CarYear = 1994, CarVin = "JTS456789ASDFGHJK", CarLicencePlate = "CA2345PC", CarSystemId = 1001, Id = 5 },
                new() {CarMake = "Audi", CarModel = "Q7", CarYear = 2012, CarVin = "WVWZZZ7RZXCVBNMLA", CarLicencePlate = "CA8228PC", CarSystemId = 1002, Id = 6 },
                new() {CarMake = "Lancia", CarModel = "Delta Integrale", CarYear = 1991, CarVin = "PT56789JKAWCEVEQW", CarLicencePlate = "CA2032PC", CarSystemId = 1003, Id = 7 },            }
        );

        modelBuilder.Entity<Vehicle>()
            .HasOne(v => v.CarMake)
            .WithMany()
            .HasForeignKey(v => v.Id)
            .OnDelete(DeleteBehavior.NoAction);
    }
}