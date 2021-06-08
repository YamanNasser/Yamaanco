using System;
using System.Collections.Generic;

namespace Yamaanco.Application.ApiResponses
{
    public class Response<T>
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> ErrorMessages { get; set; }
        public T Result { get; set; }

        public DateTime SentDate
        {
            get
            {
                return DateTime.Now;
            }
        }

        public Response()
        {
        }

        public Response(T result, string message = null)
        {
            Succeeded = true;
            Message = message;
            Result = result;
        }

        public Response(string message)
        {
            Succeeded = false;
            Message = message;
        }
    }
}