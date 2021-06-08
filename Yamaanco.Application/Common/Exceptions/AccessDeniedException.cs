using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yamaanco.Application.Common.Exceptions
{
    public class AccessDeniedException : Exception
    {
        public AccessDeniedException(string name, object key)
             : base($"Access to Entity \"{name}\" ({key}) denied.")
        {
        }
    }
}