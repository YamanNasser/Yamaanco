using System.Collections.Generic;
using System.Threading.Tasks;
using Yamaanco.Application.DTOs.Group;

namespace Yamaanco.Application.Interfaces.Repositories.Group
{
    public interface IGroupRepository : IRepository<Domain.Entities.GroupEntities.Group>
    {
        Task<IList<GroupDto>> GetGroupByNameAndIncludingResourcesOnly(string name, int skip, int take, string userId);

        Task<GroupDto> GetGroupById(string id, string userId);

        Task<IList<GroupFilterResultDto>> GetAllGroupIncludingResourcesOnly(string filter, string sortColumn, string sortColumnDirection, int skip, int take);

        Task<IList<GroupDto>> GetGroupUserAndIncludingResourcesOnly(string userId, int skip, int take, params int[] groupType);
    }
}