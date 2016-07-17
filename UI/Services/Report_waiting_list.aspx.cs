using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.ComponentModel;

namespace UI.Services
{
    public partial class Report_waiting_list : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillGridView();
                fillFilter();

            }
        }

        private void fillGridView()
        {
            DataTable oDataTable = new DataTable();
            oDataTable = BLL.WaitingListBLL.getInstance().getAllCostumers();
            gvCustomers.DataSource = oDataTable;
            gvCustomers.DataBind();
        }

        private void fillGridViewContacted()
        {
            DataTable oDataTable = new DataTable();
            oDataTable = BLL.WaitingListBLL.getInstance().getAllCostumersContacted();
            gvCustomers.DataSource = oDataTable;
            gvCustomers.DataBind();
        }

        private void fillGridViewByCourse()
        {
            DataTable oDataTable = new DataTable();
            oDataTable = BLL.WaitingListBLL.getInstance().getAllCostumersByCourse();
            gvCustomers.DataSource = oDataTable;
            gvCustomers.DataBind();
        }

        protected void fillFilter()
        {
            ListItem oItem = new ListItem("No Contactado", "1");
            cboFilter.Items.Add(oItem);
            ListItem oItem1 = new ListItem("Contactado", "2");
            cboFilter.Items.Add(oItem1);
            ListItem oItem2 = new ListItem("Curso", "3");
            cboFilter.Items.Add(oItem2);
        }

        protected void cboFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFilter.SelectedItem.Value.ToString() == "1")
            {
                fillGridView();
            }
            if (cboFilter.SelectedItem.Value.ToString() == "2")
            {
                fillGridViewContacted();
            }
            if (cboFilter.SelectedItem.Value.ToString() == "3")
            {
                fillGridViewByCourse();
            }
        }

        protected void Btn_Update(object sender, EventArgs e)
        {
            Int32 code = 0;
            // String id = "";
            Boolean contacted = false;

            foreach (GridViewRow row in gvCustomers.Rows)
            {
                code = Convert.ToInt32(row.Cells[0].Text);
                //   id = row.Cells[1].Text;
                CheckBox chk = row.FindControl("contacted") as CheckBox;
                if (chk.Checked)
                {
                    contacted = true;
                }
                else
                {
                    contacted = false;
                }
                BLL.WaitingListBLL.getInstance().UpdateWaitingListContacted(code, contacted);
            }
            Response.Redirect("Report_waiting_list.aspx");
        }
    }
}