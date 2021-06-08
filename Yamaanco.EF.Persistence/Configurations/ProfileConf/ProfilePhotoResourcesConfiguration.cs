using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yamaanco.Domain.Entities.ProfileEntities;

namespace Yamaanco.Infrastructure.EF.Persistence.Configurations.ProfileConf
{
    public class ProfilePhotoResourcesConfiguration : IEntityTypeConfiguration<ProfilePhotoResources>
    {
        public void Configure(EntityTypeBuilder<ProfilePhotoResources> builder)
        {
            // builder.HasQueryFilter(c => !c.IsDeleted);
        }
    }
}