using Yamaanco.Application.Interfaces;
using Yamaanco.Application.Interfaces.Repositories.Profile;
using Yamaanco.Domain.Entities.ProfileEntities;
using Yamaanco.Infrastructure.EF.Persistence.MSSQL.Common;

namespace Yamaanco.Infrastructure.EF.Persistence.MSSQL.Repositories.ProfileRepository
{
    public class ProfileCommentHashtagRepository : Repository<ProfileCommentHashtag>, IProfileCommentHashtagRepository
    {
        public ProfileCommentHashtagRepository(IYamaancoDbContext context)
            : base(context)
        {
        }
    }
}