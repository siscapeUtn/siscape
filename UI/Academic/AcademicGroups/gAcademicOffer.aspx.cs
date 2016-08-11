using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI.Academic.AcademicGroups
{
    public partial class gAcademicOffer : System.Web.UI.Page
    {
        public bool offerAcademic { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void showOfferAcademic()
        {
            this.offerAcademic = Convert.ToBoolean(Session["OfferAcademic"].ToString());
            if (this.offerAcademic == false)
            {
                Response.Redirect("../../index.aspx");
            }
        }
    }
}