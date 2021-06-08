using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yamaanco.Domain.Entities.ProfileEntities;

namespace Yamaanco.Infrastructure.EF.Persistence.Configurations.ProfileConf
{
    public class ProfileMessageConfiguration : IEntityTypeConfiguration<ProfileMessage>
    {
        public void Configure(EntityTypeBuilder<ProfileMessage> builder)
        {
            builder.HasOne(x => x.Profile)
                   .WithMany(m => m.Messages)
                   .HasForeignKey(x => x.ProfileId);

            builder.HasOne(x => x.File)
                    .WithOne(m => m.Message)
                    .HasForeignKey<ProfileMessage>(x => x.FileId);

            builder.HasQueryFilter(c => !c.IsDeleted);
        }
    }
}