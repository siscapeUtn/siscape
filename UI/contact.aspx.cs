using BLL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI
{
    public partial class contact : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            String name = txtName.Text;
            String email = txtEmail.Text;
            String message = txtMessage.Text;
            String phone = txtPhone.Text;

            BLL.SendEmail oSendEmail = new BLL.SendEmail();
            oSendEmail.from = "siscape.utn@gmail.com";
            oSendEmail.subjet = "Contacto";
            oSendEmail.message = messageDesign(message, phone, name, email);
            oSendEmail.send();
        }

        private String messageDesign( String message, String phone, String name, String email)
        {
            String body = "";
            body += "Nombre: " + name + "\n";
            body += "Email: " + email + "\n";
            body += ( phone.Trim() != "" ? "Email: " + email + "\n" : "" ) ;
            body += "Se ha contactada por : " + message;
            return body;
        }
    }
}