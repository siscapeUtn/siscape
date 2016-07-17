using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI
{
    public partial class logOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
            singOut();
            }
            else
            {
                singOut();
            }
        }

        protected void singOut()
        {
            Session.Remove("User");
            Session.Remove("Administration");
            Session.Remove("Security");
            Session.Remove("Service");
            Session.Remove("Academic");
            Session.Remove("OfferAcademic");
            Response.Redirect("index.aspx");
        }
    }
}