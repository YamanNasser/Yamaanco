using System.Collections.Generic;
using System.Threading.Tasks;
using Yamaanco.Application.DTOs.Group;
using Yamaanco.Application.Features.GroupMembers.ViewModel;
using Yamaanco.Domain.Entities.GroupEntities;

namespace Yamaanco.Application.Interfaces.Repositories.Group
{
    public interface IGroupMemberRepository : IRepository<GroupMember>
    {
        Task<CreatedGroupMemberBasicInfoDto> CreateGroupMember(string groupId, string memberId);

        Task<int> DeleteGroupMember(string groupId, string memberId);

        Task<IList<GroupMemberDto>> GetGroupMembers(string groupId, string memberNameContains, string currentUserId, int skip, int take);

        Task<IList<string>> GetUnMentiondMembersIdList(IList<string> userIdPings, string groupId, string creatorId);
    }
}