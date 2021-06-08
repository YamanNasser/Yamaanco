using System.Collections.Generic;
using System.Threading.Tasks;
using Yamaanco.Application.DTOs.Profile;
using Yamaanco.Domain.Entities.ProfileEntities;

namespace Yamaanco.Application.Interfaces.Repositories.Profile
{
    public interface IProfileViewerRepository : IRepository<ProfileViewer>
    {
        Task<int> CreateProfileViewer(string profileId, string viewerId);

        Task<IList<ProfileViewersInfoDto>> GetProfileViewers(string id, int skip, int take);
        Task<int> GetProfileViewersCount(string profileId);
        Task<bool> IsUserViewdProfileBefore(string userId, string profileId);
    }
}