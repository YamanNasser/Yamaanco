using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yamaanco.Domain.Entities.ProfileEntities;

namespace Yamaanco.Infrastructure.EF.Persistence.Configurations.ProfileConf
{
    internal class ProfileViewerConfiguration : IEntityTypeConfiguration<ProfileViewer>
    {
        public void Configure(EntityTypeBuilder<ProfileViewer> builder)
        {
            builder.HasOne(x => x.Profile)
                .WithMany(m => m.Viewers)
                .HasForeignKey(x => x.ProfileId);
            builder.HasQueryFilter(c => !c.Profile.IsDeleted);
        }
    }
}