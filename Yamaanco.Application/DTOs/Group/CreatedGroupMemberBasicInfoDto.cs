using System;

namespace Yamaanco.Application.DTOs.Group
{
    public class CreatedGroupMemberBasicInfoDto
    {
        public string MemberId { get; set; }
        public string MemberProfileMediumPhotoPath { get; set; }
        public string MemberName { get; set; }
        public string GroupName { get; set; }
        public string GroupId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime JoinDate { get; set; }
        public int NumberOfGroupMembers { get; set; }
    }
}