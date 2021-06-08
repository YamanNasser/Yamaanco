using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Yamaanco.Application.DTOs.Common;
using Yamaanco.Application.DTOs.Message;
using Yamaanco.Application.Interfaces;
using Yamaanco.Application.Interfaces.Repositories.Group;
using Yamaanco.Domain.Entities.GroupEntities;
using Yamaanco.Domain.Enums;
using Yamaanco.Infrastructure.EF.Persistence.MSSQL.Common;

namespace Yamaanco.Infrastructure.EF.Persistence.MSSQL.Repositories.GroupRepository
{
    public class GroupMessageRepository : Repository<GroupMessage>, IGroupMessageRepository
    {
        public GroupMessageRepository(IYamaancoDbContext context)
            : base(context)
        {
        }

        public async Task<List<MessageDto>> GetMessagesByTarget(string targetId, int pageIndex, int pageSize)
        {
            var message = await Context.GroupMessage
                         .Where(o => o.Target == targetId)
                         .OrderByDescending(o => o.Created)
                         .Skip(pageIndex)
                         .Take(pageSize)
                         .Include(o => o.File)
                         .Include(o => o.Group)
                         .ThenInclude(o => o.PhotoResources)
                         .Include(o => o.CreatedBy)
                         .ThenInclude(o => o.PhotoResources)
                         .Select(message => new MessageDto()
                         {
                             Content = message.Content,
                             Created = message.Created,
                             ParticipantId = message.GroupId,
                             ParticipantName = message.Group.Name,
                             Id = message.Id,
                             ParticipantPictureUrl = message.CreatedBy
                             .PhotoResources
                             .First(o => o.PhotoSize == PhotoSize.Small).Path,
                             CategoryPictureUrl = message.Group
                             .PhotoResources
                             .First(o => o.PhotoSize == PhotoSize.Small).Path,
                             CategoryName = message.CreatedBy.UserName,
                             CategoryId = message.CreatedById,
                             Target = message.Target,
                             ViewCount = message.ViewCount,
                             File = new ResourcesBasicInfoDto
                             {
                                 Extension = message.File.Extension,
                                 FileId = message.File.Id,
                                 Path = message.File.FullPath
                             }
                         }).AsNoTracking()
                         .ToListAsync();

            return message;
        }

        public async Task<MessageDto> GetGroupMessage(string id)
        {
            var message = await Context.GroupMessage
                         .Where(o => o.Id == id)
                         .Include(o => o.File)
                         .Include(o => o.Group)
                         .ThenInclude(o => o.PhotoResources)
                         .Include(o => o.CreatedBy)
                         .ThenInclude(o => o.PhotoResources)
                         .Select(message => new MessageDto()
                         {
                             Content = message.Content,
                             Created = message.Created,
                             ParticipantId = message.GroupId,
                             ParticipantName = message.Group.Name,
                             Id = message.Id,
                             ParticipantPictureUrl = message.CreatedBy
                             .PhotoResources
                             .First(o => o.PhotoSize == PhotoSize.Small).Path,
                             CategoryPictureUrl = message.Group
                             .PhotoResources
                             .First(o => o.PhotoSize == PhotoSize.Small).Path,
                             CategoryName = message.CreatedBy.UserName,
                             CategoryId = message.CreatedById,
                             Target = message.Target,
                             ViewCount = message.ViewCount,
                             File = new ResourcesBasicInfoDto
                             {
                                 Extension = message.File.Extension,
                                 FileId = message.File.Id,
                                 Path = message.File.FullPath
                             }
                         }).AsNoTracking()
                         .SingleOrDefaultAsync();

            return message;
        }
    }
}