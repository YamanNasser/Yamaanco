using System;

namespace Yamaanco.Application.DTOs.Profile
{
    public class CreatedProfileFollowerBasicInfoDto
    {
        public string ProfileId { get; set; }
        public string ProfileUserName { get; set; }
        public string ProfilePhone { get; set; }
        public string ProfileEmail { get; set; }
        public int NumberOfUnSeenProfileFollowers { get; set; }

        public int NumberOfProfileFollowers { get; set; }
        public string FollowerId { get; set; }
        public string FollowerName { get; set; }
        public DateTime FollowedDate { get; set; }
        public string FollowerProfileMediumPhotoPath { get; set; }
    }
}