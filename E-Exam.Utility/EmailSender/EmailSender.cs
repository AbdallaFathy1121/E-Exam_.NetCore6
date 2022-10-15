using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace E_Exam.Utility.EmailSender
{
    public class EmailSender : IEmailSender
    {
        public void SendEmail(string email, string subject, string htmlMessage)
        {
            var fromMail = "Abdallafathy1121@outlook.com";
            var fromPassword = "Abdalla0159357";

            var message = new MailMessage();

            message.From = new MailAddress(fromMail);
            message.To.Add(email);
            message.Subject = subject;
            message.Body = $"<html><body> {htmlMessage}</body></html>";
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp-mail.outlook.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true
            };

            smtpClient.Send(message);
        }
    }
}
