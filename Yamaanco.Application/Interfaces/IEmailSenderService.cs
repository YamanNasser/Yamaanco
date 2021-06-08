using System.Collections.Generic;
using System.Threading.Tasks;

namespace Yamaanco.Application.Interfaces
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(string email, string subject, string message);

        Task SendEmailAsync(List<string> emailList, string subject, string message);
    }
}