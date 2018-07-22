using MailKit.Net.Smtp;
using MimeKit;
using plan2plan.Services.Interfaces.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plan2plan.Infrastructure.Business.Email
{
    public class EmailWorker : IEmailWorker
    {
        private const string mail = "info@plan2plan.ru";
        private const string password = "picals3n2uz";
        private const string server = "smtp.masterhost.ru";
        private const int port = 465; //smtp.masterhost.ru порт 2525 или 25 (465 порт защищенного соединения) 

        public void SendEmail(string email, string title, string message)
        {
            MimeMessage emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта plan2plan.ru", mail));
            emailMessage.To.Add(new MailboxAddress(email));
            emailMessage.Subject = title;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                client.Connect(server, port, true);
                client.Authenticate(mail, password);
                client.Send(emailMessage);

                client.Disconnect(true);
            }
        }

        public async Task SendEmailAsync(string email, string title, string message)
        {
            MimeMessage emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта plan2plan.ru", mail));
            emailMessage.To.Add(new MailboxAddress(email));
            emailMessage.Subject = title;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(server, port, true);
                await client.AuthenticateAsync(mail, password);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
