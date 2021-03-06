﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI.Security
{
    public partial class Security : System.Web.UI.MasterPage
    {
        public bool security { get; set; }
        public bool administration { get; set; }
        public bool Academic { get; set; }
        public bool offerAcademic { get; set; }
        public bool services { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void validationUserExternal()
        {
            string header = "<li><div class='option'>";
            string offer = "<div><a href = '../../Services/Services/Services.aspx' class='anchor'>Oferta de Cursos</a></div>";
            string footer = "</div></li>";
            try
            {
                this.services = Convert.ToBoolean(Session["Service"].ToString());
            }
            catch
            {
                this.services = false;
            }

            if (this.services == false)
            {
                Response.Write(header + offer + footer);
            }
            else if (this.services == true)
            {
                Response.Write("<li><div class='option'><div><a href='../../Services/ServicesGroups/ServicesGroups.aspx' id='service' class='anchor'>Servicios</a></div>" +
                   "</div><ul class='sub-menu'>" +
                   "<li><div><a href='../../Services/Services/Services.aspx'>Oferta de Cursos</a></div></li>" +
                   "<li><div><a href='../../Services/Services/Report_waiting_list.aspx'>Reporte</a></div></li>" +
                   "</ul></li>");
            }
        }

        protected void validations()
        {
            if (Session["User"] != null)
            {
                this.security = Convert.ToBoolean(Session["Security"].ToString());
                this.administration = Convert.ToBoolean(Session["Administration"].ToString());
                this.Academic = Convert.ToBoolean(Session["Academic"].ToString());
                this.offerAcademic = Convert.ToBoolean(Session["OfferAcademic"].ToString());
                showSecurity();
                showAdministartion();
                showAcademic();
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
                Response.Write("<li><div class='option'><div><a href='../SecurityGroups/security.aspx' id='security' class='anchor'>Seguridad</a></div>" +
                                   "</div><ul class='sub-menu'>" +
                                   "<li><div><a href='../Security/role.aspx'>Roles</a></div></li>" +
                                   "<li><div><a href='../Security/user.aspx'>Usuarios</a></div></li>" +
                                   "</ul></li>");
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
                                    "<div><a href='../../Administration/AdministrationGroups/administration.aspx' id='administration' class='anchor'>Administraci&oacute;n</a></div>" +
                                    "</div><ul class='sub-menu'>" +
                                    "<li><div><a href='../../Administration/Program/program.aspx'>Programas</a></div></li>" +
                                    "<li><div><a href='../../Administration/AdministrationGroups/gPeriod.aspx'>Per&iacute;odo</a></div></li>" +
                                    "<li><div><a href='../../Administration/Slider/slider.aspx'>Banner</a></div></li>" +
                                    "</ul></li>");
                }
        }

        protected void showAcademic()
        {
            string head = "<li><div class='option'>" +
                        "<div><a href='../../Academic/AcademicGroups/academic.aspx' id='academic' class='anchor'>Acad&eacute;mico</a></div>" +
                        "</div><ul class='sub-menu'>";
            string administartionBody = "<li><div><a href='../../Academic/AcademicGroups/gBuilding.aspx'>Infraestructura</a></div></li>" +
                        "<li><div><a href='../../Academic/AcademicGroups/gFunctionary.aspx'>Funcionarios</a></div></li>";
            string offerBody = "<li><div><a href='../../Academic/AcademicGroups/gAcademicOffer.aspx'>Oferta Acad&eacute;mica</a></div></li>";
            string reportBody = "<li><div><a href='../../Academic/AcademicGroups/gReport.aspx'>Reportes</a></div></li>";
            string footer = "</ul></li>";
            if (this.Academic == true && this.offerAcademic == true)
            {
                Response.Write(head + administartionBody + offerBody + reportBody + footer);
            }
            else if (this.Academic == true)
            {
                Response.Write(head + administartionBody + reportBody + footer);
            }
            else if (this.offerAcademic == true)
            {
                Response.Write("<li><div class='option'>" +
                 "<div><a href='../../Academic/AcademicGroups/gAcademicOffer.aspx' id='academic' class='anchor'>Oferta Acad&eacute;mica</a></div>" +
                 "</div><ul class='sub-menu'>" + footer);
            }
        }

        protected void showSessionFooter()
        {
            if (Session["User"] != null)
            {
                Response.Write("<a href='../../logOut.aspx'>Cerrar Sesión</a>");
            }
            else
            {
                Response.Write("<a href='../../login.aspx'>Ingreso Interno</a>");

            }
        }

    }
}