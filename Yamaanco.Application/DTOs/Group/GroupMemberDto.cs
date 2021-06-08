using System;
using System.Collections.Generic;
using Yamaanco.Application.DTOs.Common;
using Yamaanco.Domain.Entities.ProfileEntities;

namespace Yamaanco.Application.DTOs.Group
{
    public class GroupMemberDto
    {
        public string GroupId { get; set; }
        public string MemberId { get; set; }
        public string MemberName { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime JoinDate { get; set; }
        public bool IsCurrentUserFollowedTheMember { get; set; }
        public IEnumerable<PhotoResourcesDto> ProfilePhotoResources { get; set; }
        public Gender Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}