using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;

namespace CalendarApp.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void EnviarEmail(string destinatario, string assunto, string mensagem)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Calendário", _configuration["Smtp:Username"]));
            emailMessage.To.Add(new MailboxAddress("", destinatario));
            emailMessage.Subject = assunto;
            emailMessage.Body = new TextPart("plain") { Text = mensagem };

            using (var client = new SmtpClient())
            {
                client.Connect(_configuration["Smtp:Host"], int.Parse(_configuration["Smtp:Port"]), false);
                client.Authenticate(_configuration["Smtp:Username"], _configuration["Smtp:Password"]);
                client.Send(emailMessage);
                client.Disconnect(true);
            }
        }
    }
}
