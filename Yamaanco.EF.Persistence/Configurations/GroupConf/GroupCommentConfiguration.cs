using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yamaanco.Domain.Entities.GroupEntities;

namespace Yamaanco.Infrastructure.EF.Persistence.Configurations.GroupConf
{
    public class GroupCommentConfiguration : IEntityTypeConfiguration<GroupComment>
    {
        public void Configure(EntityTypeBuilder<GroupComment> builder)
        {
            builder.HasQueryFilter(c => !c.IsDeleted);
        }
    }
}