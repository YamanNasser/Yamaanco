using System;
using System.Globalization;

namespace Yamaanco.Application.Common.Exceptions
{
    public class YamaancoException : Exception
    {
        public YamaancoException() : base()
        {
        }

        public YamaancoException(string message) : base(message)
        {
        }

        public YamaancoException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}