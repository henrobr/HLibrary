using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace HLibrary
{
    public class EmailSendGrid
    {
        public string EmailFrom;
        public string EmailFromName;
        public string EmailTo;
        public string EmailToName;
        public string Subject;
        public string MessageText;
        public string MessageHtml;
        public string ApiKey;
        public EmailSendGrid()
        {
        }
        public EmailSendGrid(string apiKey, string emailFrom, string emailFromName, string emailTo, string emailToName, string emailSubject, string emailMessageText, string emailMessageHtml)
        {
            EmailFrom = emailFrom;
            EmailFromName = emailFromName;
            EmailTo = emailTo;
            EmailToName = emailToName;
            Subject = emailSubject;
            MessageText = emailMessageText;
            MessageHtml = emailMessageHtml;
            ApiKey = apiKey;
            EnviaEmailAsync().Wait();
        }
        public async Task EnviaEmailAsync()
        {
            try
            {
                var client = new SendGridClient(ApiKey);
                var from = new EmailAddress(EmailFrom, EmailFromName);
                var to = new EmailAddress(EmailTo, EmailToName);
                var msg = MailHelper.CreateSingleEmail(from, to, Subject, MessageText, MessageHtml);
                var response = await client.SendEmailAsync(msg);
            }
            catch (Exception e)
            {
                string texto = e.Message;
            }
        }
    }
}
