using System.Collections.Generic;
using Yamaanco.Application.DTOs.Common;
using Yamaanco.Domain.Entities.ProfileEntities;

namespace Yamaanco.Application.DTOs.Profile
{
    public class ProfileBasicSearchResultDto
    {
        public string Id { get; set; }
        public IEnumerable<PhotoResourcesDto> ProfilePhotoResources { get; set; }
        public Gender Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public int NumberOfFollowers { get; set; }
        public int NumberOfViewers { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsFollowedByloggedInUser { get; set; }
    }
}