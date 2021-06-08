using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yamaanco.Application.Common.Exceptions
{
    public class UserPhoneNumberAlreadyExistException : Exception
    {
        public UserPhoneNumberAlreadyExistException(string phoneNumber)
           : base($"The  user phoneNumber \"{phoneNumber}\"  is already exist.")
        {
        }

    }
}