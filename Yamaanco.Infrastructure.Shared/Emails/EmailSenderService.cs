using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Yamaanco.Application.Common.Options;
using Yamaanco.Application.Interfaces;

namespace Yamaanco.Infrastructure.Shared.Emails
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly EmailOptions _emailSettings;

        public EmailSenderService(EmailOptions emailSettings)
        {
            _emailSettings = emailSettings;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                string userName = _emailSettings.UserName;
                string passwrod = _emailSettings.Password;
                var mailMessage = new MailMessage(userName, email, subject, message)
                {
                    IsBodyHtml = _emailSettings.IsBodyHtml
                };

                var networkCredential = new NetworkCredential(userName, passwrod);
                var mailClient = new SmtpClient
                {
                    EnableSsl = _emailSettings.EnableSsl,
                    Host = _emailSettings.SmtpServer,
                    Port = _emailSettings.Port,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = _emailSettings.UseDefaultCredentials,
                    Credentials = new NetworkCredential(userName, passwrod)
                };

                mailClient.Send(mailMessage);
            }
            catch (Exception)
            {
                throw;
            }
            return Task.CompletedTask;
        }

        public Task SendEmailAsync(List<string> emailList, string subject, string message)
        {
            try
            {
                string userName = _emailSettings.UserName;
                string passwrod = _emailSettings.Password;
                var combined = string.Join(",", emailList);

                var mailMessage = new MailMessage(userName, combined, subject, message)
                {
                    IsBodyHtml = _emailSettings.IsBodyHtml
                };

                var mailClient = new SmtpClient
                {
                    EnableSsl = _emailSettings.EnableSsl,
                    Host = _emailSettings.SmtpServer,
                    Port = _emailSettings.Port,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = _emailSettings.UseDefaultCredentials,
                    Credentials = new NetworkCredential(userName, passwrod)
                };

                mailClient.Send(mailMessage);
            }
            catch (Exception) { throw; }
            return Task.CompletedTask;
        }
    }
}