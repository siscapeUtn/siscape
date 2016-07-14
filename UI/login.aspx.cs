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
                
                    Entities.UserSystem oUser;
                    oUser = (Entities.UserSystem)BLL.UserSystemBLL.getInstance().verify_User(txtUser.Text, txtPassword.Text);

                    if (oUser.code == 0)
                    {
                        lblMessage.Text = "Nombre deusuario o Contraseña incorrectas";
                    }
                    else
                    {
                        Session["User"] = oUser;
                        addPermission(oUser.oRole.Role_Id);
                        getPeriod();
                        modalperiod();
                       
                    }
                
                
            }
            else
            {
                lblMessage.Text = "Debe agregar la información correctamente";
            }
            
        }

        private void modalperiod()
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirmMessage", "$('#confirmMessage').modal();", true);
            confirmModal.Update();
        }

        protected void btnPeriod_Click(object sender, EventArgs e)
        {
            
            Session["period"] = cboPeriod.SelectedValue;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirmMessage", "$('#confirmMessage').modal('toggle');", true);
            Response.Redirect("index.aspx");
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
                bandera = false;
            }
            if (txtPassword.Text == "")
            {
                bandera = false;
            }

            return bandera;
        }

        private void clearControls()
        {
            txtPassword.Text = "";
            txtUser.Text = "";
            //cboPeriod.SelectedValue = "0";
        }


    }
}