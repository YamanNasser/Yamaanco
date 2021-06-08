using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yamaanco.Domain.Entities.ProfileEntities;

namespace Yamaanco.Infrastructure.EF.Persistence.Configurations.ProfileConf
{
    public class ProfileMessageViewerConfiguration : IEntityTypeConfiguration<ProfileMessageViewer>
    {
        public void Configure(EntityTypeBuilder<ProfileMessageViewer> builder)
        {
        }
    }
}