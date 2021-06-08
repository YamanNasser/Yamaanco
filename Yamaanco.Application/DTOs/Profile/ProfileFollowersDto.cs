using System;
using System.Collections.Generic;
using Yamaanco.Application.DTOs.Common;
using Yamaanco.Domain.Entities.ProfileEntities;

namespace Yamaanco.Application.DTOs.Profile
{
    public class ProfileFollowersDto
    {
        public string ProfileId { get; set; }
        public string FollowerId { get; set; }
        public bool IsProfileOwnerFollowedTheFollower { get; set; }
        public IEnumerable<PhotoResourcesDto> ProfilePhotoResources { get; set; }
        public string UserName { get; set; }
        public bool IsSeen { get; set; }
        public DateTime FollowedDate { get; set; }
        public Gender Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}