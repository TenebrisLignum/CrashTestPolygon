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
    public class ApplicationDbContext : IdentityDbContext<AppUser>
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

            var configurations = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => !t.IsAbstract && typeof(IEntityConfiguration).IsAssignableFrom(t))
                .Select(Activator.CreateInstance)
                .Cast<IEntityConfiguration>();

            foreach (var configuration in configurations)
            {
                configuration.Configure(modelBuilder);
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

            AddEntities(modelBuilder);
        }

        private void AddEntities(ModelBuilder modelBuilder)
        {
            var entityTypes = Assembly.GetAssembly(typeof(Entity))
                .GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(Entity)));

            foreach (var type in entityTypes)
            {
                modelBuilder.Entity(type);
            }
        }
    }
}
