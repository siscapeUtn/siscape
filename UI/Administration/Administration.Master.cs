using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI.Administration
{
    public partial class Administracion : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void showSecurity()
        {

            if (Session["User"] != null)
            {
                if (Session["Security"].Equals(true))
                {
                    Response.Write("<li><div class='option'><div><a href='../../Security/SecurityGroups/security.aspx' id='security' class='anchor'>Seguridad</a></div>" +
                                   "</div><ul class='sub-menu'>" +
                                   "<li><div><a href='../../Security/Security/role.aspx'>Roles</a></div></li>" +
                                   "<li><div><a href='../../Security/Security/user.aspx'>Usuarios</a></div></li>" +
                                   "</ul></li>");
                }
            }
        }
        
        protected void showAcademic()
        {
            if (Session["User"] != null)
            {
                if (Session["Academic"].Equals(true))
                {
                    Response.Write("<li><div class='option'>" +
                        "<div><a href='../Academic/AcademicGroups/academic.aspx' id='academic' class='anchor'>Acad&eacute;mico</a></div>" +
                        "</div><ul class='sub-menu'>" +
                        "<li><div><a href='../../Academic/AcademicGroups//gBuilding.aspx'>Infraestructura</a></div></li>" +
                        "<li><div><a href='../../Academic/AcademicGroups/gFunctionary.aspx'>Funcionarios</a></div></li>" +
                        "<li><div><a href='../../Academic/AcademicGroups/gAcademicOffer.aspx'>Oferta Acad&eacute;mica</a></div></li>" +
                        "</ul></li>");
                }
            }
        }
    }
}