using System.Collections.Generic;

namespace Yamaanco.Application.DTOs.SystemNotifications
{
    public class MultipleEmails
    {
        public List<string> To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}