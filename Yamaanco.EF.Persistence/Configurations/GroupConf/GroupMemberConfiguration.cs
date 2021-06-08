using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yamaanco.Domain.Entities.GroupEntities;

namespace Yamaanco.Infrastructure.EF.Persistence.Configurations.GroupConf
{
    public class GroupMemberConfiguration : IEntityTypeConfiguration<GroupMember>
    {
        public void Configure(EntityTypeBuilder<GroupMember> builder)
        {
            builder.HasOne(x => x.Group)
              .WithMany(m => m.Members)
              .HasForeignKey(x => x.GroupId);
            builder.HasQueryFilter(c => !c.Member.IsDeleted);
        }
    }
}