namespace Yamaanco.Application.Common.Options
{
    public class EmailOptions
    {
        public string SmtpServer { get; set; } = "smtp.gmail.com";
        public int Port { get; set; } = 587;
        public bool EnableSsl { get; set; } = true;
        public bool UseDefaultCredentials { get; set; } = false;

        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsBodyHtml { get; set; } = true;
    }
}