using System;
using Yamaanco.Domain.Enums;

namespace Yamaanco.Domain.Entities.UserLogsEntities
{
    public class UserLoginLogs
    {
        public UserLoginLogs(string userId, DateTime actionDate, int userActionId, UserLogsAction userAction, string userAgent, string data)
        {
            Id = Guid.NewGuid().ToString();
            UserId = userId;
            ActionDate = actionDate;
            UserActionId = userActionId;
            UserAction = userAction;
            UserAgent = userAgent;
            Data = data;
        }

        public string Id { get; private set; }

        public string UserId { get; private set; }
        public DateTime ActionDate { get; private set; }
        public int UserActionId { get; private set; }
        public UserLogsAction UserAction { get; private set; }
        public string UserAgent { get; private set; }
        public string Data { get; private set; }
    }
}