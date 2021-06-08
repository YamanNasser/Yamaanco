﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yamaanco.Application.Common.Exceptions
{
    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException(string value)
           : base($"Invalid Credentials for {value}.")
        {
        }

    }
}