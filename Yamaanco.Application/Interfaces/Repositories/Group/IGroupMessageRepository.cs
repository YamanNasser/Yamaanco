using System.Collections.Generic;
using System.Threading.Tasks;
using Yamaanco.Application.DTOs.Message;
using Yamaanco.Domain.Entities.GroupEntities;

namespace Yamaanco.Application.Interfaces.Repositories.Group
{
    public interface IGroupMessageRepository : IRepository<GroupMessage>
    {
        Task<MessageDto> GetGroupMessage(string id);

        Task<List<MessageDto>> GetMessagesByTarget(string targetId, int pageIndex, int pageSize);
    }
}