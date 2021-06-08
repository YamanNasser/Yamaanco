using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yamaanco.Domain.Entities.ProfileEntities;

namespace Yamaanco.Infrastructure.EF.Persistence.Configurations.ProfileConf
{
    public class ProfileNotificationConfiguration : IEntityTypeConfiguration<ProfileNotification>
    {
        public void Configure(EntityTypeBuilder<ProfileNotification> builder)
        {
            builder.HasOne(x => x.Profile)
           .WithMany(m => m.Notifications)
           .HasForeignKey(x => x.ProfileId);
            builder.HasQueryFilter(c => !c.Comment.IsDeleted);
        }
    }
}