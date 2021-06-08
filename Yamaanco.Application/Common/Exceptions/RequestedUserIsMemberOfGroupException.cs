using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yamaanco.Application.Common.Exceptions
{
    public class RequestedUserIsMemberOfGroupException : Exception
    {
        public RequestedUserIsMemberOfGroupException(string userIdOrEmail, string groupId)
           : base($"The requested user \"{userIdOrEmail}\"  is already member of the  group {groupId}.")
        {
        }

    }
}