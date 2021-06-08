using Yamaanco.Application.Interfaces;
using Yamaanco.Application.Interfaces.Repositories.Profile;
using Yamaanco.Domain.Entities.ProfileEntities;
using Yamaanco.Infrastructure.EF.Persistence.MSSQL.Common;

namespace Yamaanco.Infrastructure.EF.Persistence.MSSQL.Repositories.ProfileRepository
{
    public class ProfileMessageViewerRepository : Repository<ProfileMessageViewer>, IProfileMessageViewerRepository
    {
        public ProfileMessageViewerRepository(IYamaancoDbContext context)
               : base(context)
        {
        }
    }
}