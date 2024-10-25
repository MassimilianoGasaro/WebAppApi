using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAppApi.Entities;
using Microsoft.Extensions.Hosting;

namespace WebAppApi.Data
{
    public class DataContext(DbContextOptions options) : IdentityDbContext<User, Role, int, IdentityUserClaim<int>,
       UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>(options)
    {
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Typology> Typologies { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            modelBuilder.Entity<Role>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            modelBuilder.Entity<Typology>().HasData(
                    new Typology
                    {
                        Id = 1,
                        Description = "Generica",
                        Name = "Generica"
                    },
                    new Typology
                    {
                        Id = 2,
                        Description = "Svago",
                        Name = "Svago"
                    },
                    new Typology
                    {
                        Id = 3,
                        Description = "Stipendio",
                        Name = "Stipendio"
                    },
                    new Typology
                    {
                        Id = 4,
                        Description = "Investimento",
                        Name = "Investimento"
                    });

            modelBuilder.Entity<Currency>().HasData(
                    new Currency
                    {
                        Id = 1,
                        Title = "Euro"
                    },
                    new Currency
                    {
                        Id = 2,
                        Title = "Dollaro"
                    });
        }
    }
}
