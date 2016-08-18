using Entities;
using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace UI.Services.Services
{
    public partial class Services : System.Web.UI.Page
    {
        public List<Program> listProgram = new List<Program>();

        public List<Course> listCourse = new List<Course>();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        //protected void programs()
        //{
        //    //This line brings the list of programs to show them.
        //    listProgram = BLL.ProgramBLL.getInstance().getAll();

        //    foreach (Entities.Program program in listProgram)
        //    {
        //        Response.Write("<h3 class='subtitle'>" + program.name + "</h3>");

        //        //This line brings the list of courses by program to show them.
        //        listCourse = BLL.CourseBLL.getInstance().getAllActivedCourseProgram(program.code);

        //        foreach (Entities.Course course in listCourse)
        //        {
        //            Response.Write("<div class='listCourse'>" +
        //            "<section class='course'>" +
        //            "<form id='form1' action='Waiting_list.aspx' runat='server'>" +
        //            "<div class='name'>" + course.description + "</div>" +
        //            "<>" +
        //            "<asp:Button runat='server' ID='btnInteresado' CssClass='btnService' Text='Estoy interesado' onClick='submit'/>" +
        //            "</form></section></div>");
        //        }
        //    }
        //}
    }
}