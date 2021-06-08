using System.Collections.Generic;
using System.Threading.Tasks;
using Yamaanco.Application.DTOs.Profile;

namespace Yamaanco.Application.Interfaces.Repositories.Profile
{
    public interface IProfileRepository : IRepository<Domain.Entities.ProfileEntities.Profile>
    {
        Task<ProfileDto> GetProfileById(string id);

        Task<string> GetProfileUserName(string id);

        Task<IList<ProfileDto>> GetProfileList(string filter, string sortColumn, string sortColumnDirection, int skip, int take);

        Task<IList<ProfileDto>> GetProfileByName(string name, int skip, int take);

        bool HaveProfile(string id);
        Task<string> GetProfileIdByEmail(string email);
    }
}