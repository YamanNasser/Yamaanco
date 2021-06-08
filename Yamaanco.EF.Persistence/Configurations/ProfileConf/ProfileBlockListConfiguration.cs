using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yamaanco.Domain.Entities.ProfileEntities;

namespace Yamaanco.Infrastructure.EF.Persistence.Configurations.ProfileConf
{
    public class ProfileBlockListConfiguration : IEntityTypeConfiguration<ProfileBlockList>
    {
        public void Configure(EntityTypeBuilder<ProfileBlockList> builder)
        {
            builder.HasOne(x => x.Profile)
           .WithMany(m => m.BlockList)
           .HasForeignKey(x => x.ProfileId);
        }
    }
}