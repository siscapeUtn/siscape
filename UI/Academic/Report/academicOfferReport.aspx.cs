using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI.Academic.Report
{
    public partial class academicOfferReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            getPeriod();
        }

        public void getPeriod()
        {
            List<Entities.Period> listPeriod = new List<Entities.Period>();
            listPeriod = BLL.PeriodBLL.getInstance().getAll();
            ListItem oItemS = new ListItem("---- Seleccione ----", "0");
            cboPeriod.Items.Add(oItemS);
            foreach (Entities.Period olistPeriod in listPeriod)
            {
                ListItem oItem = new ListItem(olistPeriod.name + " " + olistPeriod.oPeriodType.description, olistPeriod.code.ToString());
                cboPeriod.Items.Add(oItem);
            }
        }

        protected void btnReport_Click(object sender, ImageClickEventArgs e)
        {
            Int32 period_id = Convert.ToInt32(cboPeriod.SelectedValue);
        }
    }
}