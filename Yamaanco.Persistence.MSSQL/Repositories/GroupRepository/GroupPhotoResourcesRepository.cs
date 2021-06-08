using Yamaanco.Application.Interfaces;
using Yamaanco.Application.Interfaces.Repositories.Group;
using Yamaanco.Domain.Entities.GroupEntities;
using Yamaanco.Infrastructure.EF.Persistence.MSSQL.Common;

namespace Yamaanco.Infrastructure.EF.Persistence.MSSQL.Repositories.GroupRepository
{
    public class GroupPhotoResourcesRepository : Repository<GroupPhotoResources>, IGroupPhotoResourcesRepository
    {
        public GroupPhotoResourcesRepository(IYamaancoDbContext context)
            : base(context)
        {
        }
    }
}