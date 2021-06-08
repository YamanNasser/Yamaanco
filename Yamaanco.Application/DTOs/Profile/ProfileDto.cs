using System;
using System.Collections.Generic;
using Yamaanco.Application.DTOs.Common;
using Yamaanco.Domain.Entities.ProfileEntities;

namespace Yamaanco.Application.DTOs.Profile
{
    public class ProfileDto
    {
        public string Id { get; set; }
        public IEnumerable<PhotoResourcesDto> ProfilePhotoResources { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AboutMe { get; set; }
        public Gender Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int NumberOfFollowers { get; set; }
        public int NumberOfViewers { get; set; }
        public DateTime CreatedDate { get; set; }
        public IEnumerable<string> FollowersId { get; set; }
    }
}