using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yamaanco.Application.DTOs.Notification;
using Yamaanco.Application.Interfaces;
using Yamaanco.Application.Interfaces.Repositories.Notifications;
using Yamaanco.Domain.Enums;

namespace Yamaanco.Infrastructure.EF.Persistence.MSSQL.Repositories.NotificationsRepository
{
    public class NotificationsRepository : INotificationsRepository
    {
        private readonly NotificationType[] generalNotifications = new[]{
                NotificationType.NewComment,
                NotificationType.UpdateComment,
                NotificationType.NewReply,
                NotificationType.GroupRequest,
                NotificationType.SystemMessage
            };

        private readonly NotificationType[] messageNotifications = new[]{
                NotificationType.NewMessage,
                NotificationType.VideoOrAudioCall
            };

        protected readonly IYamaancoDbContext Context;

        public NotificationsRepository(IYamaancoDbContext context)
        {
            Context = context;
        }


        public async Task<IList<NotificationDto>> GetAllGeneralNotifications(string profileId, int pageIndex, int pageSize, bool? withIsSeen = null)
        {
            List<NotificationDto> messages;
            IDbConnection db = Context.YamaacoDatabase.GetDbConnection();
            try
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                if (withIsSeen == null)
                {
                    var @params = new
                    {
                        ProfileId = profileId,
                        PageSize = pageSize,
                        PageIndex = pageIndex,
                        NotificationType = generalNotifications
                    };

                    var result = await db.QueryAsync<NotificationDto>(@"
                            select * from [vwNotifications]
                            where ProfileId=@ProfileId and
                            NotificationType in @NotificationType
                            order by CreatedDate desc
                            OFFSET (@PageIndex-1)*@PageSize ROWS
                            FETCH NEXT @PageSize ROWS ONLY", @params);
                    messages = result.ToList();
                    await SetNotificationAsSeenAsync(messages, db);
                }
                else
                {
                    var @params = new
                    {
                        ProfileId = profileId,
                        PageSize = pageSize,
                        PageIndex = pageIndex,
                        NotificationType = generalNotifications,
                        IsSeen = withIsSeen
                    };

                    var result = await db.QueryAsync<NotificationDto>(@"
                            select * from [vwNotifications]
                            where IsSeen=@IsSeen and ProfileId=@ProfileId
                            and NotificationType in @NotificationType
                            order by CreatedDate desc
                            OFFSET (@PageIndex-1)*@PageSize ROWS
                            FETCH NEXT @PageSize ROWS ONLY", @params);
                    messages = result.ToList();
                    await SetNotificationAsSeenAsync(messages, db);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                {
                    db.Close();
                }
            }
            return messages;
        }



        private async Task SetNotificationAsSeenAsync(List<NotificationDto> notifications, IDbConnection db)
        {
            StringBuilder sql = new();

            List<string> profileNotificationIdList = new();
            List<string> groupNotificationIdList = new();

            notifications.ForEach(notification =>
            {
                if (notification.CategoryType == NotificationCategory.Profile)
                {
                    profileNotificationIdList.Add(notification.Id);
                }
                else if (notification.CategoryType == NotificationCategory.Group)
                {
                    groupNotificationIdList.Add(notification.Id);
                }
            });

            using (var trn = db.BeginTransaction())
            {
                try
                {
                    if (profileNotificationIdList.Any())
                    {
                        var @params = new
                        {
                            Ids = profileNotificationIdList.ToArray()
                        };

                        await db.ExecuteAsync(@"
                                  update ProfileNotification set IsSeen = 1 where ID in @Ids",
                                          @params, trn);
                    }

                    if (groupNotificationIdList.Any())
                    {
                        var @params = new
                        {
                            Ids = groupNotificationIdList.ToArray()
                        };
                        await db.ExecuteAsync(@"
                                  update GroupNotification set IsSeen = 1 where ID in @ids",
                                          @params, trn);
                    }

                    trn.Commit();
                }
                catch (Exception)
                {
                    trn.Rollback();
                    throw;
                }
            }
        }


        public async Task<IList<NotificationDto>> GetAllMessageNotifications(string profileId, int pageIndex, int pageSize, bool? withIsSeen = null)
        {
            List<NotificationDto> messages;
            IDbConnection db = Context.YamaacoDatabase.GetDbConnection();
            try
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                if (withIsSeen == null)
                {
                    var @params = new
                    {
                        ProfileId = profileId,
                        PageSize = pageSize,
                        PageIndex = pageIndex,
                        NotificationType = messageNotifications
                    };

                    var result = await db.QueryAsync<NotificationDto>(@"
                            select * from [vwNotifications]
                            where ProfileId=@ProfileId and
                            NotificationType in @NotificationType
                            order by CreatedDate desc
                            OFFSET (@PageIndex-1)*@PageSize ROWS
                            FETCH NEXT @PageSize ROWS ONLY", @params);
                    messages = result.ToList();

                    await SetNotificationAsSeenAsync(messages, db);
                }
                else
                {
                    var @params = new
                    {
                        ProfileId = profileId,
                        PageSize = pageSize,
                        PageIndex = pageIndex,
                        NotificationType = messageNotifications,
                        IsSeen = withIsSeen
                    };

                    var result = await db.QueryAsync<NotificationDto>(@"
                            select * from [vwNotifications]
                            where IsSeen=@IsSeen and ProfileId=@ProfileId
                            and NotificationType in @NotificationType
                            order by CreatedDate desc
                            OFFSET (@PageIndex-1)*@PageSize ROWS
                            FETCH NEXT @PageSize ROWS ONLY", @params);
                    messages = result.ToList();
                    await SetNotificationAsSeenAsync(messages, db);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                {
                    db.Close();
                }
            }
            return messages;
        }



        //----------------------------------------------------------------------------
        public async Task<int> GetNumberOfUnSeenMessageNotification(string profileId)
        {
            var count = 0;

            IDbConnection db = Context.YamaacoDatabase.GetDbConnection();
            try
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                var @params = new { ProfileId = profileId };

                count = await db.ExecuteScalarAsync<int>(@"select
                                (select count(*)
                                from GroupNotification
                                where  ProfileId=@ProfileId and
                                (IsSeen is null or IsSeen=0)
                                and NotificationType in (0,30))
                                +
                                (select count(*)
                                from ProfileNotification
                                where  ProfileId=@ProfileId and
                                (IsSeen is null or IsSeen=0)
                                and NotificationType in (0,30)) ", @params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                {
                    db.Close();
                }
            }

            return count;
        }

        public async Task<int> GetNumberOfUnSeenGeneralNotification(string profileId)
        {
            var count = 0;

            IDbConnection db = Context.YamaacoDatabase.GetDbConnection();
            try
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                var @params = new { ProfileId = profileId, NotificationType = generalNotifications };

                count = await db.ExecuteScalarAsync<int>(@"select
                                (select count(*)
                                from GroupNotification
                                where  ProfileId=@ProfileId and
                                (IsSeen is null or IsSeen=0)
                                and NotificationType in @NotificationType)
                                +
                                (select count(*)
                                from ProfileNotification
                                where  ProfileId=@ProfileId and
                                (IsSeen is null or IsSeen=0)
                                and NotificationType in @NotificationType) ", @params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                {
                    db.Close();
                }
            }

            return count;
        }

        public async Task<Dictionary<string, int>> GetNumberOfUnSeenGeneralNotificationForProfileFollower(string profileId)
        {
            Dictionary<string, int> dictionaryResult;

            IDbConnection db = Context.YamaacoDatabase.GetDbConnection();
            try
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                var @params = new { ProfileId = profileId, NotificationType = generalNotifications };

                var result = await db.QueryAsync<KeyValuePair<string, int>>(@"
                       select [Key],sum([Value]) as [Value] from(
                        select ProfileId as [Key],count(*) as [Value]
                        from ProfileNotification
                        where ProfileId in (select ParticipantId from ProfileFollower where ProfileId=@ProfileId)
                        and (IsSeen is null or IsSeen=0) and NotificationType in @NotificationType
                        group by ProfileId
                        UNION ALL
                        select ProfileId as [Key],count(*) as [Value]
                        from GroupNotification
                        where ProfileId in (select ParticipantId from ProfileFollower where ProfileId=@ProfileId)
                        and (IsSeen is null or IsSeen=0) and NotificationType in @NotificationType
                        group by ProfileId
                        ) as TotalNotifications
                        group by [Key] ", @params);
                dictionaryResult = new Dictionary<string, int>(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                {
                    db.Close();
                }
            }

            return dictionaryResult;
        }

        public async Task<Dictionary<string, int>> GetNumberOfUnSeenGeneralNotificationForGroupMembers(string groupId)
        {
            Dictionary<string, int> dictionaryResult;

            IDbConnection db = Context.YamaacoDatabase.GetDbConnection();
            try
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                var @params = new { GroupId = groupId, NotificationType = generalNotifications };

                var result = await db.QueryAsync<KeyValuePair<string, int>>(@"
                       select [Key],sum([Value]) as [Value] from(
                        select GroupId as [Key],count(*) as [Value]
                        from GroupNotification
                        where GroupId in (select ParticipantId from GroupMember where GroupId=@GroupId)
                        and (IsSeen is null or IsSeen=0) and NotificationType in @NotificationType
                        group by GroupId
                        UNION ALL
                        select GroupId as [Key],count(*) as [Value]
                        from GroupNotification
                        where GroupId in (select ParticipantId from GroupMember where GroupId=@GroupId)
                        and (IsSeen is null or IsSeen=0) and NotificationType in @NotificationType
                        group by GroupId
                        ) as TotalNotifications
                        group by [Key] ", @params);
                dictionaryResult = new Dictionary<string, int>(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                {
                    db.Close();
                }
            }

            return dictionaryResult;
        }

        public async Task<Dictionary<string, int>> GetNumberOfUnSeenGeneralNotificationForProfileList(string[] profileIdList)
        {
            Dictionary<string, int> dictionaryResult;

            IDbConnection db = Context.YamaacoDatabase.GetDbConnection();
            try
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                var @params = new { ProfileIdList = profileIdList, NotificationType = generalNotifications };

                var result = await db.QueryAsync<KeyValuePair<string, int>>(@" select [Key],sum([Value]) as [Value] from(
                        select ProfileId as [Key],count(*) as [Value]
                        from ProfileNotification
                        where ProfileId in @ProfileIdList
                        and (IsSeen is null or IsSeen=0) and NotificationType in @NotificationType
                        group by ProfileId
                        UNION ALL
                        select ProfileId as [Key],count(*) as [Value]
                        from GroupNotification
                        where ProfileId in @ProfileIdList
                        and (IsSeen is null or IsSeen=0) and NotificationType in @NotificationType
                        group by ProfileId
                        ) as TotalNotifications
                        group by [Key]
                        ", @params);

                dictionaryResult = new Dictionary<string, int>(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                {
                    db.Close();
                }
            }

            return dictionaryResult;
        }

        public async Task<IList<NotificationDto>> GetUnSeeGeneralNotifications(string profileId, int pageIndex, int pageSize)
        {
            return await GetAllGeneralNotifications(profileId, pageIndex, pageSize, false);
        }

        public async Task<IList<NotificationDto>> GetUnSeeMessageNotifications(string profileId, int pageIndex, int pageSize)
        {
            return await GetAllMessageNotifications(profileId, pageIndex, pageSize, false);
        }
    }
}