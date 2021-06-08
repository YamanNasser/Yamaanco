using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yamaanco.Application.Common.Exceptions
{
    public class UserNameAlreadyExistException : Exception
    {
        public UserNameAlreadyExistException(string userName)
           : base($"The  user name \"{userName}\"  is already exist.")
        {
        }

    }
}