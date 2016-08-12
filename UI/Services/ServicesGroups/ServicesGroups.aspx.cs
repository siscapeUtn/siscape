using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI.Services.ServicesGroups
{
    public partial class ServicesGroups : System.Web.UI.Page
    {
        public bool service { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void showOService()
        {
            try
            {
                this.service = Convert.ToBoolean(Session["Service"].ToString());
            }
            catch
            {
                this.service = false;
            }

            if (this.service == false)
            {
                Response.Redirect("../../index.aspx");
            }
        }
    }
}