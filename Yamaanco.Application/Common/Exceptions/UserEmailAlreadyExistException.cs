using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yamaanco.Application.Common.Exceptions
{
    public class UserEmailAlreadyExistException : Exception
    {
        public UserEmailAlreadyExistException(string email)
           : base($"The  user email \"{email}\"  is already exist.")
        {
        }

    }
}