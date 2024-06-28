using Domain.Entities.Chats;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations.Chats
{
    public class ChatRoomConfiguration : IEntityTypeConfiguration<ChatRoom>
    {
        public void Configure(EntityTypeBuilder<ChatRoom> builder)
        {
            builder
                .ToTable("ChatRooms")
                .HasKey(c => c.Id);

            builder
                .Property(c => c.Name)
                .HasMaxLength(64)
                .IsRequired();

            builder
                .HasMany(cr => cr.ChatMessages)
                .WithOne(cm => cm.ChatRoom)
                .HasForeignKey(cm => cm.ChatRoomId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(cr => cr.Owner)
                .WithMany()
                .HasForeignKey(cr => cr.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
