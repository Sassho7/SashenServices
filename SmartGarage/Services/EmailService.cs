using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;

namespace SmartGarage.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmail(string to, string subject, string body)
        {
            var emailSettings = _configuration.GetSection("EmailSettings");
            var senderName = emailSettings["SenderName"];
            var senderEmail = emailSettings["SenderEmail"];

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(senderName, senderEmail));
            message.From.Add(new MailboxAddress(senderName, senderEmail));
            message.Subject = subject;
            message.Body = new TextPart("plain")
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(emailSettings["SmtpServer"], Convert.ToInt32(emailSettings["Port"]), false);
                    client.Authenticate(emailSettings["Username"], emailSettings["Password"]);
                    client.Send(message);
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                }
            }
        }
    }

    public interface IEmailService
    {
        void SendEmail(string to, string subject, string body);
    }
}
