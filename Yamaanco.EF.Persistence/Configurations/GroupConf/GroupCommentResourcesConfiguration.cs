using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yamaanco.Domain.Entities.GroupEntities;

namespace Yamaanco.Infrastructure.EF.Persistence.Configurations.GroupConf
{
    public class GroupCommentResourcesConfiguration : IEntityTypeConfiguration<GroupCommentResources>
    {
        public void Configure(EntityTypeBuilder<GroupCommentResources> builder)
        {
            //  builder.HasQueryFilter(c => !c.IsDeleted);
        }
    }
}