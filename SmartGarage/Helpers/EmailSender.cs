using System.Net;
using System.Net.Mail;

namespace SmartGarage.Helpers
{   
  
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string reciever, string subject, string message)
        {
            var email = "sashengarage@gmail.com";
            var password = "tututu";

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(email, password)
            };

            var mailMessage = new MailMessage(from: email, to: reciever, subject, message)
            {
                IsBodyHtml = true 
            };

            return client.SendMailAsync(mailMessage);
        }
    }
}
