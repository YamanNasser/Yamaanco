using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yamaanco.Domain.Entities.ProfileEntities;

namespace Yamaanco.Infrastructure.EF.Persistence.Configurations.ProfileConf
{
    public class ProfileFriendConfiguration : IEntityTypeConfiguration<ProfileFriend>
    {
        public void Configure(EntityTypeBuilder<ProfileFriend> builder)
        {
            builder.HasOne(x => x.Profile)
             .WithMany(m => m.Friends)
             .HasForeignKey(x => x.ProfileId);
            builder.HasQueryFilter(c => !c.Profile.IsDeleted);
        }
    }
}