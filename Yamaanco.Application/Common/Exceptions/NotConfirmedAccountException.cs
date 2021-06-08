using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yamaanco.Application.Common.Exceptions
{
    public class NotConfirmedAccountException : Exception
    {
        public NotConfirmedAccountException(string email)
            : base($"Account Not Confirmed for '{email}'.")
        {
        }
    }
}
