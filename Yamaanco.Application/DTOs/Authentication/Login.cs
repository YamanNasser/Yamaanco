using System.ComponentModel.DataAnnotations;

namespace Yamaanco.Application.DTOs.Authentication
{
    public class Login
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}