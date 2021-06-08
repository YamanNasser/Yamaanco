using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yamaanco.Domain.Entities.ProfileEntities;

namespace Yamaanco.Infrastructure.EF.Persistence.Configurations.ProfileConf
{
    public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder.HasIndex(u => new { u.Email })
                  .IsUnique(true);
            builder.HasIndex(u => new { u.UserName })
                 .IsUnique(true);
            builder.HasIndex(u => new { u.PhoneNumber })
                 .IsUnique(true);

            builder.HasQueryFilter(c => !c.IsDeleted);
            builder.HasQueryFilter(c => !c.IsDeactivated);
        }
    }
}