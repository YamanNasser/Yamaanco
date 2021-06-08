using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yamaanco.Application.DTOs.Account
{
    public class RegisterResponse
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
    }
}