using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Yamaanco.Application.DTOs.Common;
using Yamaanco.Application.DTOs.Message;
using Yamaanco.Application.Interfaces;
using Yamaanco.Application.Interfaces.Repositories.Profile;
using Yamaanco.Domain.Entities.ProfileEntities;
using Yamaanco.Domain.Enums;
using Yamaanco.Infrastructure.EF.Persistence.MSSQL.Common;

namespace Yamaanco.Infrastructure.EF.Persistence.MSSQL.Repositories.ProfileRepository
{
    public class ProfileMessageRepository : Repository<ProfileMessage>, IProfileMessageRepository
    {
        public ProfileMessageRepository(IYamaancoDbContext context)
            : base(context)
        {
        }

        public async Task<List<MessageDto>> GetMessagesByTarget(string targetId, int pageIndex, int pageSize)
        {
            var message = await Context.ProfileMessage
                         .Where(o => o.Target == targetId)
                         .OrderByDescending(o => o.Created)
                         .Skip(pageIndex)
                         .Take(pageSize)
                         .Include(o => o.File)
                         .Include(o => o.Profile)
                         .ThenInclude(o => o.PhotoResources)
                         .Include(o => o.CreatedBy)
                         .ThenInclude(o => o.PhotoResources)
                         .Select(message => new MessageDto()
                         {
                             Content = message.Content,
                             Created = message.Created,
                             ParticipantId = message.CreatedById,
                             ParticipantName = message.CreatedBy.UserName,
                             Id = message.Id,
                             ParticipantPictureUrl = message.CreatedBy
                             .PhotoResources
                             .First(o => o.PhotoSize == PhotoSize.Small).Path,
                             CategoryPictureUrl = message.Profile
                             .PhotoResources
                             .First(o => o.PhotoSize == PhotoSize.Small).Path,
                             CategoryName = message.Profile.UserName,
                             CategoryId = message.ProfileId,
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

        public async Task<MessageDto> GetProfileMessage(string id)
        {
            var message = await Context.ProfileMessage
                         .Where(o => o.Id == id)
                         .Include(o => o.File)
                         .Include(o => o.Profile)
                         .ThenInclude(o => o.PhotoResources)
                         .Include(o => o.CreatedBy)
                         .ThenInclude(o => o.PhotoResources)
                         .Select(message => new MessageDto()
                         {
                             Content = message.Content,
                             Created = message.Created,
                             ParticipantId = message.CreatedById,
                             ParticipantName = message.CreatedBy.UserName,
                             Id = message.Id,
                             ParticipantPictureUrl = message.CreatedBy
                             .PhotoResources
                             .First(o => o.PhotoSize == PhotoSize.Small).Path,
                             CategoryPictureUrl = message.Profile
                             .PhotoResources
                             .First(o => o.PhotoSize == PhotoSize.Small).Path,
                             CategoryName = message.Profile.UserName,
                             CategoryId = message.ProfileId,
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