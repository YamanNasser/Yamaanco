using System.Collections.Generic;
using Yamaanco.Domain.ValueObjects;

namespace Yamaanco.Application.DTOs.Authentication
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Id { get; set; }
        public string UserName => (new UserName(FirstName, LastName)).ToString();
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string IP { get; set; }
        public List<string> Roles { get; set; }
    }
}