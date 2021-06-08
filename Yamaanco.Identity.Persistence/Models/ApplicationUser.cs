using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using Yamaanco.Application.DTOs.Account;

namespace Yamaanco.Infrastructure.EF.Identity.Persistence.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; }

        public bool OwnsToken(string token)
        {
            return RefreshTokens?.Find(x => x.Token == token) != null;
        }
    }
}