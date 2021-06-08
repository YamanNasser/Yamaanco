using System;
using Yamaanco.Domain.Entities.ProfileEntities;

namespace Yamaanco.Application.DTOs.Profile
{
    public class ProfileFollowerBasicInfoDto
    {
        public string FollowerId { get; set; }
        public string UserName { get; set; }
        public DateTime FollowedDate { get; set; }
        public Gender Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}