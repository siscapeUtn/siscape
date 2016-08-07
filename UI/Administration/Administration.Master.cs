﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI.Administration
{
    public partial class Administracion : System.Web.UI.MasterPage
    {
        public bool security { get; set; }
        public bool administration { get; set; }
        public bool Academics { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void validations()
        {
            if (Session["User"] != null)
            {
                this.security = Convert.ToBoolean(Session["Security"].ToString());
                this.administration = Convert.ToBoolean(Session["Administration"].ToString());
                this.Academics = Convert.ToBoolean(Session["Academic"].ToString());
                showSecurity();
                showAdministartion();
                showAcademic();
            }
            else
            {
                Response.Redirect("../../index.aspx");
            }
        }

        protected void showAdministartion()
        {
            if (this.administration == true)
            {
                Response.Write("<li><div class='option'>" +
                                "<div><a href='../AdministrationGroups/administration.aspx' id='administration' class='anchor'>Administraci&oacute;n</a></div>" +
                                "</div><ul class='sub-menu'>" +
                                "<li><div><a href='../AdministrationGroups/gProgram.aspx'>Programas</a></div></li>" +
                                "<li><div><a href='../AdministrationGroups/gPeriod.aspx'>Per&iacute;odo</a></div></li>" +
                                "<li><div><a href='../Slider/slider.aspx'>Banner</a></div></li>" +
                                "</ul></li>");
            }
            else
            {
                Response.Redirect("../../index.aspx");
            }
        }

        protected void showSecurity()
        {
                if (this.security == true)
                {
                    Response.Write("<li><div class='option'><div><a href='../../Security/SecurityGroups/security.aspx' id='security' class='anchor'>Seguridad</a></div>" +
                                   "</div><ul class='sub-menu'>" +
                                   "<li><div><a href='../../Security/Security/role.aspx'>Roles</a></div></li>" +
                                   "<li><div><a href='../../Security/Security/user.aspx'>Usuarios</a></div></li>" +
                                   "</ul></li>");
                }
        }
        
        protected void showAcademic()
        {
                if (this.Academics == true)
                {
                    Response.Write("<li><div class='option'>" +
                        "<div><a href='../../Academic/AcademicGroups/academic.aspx' id='academic' class='anchor'>Acad&eacute;mico</a></div>" +
                        "</div><ul class='sub-menu'>" +
                        "<li><div><a href='../../Academic/AcademicGroups/gBuilding.aspx'>Infraestructura</a></div></li>" +
                        "<li><div><a href='../../Academic/AcademicGroups/gFunctionary.aspx'>Funcionarios</a></div></li>" +
                        "<li><div><a href='../../Academic/AcademicGroups/gAcademicOffer.aspx'>Oferta Acad&eacute;mica</a></div></li>" +
                        "</ul></li>");
                }
        }
    }
}