using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using System.Threading.Tasks;

namespace ISouling.Component.SystemFramework
{
    public static class MessageHelper
    {
        public static async Task SendEmailAsync(string email, string subject, string message)
        {
            var mail = new MimeMessage();
            mail.From.Add(new MailboxAddress("ISouling", "noreply@isouling.com"));
            mail.To.Add(new MailboxAddress(email));
            mail.Subject = subject;

            mail.Body = new TextPart(TextFormat.Html) { Text = message };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.exmail.qq.com", 465, true);

                await client.AuthenticateAsync("noreply@isouling.com", "Q34/Fd34]nj");

                await client.SendAsync(mail);

                client.Disconnect(true);
            }
        }
    }
}