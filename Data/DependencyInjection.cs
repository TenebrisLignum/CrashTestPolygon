using Data.Repository;
using Data.Repository.Chats;
using Domain.Repositories.Articles;
using Domain.Repositories.Chats;
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
                options.UseNpgsql(@"Host=localhost;Port=5432;Database=crashtestpolygon;Username=postgres;Password=1706;",
                    b => b.MigrationsAssembly("Data"));
            });

            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<IChatRoomsRepository, ChatRoomsRepository>();
            services.AddScoped<IChatMessagesRepository, ChatMessagesRepository>();
            services.AddScoped<IChatRoomApplicationUserRepository, ChatRoomApplicationUserRepository>();

            return services;
        }
    }
}
