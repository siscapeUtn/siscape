using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI.Academic
{
    public partial class academic : System.Web.UI.Page
    {
        public bool offerAcademic { get; set; }
        public bool Academics { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
        protected void showOAcademic()
        {
            try
            {
                this.Academics = Convert.ToBoolean(Session["Academic"].ToString());
            }
            catch
            {
                this.Academics = false;
            }
            if (this.Academics == false)
            {
                Response.Redirect("../../index.aspx");
            }
        }

        protected void showOfferAcademic()
        {
            this.offerAcademic = Convert.ToBoolean(Session["OfferAcademic"].ToString());
            this.Academics = Convert.ToBoolean(Session["Academic"].ToString());
            if (this.offerAcademic == true)
            {
                Response.Write("<section class='col-md-4 col-sm-4 col-xs-12 accesses-container'>"+
                                 "<section class='accesses'>" +
                                 "<a href ='gAcademicOffer.aspx'><img alt='Oferta acad&eacute;mica' src='../../images/academic/offerAcedemic.svg' />" +
                                 "<p>Oferta acad&eacute;mica</p></a>" +
                                 "</section></section>");
            }
            else if(this.Academics == false)
            {
                Response.Redirect("../../index.aspx");
            }
        }
    }
}