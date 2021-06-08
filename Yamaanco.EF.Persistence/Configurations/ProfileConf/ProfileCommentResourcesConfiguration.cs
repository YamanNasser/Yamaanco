using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yamaanco.Domain.Entities.ProfileEntities;

namespace Yamaanco.Infrastructure.EF.Persistence.Configurations.ProfileConf
{
    public class ProfileCommentResourcesConfiguration : IEntityTypeConfiguration<ProfileCommentResources>
    {
        public void Configure(EntityTypeBuilder<ProfileCommentResources> builder)
        {
            // builder.HasQueryFilter(c => !c.IsDeleted);
        }
    }
}