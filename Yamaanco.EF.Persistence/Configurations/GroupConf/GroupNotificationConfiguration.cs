using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yamaanco.Domain.Entities.GroupEntities;

namespace Yamaanco.Infrastructure.EF.Persistence.Configurations.GroupConf
{
    public class GroupNotificationConfiguration : IEntityTypeConfiguration<GroupNotification>
    {
        public void Configure(EntityTypeBuilder<GroupNotification> builder)
        {
            builder.HasOne(x => x.Group)
           .WithMany(m => m.Notifications)
           .HasForeignKey(x => x.GroupId);
            builder.HasQueryFilter(c => !c.Comment.IsDeleted);
        }
    }
}