using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yamaanco.Domain.Entities.ProfileEntities;

namespace Yamaanco.Infrastructure.EF.Persistence.Configurations.ProfileConf
{
    public class ProfileMessageResourcesConfiguration : IEntityTypeConfiguration<ProfileMessageResources>
    {
        public void Configure(EntityTypeBuilder<ProfileMessageResources> builder)
        {
            builder.HasOne(x => x.Message)
           .WithOne(m => m.File)
           .HasForeignKey<ProfileMessageResources>(x => x.MessageId);
        }
    }
}