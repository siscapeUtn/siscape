﻿using System;
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

<<<<<<< HEAD
        //protected void Unnamed_Click(object sender, EventArgs e)
        //{
        //    if (Verify())
        //    {
        //        Entities.UserSystem oUser;
        //        oUser = (Entities.UserSystem)BLL.UserSystemBLL.getInstance().verify_User(txtUser.Text, txtPassword.Text);

        //        if (oUser.code == 0)
        //        {
        //            lblMessage.Text = "Nombre deusuario o Contraseña incorrectas";
        //        }
        //        else
        //        {
        //            Session["User"] = oUser;
        //            if (oUser.oUserType.code == 1)
        //            {
        //                Response.Redirect("Recorder/Recorder.aspx");
        //            }
        //            else
        //            {
        //                if (oUser.oUserType.code == 2)
        //                {
        //                    Response.Redirect("Recorder/Recorder.aspx");
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        lblMessage.Text = "Debe agregar la información correctamente";
        //    }

        //}

        protected void login_Click(object sender, EventArgs e)
        {
            Response.Redirect("index.aspx");
=======
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
                    Response.Redirect("index.aspx");
                }
            }
            else
            {
                lblMessage.Text = "Debe agregar la información correctamente";
            }
            
>>>>>>> 4a70333490eb2210730c461d83efeddea38f341f
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
            cboPeriod.SelectedValue = "0";
        }


    }
}