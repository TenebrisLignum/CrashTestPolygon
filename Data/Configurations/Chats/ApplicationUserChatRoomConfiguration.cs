using Domain.Entities.Chats;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations.Chats
{
    public class UserChatRoomConfiguration : IEntityTypeConfiguration<ApplicationUserChatRoom>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserChatRoom> builder)
        {
            builder.HasKey(uc => new { uc.ApplicationUserId, uc.ChatRoomId });

            builder.HasOne(uc => uc.ApplicationUser)
                .WithMany()
                .HasForeignKey(uc => uc.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(uc => uc.ChatRoom)
                .WithMany(cr => cr.UserChatRooms)
                .HasForeignKey(uc => uc.ChatRoomId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
