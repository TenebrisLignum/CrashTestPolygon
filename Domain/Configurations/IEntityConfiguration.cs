using Microsoft.EntityFrameworkCore;

namespace Domain.Configurations
{
    public interface IEntityConfiguration
    {
        void Configure(ModelBuilder modelBuilder);
    }
}
