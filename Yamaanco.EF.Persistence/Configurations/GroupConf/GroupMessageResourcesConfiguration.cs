using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yamaanco.Domain.Entities.GroupEntities;

namespace Yamaanco.Infrastructure.EF.Persistence.Configurations.GroupConf
{
    public class GroupMessageResourcesConfiguration : IEntityTypeConfiguration<GroupMessageResources>
    {
        public void Configure(EntityTypeBuilder<GroupMessageResources> builder)
        {
            builder.HasOne(x => x.Message)
         .WithOne(m => m.File)
         .HasForeignKey<GroupMessageResources>(x => x.MessageId);
        }
    }
}