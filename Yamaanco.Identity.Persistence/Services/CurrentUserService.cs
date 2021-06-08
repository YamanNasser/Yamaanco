using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Yamaanco.Application.Interfaces;

namespace Yamaanco.Infrastructure.EF.Identity.Persistence.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            IsAuthenticated = UserId != null;
        }

        public string UserId { get; }

        public bool IsAuthenticated { get; }
    }
}