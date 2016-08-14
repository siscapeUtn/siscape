using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Entities
{
    public class Email
    {
        MailMessage m = new MailMessage();
        SmtpClient smtp = new SmtpClient();

        public bool correoContacto(String from, String body, String subject)
        {
            try
            {

                m.From = new MailAddress("siscape.utn@gmail.com");
                m.To.Add(new MailAddress(from));

                //Asunto
                m.Subject = subject;
                m.SubjectEncoding = System.Text.Encoding.UTF8;

                //Cuerpo del mensaje
                m.Body = body;

                //Correo electronico desde que queremos enviar el email
                m.Priority = MailPriority.Normal;
                /* Cliente del correo */
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.Credentials = new NetworkCredential("siscape.utn@gmail.com", "Siscape.utn2016");
                smtp.EnableSsl = true;
                smtp.Send(m);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }
    }
}