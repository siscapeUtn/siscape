using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI
{
    public partial class login : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        
         protected void Unnamed_Click(object sender, EventArgs e)
        {
                              
       }

        protected void login_Click(object sender, EventArgs e)
        {

            if (Verify())
            {
               Entities.UserSystem oUser = (Entities.UserSystem)BLL.UserSystemBLL.getInstance().verify_User(txtUser.Text, txtPassword.Text);
                    
                if (oUser.code == 0)
                    {
                        lblMessage.Text = "Nombre deusuario o Contraseña incorrectas";
                    }
                    else
                    {
                        Session["User"] = oUser;
                        addPermission(oUser.oRole.Role_Id);
                    if (oUser.oProgram.code != 1)
                    {
                        getPeriod();
                        modalperiod();
                        if(oUser.setPassword=="0"){
                            modalPassword();
                        }
                    }
                    else { 
                    Response.Redirect("index.aspx");
                    }
                }
            }
            else
            {
                lblMessage.Text = "Debe agregar la información correctamente";
            }
            
        }

        private void modalPassword()
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "changePasswordModal", "$('#changePasswordModal').modal();", true);
            confirmModal.Update();
        }

        private void modalperiod()
        {
            lblMsj.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirmMessage", "$('#confirmMessage').modal();", true);
            confirmModal.Update();
        }

        protected void btnPeriod_Click(object sender, EventArgs e)
        {              
               if (cboPeriod.SelectedValue == "0")
                {
                lblMsj.Text = "Debe seleccionar un período";
                }
                else
                {
                Session["period"] = cboPeriod.SelectedValue;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirmMessage", "$('#confirmMessage').modal('toggle');", true);
                Response.Redirect("index.aspx");
                } 
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            cleanSession();
            Response.Redirect("index.aspx");
        }

        protected void cleanSession()
        {
            Session.RemoveAll();
        }

        //it's to fill in the cmbPeriod 
        public void getPeriod()
        {
            List<Entities.Period> listPeriod = new List<Entities.Period>();
            listPeriod = BLL.PeriodBLL.getInstance().getAllActive();
            ListItem oItemS = new ListItem("---- Seleccione ----", "0");
            cboPeriod.Items.Add(oItemS);
            foreach (Entities.Period olistPeriod in listPeriod)
            {
                ListItem oItem = new ListItem(olistPeriod.name + " " + olistPeriod.oPeriodType.description, olistPeriod.code.ToString());
                cboPeriod.Items.Add(oItem);
            }
        }

        private void addPermission(int roleId)
        {
            Session["Administration"] = false;
            Session["Security"] = false;
            Session["Service"] = false;
            Session["Academic"] = false;
            Session["OfferAcademic"] = false;

            List<SystemModule> listSystemModule = BLL.RoleBLL.getInstance().getRoleModuleByRole(roleId);
            for (int i = 0; i < listSystemModule.Count; i++)
            {
                if (listSystemModule[i].SystemModule_Id == 1)
                {
                    Session["Administration"] = true;
                }
                if (listSystemModule[i].SystemModule_Id == 2)
                {
                    Session["Security"] = true;
                }
                if (listSystemModule[i].SystemModule_Id == 3)
                {
                    Session["Service"] = true;
                }
                if (listSystemModule[i].SystemModule_Id == 4)
                {
                    Session["Academic"] = true;
                }
                if (listSystemModule[i].SystemModule_Id == 5)
                {
                    Session["OfferAcademic"] = true;
                }
            }    
        }

        private bool Verify()
        {
            bool bandera = true;
            if (txtUser.Text == "")
            {
                txtUser.Text = "siscape.utn@gmail.com";
                //bandera = false;
            }
            if (txtPassword.Text == "")
            {
               txtPassword.Text = "Siscape2016";
               //bandera = false;
            }

            return bandera;
        }

        private void clearControls()
        {
            txtPassword.Text = "";
            txtUser.Text = "";
            //cboPeriod.SelectedValue = "0";
        }

        protected void btnChange_Click1(object sender, EventArgs e)
        {
            Int32 records = -1;
            UserSystem oUser = (UserSystem)Session["User"];
            if (validatePassword(oUser.Password))
            {

                records = BLL.UserSystemBLL.getInstance().modifyPassword(oUser, txtNewPassword.Text);
                if (records > 0)
                {
                    oUser = (Entities.UserSystem)BLL.UserSystemBLL.getInstance().verify_User(oUser.email, txtNewPassword.Text);
                    Session["User"]=oUser;
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirmMessage", "$('#changePasswordModal').modal('toggle');", true);
            }
        }

        protected void cleanRestPassword()
        {
            lblMessageNewPassword.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorId", "$('#ContentPlaceHolder1_txtNewPassword').removeClass('has-error');", true);
            lblMessageConfirmPassword.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorId", "$('#ContentPlaceHolder1_txtConfirmPassword').removeClass('has-error');", true);
            lblMessageLastPassword.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorId", "$('#ContentPlaceHolder1_txtLastPassword').removeClass('has-error');", true);
        }

        private Boolean validatePassword(string lastpass)
        {
            cleanRestPassword();
            var ind = true;

            if (txtNewPassword.Text.Trim() == "")
            {
                ind = false;
                lblMessageNewPassword.Text = "Debe digitar un Nuevo Password.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "addHasErrorName", "$('#ContentPlaceHolder1_txtNewPassword').addClass('has-error');", true);
            }
            else { 
                if (txtNewPassword.Text.Trim().Length < 8)
                {
                    ind = false;
                    lblMessageNewPassword.Text = "Debe tener minimo 8 caracteres.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "addHasErrorName", "$('#ContentPlaceHolder1_txtNewPassword').addClass('has-error');", true);
                }
            }
            if (txtConfirmPassword.Text.Trim() == "" || txtConfirmPassword.Text != txtNewPassword.Text)
            {
                lblMessageConfirmPassword.Text = "Revise este campo.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "addHasErrorName", "$('#ContentPlaceHolder1_txtConfirmPassword').addClass('has-error');", true);
                ind = false;
            }
            if (txtLastPassword.Text.Trim() == "" || txtLastPassword.Text != lastpass)
            {
                lblMessageLastPassword.Text = "Debe digitar su password actual correctamente.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "addHasErrorName", "$('#ContentPlaceHolder1_txtLastPassword').addClass('has-error');", true);
                ind = false;
            }
            return ind;
        }

        
    }
}