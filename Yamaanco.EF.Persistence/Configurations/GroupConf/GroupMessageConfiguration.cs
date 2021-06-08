using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yamaanco.Domain.Entities.GroupEntities;

namespace Yamaanco.Infrastructure.EF.Persistence.Configurations.GroupConf
{
    public class GroupMessageConfiguration : IEntityTypeConfiguration<GroupMessage>
    {
        public void Configure(EntityTypeBuilder<GroupMessage> builder)
        {
            builder.HasOne(x => x.Group)
                 .WithMany(m => m.Messages)
                 .HasForeignKey(x => x.GroupId);

            builder.HasOne(x => x.File)
          .WithOne(m => m.Message)
          .HasForeignKey<GroupMessage>(x => x.FileId);
            builder.HasQueryFilter(c => !c.IsDeleted);
        }
    }
}