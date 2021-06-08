using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Yamaanco.Domain.ValueObjects;

namespace Yamaanco.Application.DTOs.Account
{
    public class RegisterRequest
    {
        [Required]
        [StringLength(60, ErrorMessage = "Must be between 2 and 60 characters", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(60, ErrorMessage = "Must be between 2 and 60 characters", MinimumLength = 2)]
        public string LastName { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Phone]
        [Required]
        public string PhoneNumber { get; set; }

        public string UserName => (new UserName(FirstName, LastName)).ToString();

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}