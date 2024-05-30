using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddData(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(@"Server=DESKTOP-9F5QCG9\SQLEXPRESS; Database=CrashTestPolygon; Persist Security Info=false; MultipleActiveResultSets=True; Trusted_Connection=True; TrustServerCertificate=True;",
                    b => b.MigrationsAssembly("Data"));
            });

            return services;
        }
    }
}
