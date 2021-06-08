using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yamaanco.Domain.Entities.ProfileEntities;

namespace Yamaanco.Infrastructure.EF.Persistence.Configurations.ProfileConf
{
    public class ProfileCommentConfiguration : IEntityTypeConfiguration<ProfileComment>
    {
        public void Configure(EntityTypeBuilder<ProfileComment> builder)
        {
            builder.HasQueryFilter(c => !c.IsDeleted);
        }
    }
}