using System.Collections.Generic;
using System.Threading.Tasks;
using Yamaanco.Application.DTOs.Message;
using Yamaanco.Domain.Entities.ProfileEntities;

namespace Yamaanco.Application.Interfaces.Repositories.Profile
{
    public interface IProfileMessageRepository : IRepository<ProfileMessage>
    {
        Task<MessageDto> GetProfileMessage(string id);

        Task<List<MessageDto>> GetMessagesByTarget(string targetId, int pageIndex, int pageSize);
    }
}