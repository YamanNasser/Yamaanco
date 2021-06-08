using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yamaanco.Domain.Entities.GroupEntities;

namespace Yamaanco.Infrastructure.EF.Persistence.Configurations.GroupConf
{
    public class GroupPhotoResourcesConfiguration : IEntityTypeConfiguration<GroupPhotoResources>
    {
        public void Configure(EntityTypeBuilder<GroupPhotoResources> builder)
        {
            //  builder.HasQueryFilter(c => !c.IsDeleted);
        }
    }
}