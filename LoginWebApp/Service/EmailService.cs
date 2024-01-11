using System;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using LoginWebApp.Models; // Asegúrate de tener esta referencia para acceder a EmailSend

namespace LoginWebApp.Service
{
    public static class EmailService
    {
        private static string _Host = "smtp.gmail.com";
        private static int _Puerto = 587;
        private static string _NameSend = "LoginApp";
        private static string _Email = "apistest1130@gmail.com";
        private static string _Password = "wqbqrmqsuikzvweh";

        public static bool Enviar(EmailSend emailSend)
        {
            try
            {
                var email = new MimeMessage();

                email.From.Add(new MailboxAddress(_NameSend, _Email));
                email.To.Add(MailboxAddress.Parse(emailSend.EmailTo));
                email.Subject = emailSend.Subject;
                email.Body = new TextPart(TextFormat.Html) { Text = emailSend.Report };

                using (var smtp = new SmtpClient())
                {
                    smtp.Connect(_Host, _Puerto, SecureSocketOptions.StartTls);
                    smtp.Authenticate(_Email, _Password);
                    smtp.Send(email);
                    smtp.Disconnect(true);
                }

                return true;
            }
            catch
            {
                
                return false;
            }
        }
    }
}
