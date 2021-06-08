using System;
using System.Collections.Generic;
using Yamaanco.Application.DTOs.Common;
using Yamaanco.Domain.Entities.ProfileEntities;

namespace Yamaanco.Application.DTOs.Group
{
    public class GroupViewersInfoDto
    {
        public string GroupId { get; set; }
        public string ViewerId { get; set; }
        public IEnumerable<PhotoResourcesDto> ProfilePhotoResources { get; set; }
        public string UserName { get; set; }
        public DateTime ViewedDate { get; set; }
        public Gender Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}