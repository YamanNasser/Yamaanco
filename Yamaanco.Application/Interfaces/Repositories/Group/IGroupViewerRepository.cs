using System.Collections.Generic;
using System.Threading.Tasks;
using Yamaanco.Application.DTOs.Group;
using Yamaanco.Domain.Entities.GroupEntities;

namespace Yamaanco.Application.Interfaces.Repositories.Group
{
    public interface IGroupViewerRepository : IRepository<GroupViewer>
    {
        Task<int> CreateGroupViewer(string groupId, string viewerId);

        Task<IList<GroupViewersInfoDto>> GetGroupViewers(string groupId, int pageIndex, int pageSize);
    }
}