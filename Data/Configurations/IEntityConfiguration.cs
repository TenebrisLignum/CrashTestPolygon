using Microsoft.EntityFrameworkCore;

namespace Data.Configurations
{
    public interface IEntityConfiguration
    {
        void Configure(ModelBuilder modelBuilder);
    }
}
