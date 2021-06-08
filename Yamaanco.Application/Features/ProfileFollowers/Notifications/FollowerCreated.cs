using MediatR;
using System;
using Yamaanco.Application.Structs;

namespace Yamaanco.Application.Features.GroupFollowers.Notifications.Created
{
    public class FollowerCreated : INotification
    {
        public string ProfileId { get; set; }
        public string ProfileUserName { get; set; }
        public string ProfilePhone { get; set; }
        public string ProfileEmail { get; set; }
        public int NumberOfUnSeenProfileFollowers { get; set; }
        public string FollowerId { get; set; }
        public string FollowerName { get; set; }
        public ElapsedTimeValue ElapsedTime { get; set; }
        public DateTime FollowedDate { get; set; }
        public string FollowerProfileMediumPhotoPath { get; set; }
    }
}