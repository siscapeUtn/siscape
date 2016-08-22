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

        
    }
}