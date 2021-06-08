using System.Collections.Generic;
using System.Threading.Tasks;
using Yamaanco.Application.DTOs.Notification;

namespace Yamaanco.Application.Interfaces.Repositories.Notifications
{
    public interface INotificationsRepository
    {
        Task<Dictionary<string, int>> GetNumberOfUnSeenGeneralNotificationForProfileList(string[] profileIdList);

        Task<Dictionary<string, int>> GetNumberOfUnSeenGeneralNotificationForProfileFollower(string profileId);

        Task<int> GetNumberOfUnSeenMessageNotification(string profileId);

        Task<int> GetNumberOfUnSeenGeneralNotification(string profileId);

        Task<IList<NotificationDto>> GetUnSeeMessageNotifications(string profileId, int pageIndex, int pageSize);

        Task<IList<NotificationDto>> GetAllMessageNotifications(string profileId, int pageIndex, int pageSize, bool? withIsSeen = null);

        Task<IList<NotificationDto>> GetUnSeeGeneralNotifications(string profileId, int pageIndex, int pageSize);

        Task<IList<NotificationDto>> GetAllGeneralNotifications(string profileId, int pageIndex, int pageSize, bool? withIsSeen = null);

        Task<Dictionary<string, int>> GetNumberOfUnSeenGeneralNotificationForGroupMembers(string groupId);
    }
}