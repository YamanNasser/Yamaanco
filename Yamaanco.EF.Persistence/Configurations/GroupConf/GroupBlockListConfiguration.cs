using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yamaanco.Domain.Entities.GroupEntities;

namespace Yamaanco.Infrastructure.EF.Persistence.Configurations.GroupConf
{
    public class GroupBlockListConfiguration : IEntityTypeConfiguration<GroupBlockList>
    {
        public void Configure(EntityTypeBuilder<GroupBlockList> builder)
        {
            builder.HasOne(x => x.Group)
           .WithMany(m => m.BlockList)
           .HasForeignKey(x => x.GroupId);
            builder.HasQueryFilter(c => !c.BlockProfile.IsDeleted);
        }
    }
}