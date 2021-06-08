using System.Collections.Generic;
using System.Threading.Tasks;
using Yamaanco.Application.DTOs.Profile;
using Yamaanco.Domain.Entities.ProfileEntities;

namespace Yamaanco.Application.Interfaces.Repositories.Profile
{
    public interface IProfileFollowerRepository : IRepository<ProfileFollower>
    {
        Task<IList<ProfileFollowersDto>> GetProfileFollowers(string id, int skip, int take, bool? withIsSeen = null);

        Task<IList<ProfileFollowerBasicInfoDto>> GetAllProfileFollowersBasicList(string id);

        Task<bool> IsUserFollowedProfile(string userId, string profileId);

        Task<int> DeleteProfileFollower(string profileId, string followerId);

        Task<CreatedProfileFollowerBasicInfoDto> CreateProfileFollower(string profileId, string followerId);

        Task<IList<string>> GetUnMentiondFollowersIdList(IList<string> userIdPings, string profileId);
    }
}