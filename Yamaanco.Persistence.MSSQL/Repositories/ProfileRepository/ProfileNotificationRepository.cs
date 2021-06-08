using Yamaanco.Application.Interfaces;
using Yamaanco.Application.Interfaces.Repositories.Profile;
using Yamaanco.Domain.Entities.ProfileEntities;
using Yamaanco.Infrastructure.EF.Persistence.MSSQL.Common;

namespace Yamaanco.Infrastructure.EF.Persistence.MSSQL.Repositories.ProfileRepository
{
    public class ProfileNotificationRepository : Repository<ProfileNotification>, IProfileNotificationRepository
    {
        //private NotificationType[] generalNotifications = new[]{
        //        NotificationType.NewComment,
        //        NotificationType.UpdateComment,
        //        NotificationType.NewReply,
        //        NotificationType.GroupRequest,
        //        NotificationType.SystemMessage
        //    };

        //private NotificationType[] messageNotifications = new[]{
        //        NotificationType.NewMessage,
        //        NotificationType.VideoOrAudioCall
        //    };

        public ProfileNotificationRepository(IYamaancoDbContext context)
            : base(context)
        {
        }

        //public async Task<IList<MessageNotificationDto>> GetUnSeeMessagenNotifications(string profileId, int pageIndex, int pageSize)
        //{
        //    var messages = await (from message in Context.ProfileNotification
        //                  .Where(o => o.ProfileId == profileId && !o.IsSeen)
        //                  .Where(o => messageNotifications
        //                  .Contains(o.NotificationType))
        //                  .OrderByDescending(o => o.CreatedDate)
        //                  .Skip(pageIndex)
        //                  .Take(pageSize)
        //                  .Include(o => o.Participant)
        //                  .ThenInclude(o => o.PhotoResources)
        //                  .Include(o => o.Participant)
        //                  .ThenInclude(o => o.Gender)
        //                          select new MessageNotificationDto()
        //                          {
        //                              IsSeen = false,
        //                              ParticipantId = message.ParticipantId,
        //                              ParticipantName = message.Participant.UserName,
        //                              CategoryType = NotificationCategory.Profile,
        //                              GenderId = message.Participant.Gender.Id,
        //                              Message = message.Content,
        //                              Target = message.Target,
        //                              ParticipantPhotoPath = "",
        //                              CreatedDate = message.CreatedDate
        //                          }).AsNoTracking()
        //                          .ToListAsync();

        //    return messages;
        //}

        //public async Task<IList<MessageNotificationDto>> GetAllMessageNotifications(string profileId, int pageIndex, int pageSize, bool? withIsSeen = null)
        //{
        //    var messages = await (from message in Context.ProfileNotification
        //                 .Where(o => o.ProfileId == profileId)
        //                 .Where(o => messageNotifications
        //                 .Contains(o.NotificationType))
        //                 .Where(o => withIsSeen == null ? 1 == 1 : o.IsSeen == withIsSeen)
        //                  .OrderByDescending(o => o.CreatedDate)
        //                  .Skip(pageIndex)
        //                  .Take(pageSize)
        //                  .Include(o => o.Participant)
        //                  .ThenInclude(o => o.PhotoResources)
        //                  .Include(o => o.Participant)
        //                  .ThenInclude(o => o.Gender)
        //                          select new MessageNotificationDto()
        //                          {
        //                              ParticipantId = message.ParticipantId,
        //                              ParticipantName = message.Participant.UserName,
        //                              CategoryType = NotificationCategory.Profile,
        //                              GenderId = message.Participant.Gender.Id,
        //                              Message = message.Content,
        //                              Target = message.Target,
        //                              ParticipantPhotoPath = "",
        //                              CreatedDate = message.CreatedDate,
        //                              IsSeen = message.IsSeen
        //                          }).AsNoTracking()
        //                          .ToListAsync();

        //    return messages;
        //}

        //public async Task<int> GetNumberOfUnSeenMessageNotification(string profileId)
        //{
        //    var count = await Context.ProfileNotification
        //        .CountAsync(o => o.ProfileId == profileId
        //        && !o.IsSeen
        //        && messageNotifications.Contains(o.NotificationType));
        //    return count;
        //}

        //public async Task<int> GetNumberOfUnSeenGeneralNotification(string profileId)
        //{
        //    var count = await Context.ProfileNotification
        //        .CountAsync(o => o.ProfileId == profileId
        //        && !o.IsSeen
        //        && generalNotifications.Contains(o.NotificationType));
        //    return count;
        //}

        //public async Task<Dictionary<string, int>> GetNumberOfUnSeenGeneralNotificationForProfileFollower(string profileId)
        //{
        //    //var cacheEntryOptions = new MemoryCacheEntryOptions()
        //    //// Keep in cache for this time, reset time if accessed.
        //    //.SetSlidingExpiration(TimeSpan.FromSeconds(3));

        //    var result = await (from n in Context.ProfileNotification
        //                        join pf in Context.ProfileFollower
        //                        on n.ParticipantId equals pf.FollowerProfileId
        //                        where !n.IsSeen && pf.ProfileId == profileId &&
        //                        generalNotifications.Contains(n.NotificationType)
        //                        group n by n.ParticipantId into g
        //                        select new
        //                        {
        //                            g.Key,
        //                            Value = g.Count()
        //                        })
        //                        .AsNoTracking()
        //                        .ToDictionaryAsync(o => o.Key, oo => oo.Value);

        //    return result;
        //}

        //public async Task<Dictionary<string, int>> GetNumberOfUnSeenGeneralNotificationForProfileList(string[] profileIdList)
        //{
        //    var numberOfMentionedNotification = await Context.ProfileNotification
        //                                .Where(o => profileIdList
        //                                .Contains(o.ProfileId))
        //                                .Where(n => generalNotifications
        //                                .Contains(n.NotificationType))
        //                                .GroupBy(o => o.ProfileId)
        //                                .Select(o => new
        //                                {
        //                                    o.Key,
        //                                    Value = o.Count()
        //                                })
        //                                .AsNoTracking()
        //                                .ToDictionaryAsync(o => o.Key, oo => oo.Value);
        //    return numberOfMentionedNotification;
        //}
    }
}