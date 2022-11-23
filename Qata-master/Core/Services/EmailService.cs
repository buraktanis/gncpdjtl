using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public class EmailService
    {


        public void Send(string from, string to, string subject, string html)
        {
            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("aslan.pilis@gencpa.com", "Qaf00458");
            smtp.Send(email);
            smtp.Disconnect(true);
        }
        public void Sendlog(string subject, string html)
        {
            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("aslan.pilis@gencpa.com"));
            email.To.Add(MailboxAddress.Parse("aslan.pilis@gencpa.com"));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("aslan.pilis@gencpa.com", "Qaf00458");
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
