using Data.Configurations;
using Domain;
using Domain.Entities.Abstract;
using Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => !type.IsAbstract && type.GetInterfaces().Any(interfaceType =>
                    interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }

            modelBuilder.Entity<IdentityRole>().HasData([
                new()
                {
                    Id = "8f9d0355-1728-4c2e-9426-787a388c7d07",
                    Name = Consts.AdminRoleString,
                    NormalizedName = Consts.AdminRoleStringNormalized
                },
                new()
                {
                    Id = "a0d4ed01-4665-463e-9507-99bcc45b7672",
                    Name = Consts.UserRoleString,
                    NormalizedName = Consts.UserRoleStringNormalized
                } 
            ]);
        }
    }
}
