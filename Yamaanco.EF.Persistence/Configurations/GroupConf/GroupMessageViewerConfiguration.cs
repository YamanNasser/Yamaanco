using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yamaanco.Domain.Entities.GroupEntities;

namespace Yamaanco.Infrastructure.EF.Persistence.Configurations.GroupConf
{
    public class GroupMessageViewerConfiguration : IEntityTypeConfiguration<GroupMessageViewer>
    {
        public void Configure(EntityTypeBuilder<GroupMessageViewer> builder)
        {
        }
    }
}