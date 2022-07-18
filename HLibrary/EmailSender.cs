using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace HLibrary
{
    public class EmailSender : SenderEmail
    {
        
        public EmailSettings emailSettings { get; }
        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            this.emailSettings = emailSettings.Value;
        }
        public Task SendEmailAsync(string email, string subject, string message)
        {
            //try
            //{
                Execute(email, subject, message).Wait();
                return Task.FromResult(0);
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
        }

        public async Task Execute(string email, string subject, string message)
        {
            //try
            //{
                string toEmail = string.IsNullOrEmpty(email) ? emailSettings.ToEmail : email;

                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(emailSettings.UsernameEmail, emailSettings.Nome)
                };

                mail.To.Add(new MailAddress(toEmail));
                //mail.CC.Add(new MailAddress(emailSettings.CcEmail));

                mail.Subject = subject;
                mail.IsBodyHtml = true;
                mail.Body = message;
                mail.Priority = MailPriority.High;

                //outras opções
                //mail.Attachments.Add(new Attachment(arquivo));

                using (SmtpClient smtp = new SmtpClient(emailSettings.PrimaryDomain, emailSettings.PrimaryPort))
                {
                    smtp.Credentials = new NetworkCredential(emailSettings.UsernameEmail, emailSettings.UsernamePassword);
                    smtp.UseDefaultCredentials = false;
                    smtp.ServicePoint.MaxIdleTime = 1;
                    smtp.EnableSsl = emailSettings.Ssl;
                    await smtp.SendMailAsync(mail);
                    mail.Dispose();
                }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }
        //static void NEVER_EAT_POISON_Disable_CertificateValidation()
        //{
        //    // Disabling certificate validation can expose you to a man-in-the-middle attack
        //    // which may allow your encrypted message to be read by an attacker
        //    // https://stackoverflow.com/a/14907718/740639
        //    ServicePointManager.ServerCertificateValidationCallback =
        //        delegate (
        //            object s,
        //            X509Certificate certificate,
        //            X509Chain chain,
        //            SslPolicyErrors sslPolicyErrors
        //        ) {
        //            return true;
        //        };
        //}
    }

    public class EmailSettings
    {
        public String PrimaryDomain { get; set; }
        public int PrimaryPort { get; set; }
        public String UsernameEmail { get; set; }
        public String UsernamePassword { get; set; }
        public String FromEmail { get; set; }
        public String ToEmail { get; set; }
        public String CcEmail { get; set; }
        public String Nome { get; set; }
        public Boolean Ssl { get; set; }
    }
    public interface SenderEmail
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
