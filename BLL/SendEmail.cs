using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace BLL
{
    public class SendEmail
    {
        public String email  {get;set;}
        private String password {get;set;}
        public String subjet { get; set; }
        public String message { get; set; }
        public String from { get; set; }

        public SendEmail() {
            this.email = "siscape.utn@gmail.com";
            this.password = "Siscape.utn2016";
        }

        public void send()
        {
            
            var smtp = new System.Net.Mail.SmtpClient();
            {
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential("siscape.utn@gmail.com", "Siscape.utn2016");
                smtp.Timeout = 20000;
            }
            smtp.Send(this.from, this.email, this.subjet, this.message);
           
        }
    }
}