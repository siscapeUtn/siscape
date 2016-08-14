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
            if (!IsPostBack)
            {
                clearControls();
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {

            if (validateData())
            {
                String name = txtName.Text;
                String email = txtEmail.Text;
                String message = txtMessage.Text;
                String phone = txtPhone.Text;
                String body = this.messageDesign(message, phone, name, email);
                String subject = "Contactenos";

                Entities.Email oEmail = new Entities.Email();

                if (oEmail.correoContacto("siscape.utn@gmail.com", body, subject))
                {
                    clearControls();
                    lblMessageSuccess.Text = "Mensaje enviado correctamente.";
                    lblMessageSuccess.CssClass = "successEmail";
                }
                else
                {
                    lblMessageSuccess.Text = "Hubo un error a la hora de enviar el mensaje.";
                    lblMessageSuccess.CssClass = "errorEmail";
                }
            }
        }

        private String messageDesign( String message, String phone, String name, String email)
        {
            String body = "";
            body += "Nombre: " + name + "\n";
            body += "Email: " + email + "\n";
            body += ( phone.Trim() != "" ? "Teléfono: " + phone + "\n" : "" ) ;
            body += "Se ha contactado por : " + message;
            return body;
        }

        private void clearControls()
        {
            lblMessageSuccess.Text = "";
            lblMessageSuccess.CssClass = "";
            txtEmail.Text = "";
            txtMessage.Text = "";
            txtName.Text = "";
            txtPhone.Text = "";
            lblMessageName.Text = "";
            lblMessageEmail.Text = "";
            lblMessageMessage.Text = "";
        }

        private Boolean validateData()
        {
            Boolean ind = true;

            if( txtName.Text.Trim() == "" ){
                ind = false;
                txtName.CssClass = "form-control col-sm-8 has-error";
                lblMessageName.Text = "Debe digitar el nombre";
            } else{
                lblMessageName.Text = "";
                txtName.CssClass = txtName.CssClass.Replace("has-error", "");
            }

            if( txtEmail.Text.Trim() == "" ){
                ind = false;
                lblMessageEmail.Text = "Debe digitar un correo electrónico";
                txtEmail.CssClass = "form-control col-sm-8 has-error";
            }
            else
            {
                if ( ! txtEmail.Text.Contains("@") ){
                    ind = false;
                    lblMessageEmail.Text = "Debe digitar un correo electrónico valido.";
                    txtEmail.CssClass = "form-control col-sm-8 has-error";
                }
                else
                {
                    lblMessageEmail.Text = "";
                    txtEmail.CssClass = txtEmail.CssClass.Replace("has-error", "");
                }
            }

            if (txtMessage.Text.Trim() == "" )
            {
                ind = false;
                lblMessageMessage.Text = "Debe digitar el mensaje.";
                txtMessage.CssClass = "form-control col-sm-8 has-error";
            }
            else
            {
                lblMessageMessage.Text = "";
                txtMessage.CssClass = txtMessage.CssClass.Replace("has-error", "");
            }

            return ind;
        }
    }
}