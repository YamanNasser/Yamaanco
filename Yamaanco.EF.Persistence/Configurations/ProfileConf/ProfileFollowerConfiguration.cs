using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yamaanco.Domain.Entities.ProfileEntities;

namespace Yamaanco.Infrastructure.EF.Persistence.Configurations.ProfileConf
{
    public class ProfileFollowerConfiguration : IEntityTypeConfiguration<ProfileFollower>
    {
        public void Configure(EntityTypeBuilder<ProfileFollower> builder)
        {
            builder.HasOne(x => x.Profile)
              .WithMany(m => m.Followers)
              .HasForeignKey(x => x.ProfileId);
            builder.HasQueryFilter(c => !c.Profile.IsDeleted);
        }
    }
}