using Yamaanco.Application.Interfaces;
using Yamaanco.Application.Interfaces.Repositories.Profile;
using Yamaanco.Domain.Entities.ProfileEntities;
using Yamaanco.Infrastructure.EF.Persistence.MSSQL.Common;

namespace Yamaanco.Infrastructure.EF.Persistence.MSSQL.Repositories.ProfileRepository
{
    public class ProfileMessageResourcesRepository : Repository<ProfileMessageResources>, IProfileMessageResourcesRepository
    {
        public ProfileMessageResourcesRepository(IYamaancoDbContext context)
            : base(context)
        {
        }
    }
}