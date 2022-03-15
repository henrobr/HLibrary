using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace HLibrary
{
    public class Email
    {
        public string remetenteNome { get; set; }
        public string remetenteEmail { get; set; }
        public string remetenteSenha { get; set; }
        public string host { get; set; }
        public string destinatarioEmail { get; set; }
        public string destinatarioNome { get; set; }

        public string emailCopia = string.Empty;

        public string emailCopiaOculta = string.Empty;
        public string assunto { get; set; }
        public string conteudo { get; set; }
        public string anexo { get; set; }
        public bool sslEmail { get; set; }
        public bool html = true;
        public string titulo { get; set; }
        public int porta { get; set; }
        public string anexoCaminho { get; set; }
        public string anexoBase64 = string.Empty;
        public string anexoBase64NomeExt { get; set; }
        public string getMessage { get; set; }
        public List<Email> Copias { get; set; }
        public bool EnviarEmail()
        {

            //Cria objeto com dados do e-mail.
            MailMessage objEmail = new MailMessage();

            //Define o Campo From e ReplyTo do e-mail.
            objEmail.From = new MailAddress(remetenteEmail, remetenteNome);

            //Define os destinatários do e-mail.
            objEmail.To.Add(destinatarioEmail);

            //Enviar cópia para.
            /*if (emailCopia != string.Empty)
                objEmail.CC.Add(emailCopia);*/
            if(Copias != null)
            {
                //foreach(Email copia in Copias)
                //{
                //    objEmail.CC.Add(copia.emailCopia);
                //}
                for(int i=0; i < Copias.Count; i++)
                {
                    objEmail.CC.Add(Copias[i].emailCopia);
                }
            }

            //Enviar cópia oculta para.
            if (emailCopiaOculta != string.Empty)
                objEmail.Bcc.Add(emailCopiaOculta);/**/

            //Define a prioridade do e-mail.
            objEmail.Priority = System.Net.Mail.MailPriority.Normal;

            //Define o formato do e-mail HTML (caso não queira HTML alocar valor false)
            objEmail.IsBodyHtml = html;

            //Define título do e-mail.
            objEmail.Subject = assunto;

            //Define o corpo do e-mail.
            objEmail.Body = conteudo;


            //Para evitar problemas de caracteres "estranhos", configuramos o charset para "ISO-8859-1"
            objEmail.SubjectEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");

            /*if (anexoCaminho.Length > 0)
            {
                // Cria o anexo para o e-mail
                Attachment axo = new Attachment(anexoCaminho, System.Net.Mime.MediaTypeNames.Application.Octet);
                // Anexa o arquivo a mensagem
                objEmail.Attachments.Add(axo);
            }*/
            if (anexoBase64 != string.Empty)
            {
                MemoryStream strm = new MemoryStream(Convert.FromBase64String(anexoBase64));
                strm.Position = 0;
                var contentType = new ContentType(MediaTypeNames.Application.Pdf);
                Attachment data = new Attachment(strm, contentType);
                data.ContentDisposition.FileName = anexoBase64NomeExt;
                //data.ContentId = anexoBase64NomeExt;
                //data.ContentDisposition.Inline = true;
                objEmail.Attachments.Add(data);

            }


            //Cria objeto com os dados do SMTP

            SmtpClient objSmtp = new SmtpClient();
            objSmtp.UseDefaultCredentials = false;
            objSmtp.Credentials = new NetworkCredential(remetenteEmail, remetenteSenha);
            objSmtp.Host = host;//SMTP
            objSmtp.Port = porta;// portaEmail;
            objSmtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            objSmtp.EnableSsl = sslEmail;
            //Enviamos o e-mail através do método .send()
            try
            {
                //objSmtp.TargetName = "StartTLS/" + SMTP;
                objSmtp.Send(objEmail);
                return true;
            }
            catch (SmtpException ex)
            {
                getMessage = "Erro SmtpException: " + ex.Message;
                return false;
            }
            catch (Exception ex)
            {
                getMessage = "Erro: " + ex.Message;
                return false;
            }
            finally
            {
                //excluímos o objeto de e-mail da memória
                objEmail.Dispose();
                // anexo.Dispose();                
            }
        }
    }
}
