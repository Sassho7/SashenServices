using Microsoft.EntityFrameworkCore;
using SmartGarage.Data;

namespace SmartGarage.Data
{
    public class SGDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public SGDbContext(DbContextOptions<SGDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);


            modelBuilder.Entity<User>()
                .Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<User>()
                .Property(u => u.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            base.OnModelCreating(modelBuilder);
        }
    }
}
