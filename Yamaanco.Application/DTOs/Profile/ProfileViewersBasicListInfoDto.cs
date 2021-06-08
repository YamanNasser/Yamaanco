using System.Collections.Generic;
using Yamaanco.Application.DTOs.Common;
using Yamaanco.Application.Structs;
using Yamaanco.Domain.Entities.ProfileEntities;

namespace Yamaanco.Application.DTOs.Profile
{
    public class ProfileViewersBasicListInfoDto
    {
        public string ProfileId { get; set; }

        public string ViewerId { get; set; }
        public IEnumerable<PhotoResourcesDto> ProfilePhotoResources { get; set; }

        public string UserName { get; set; }

        public ElapsedTimeValue ElapsedTime { get; set; }

        public Gender Gender { get; set; }
    }
}