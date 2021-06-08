using Yamaanco.Application.DTOs.Profile;

namespace Yamaanco.Application.Features.Profiles.ViewModel
{
    public class ProfileView
    {
        public ProfileDto Profile { get; set; }
        public bool IsFollowedByLoggedInUser { get; set; }
    }
}