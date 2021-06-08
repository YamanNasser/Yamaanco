using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yamaanco.Domain.Entities.GroupEntities;

namespace Yamaanco.Infrastructure.EF.Persistence.Configurations.GroupConf
{
    internal class GroupViewerConfiguration : IEntityTypeConfiguration<GroupViewer>
    {
        public void Configure(EntityTypeBuilder<GroupViewer> builder)
        {
            builder.HasOne(x => x.Group)
                .WithMany(m => m.Viewers)
                .HasForeignKey(x => x.GroupId);
            builder.HasQueryFilter(c => !c.ViewerProfile.IsDeleted);
        }
    }
}