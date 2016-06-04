using BLL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI.Academic.Functionary
{
    public partial class externalDesignation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAddExternal_Click(object sender, EventArgs e)
        {
            clearControls();
            loadDay();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "openCreateExternalDsignation", "$('#createDesignation').modal();", true);
            createModal.Update();
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "closeCreateExternalDsignation", "$('#createDesignation').modal('hide');", true);
        }

        protected void loadFunctionaries()
        {

        }

        protected void loadDay()
        {
            List<Entities.Day> listDays = new List<Entities.Day>();
            listDays = DayBLL.getInstance().getAll();
            ListItem oItemS = new ListItem("Seleccione", "0");
            cboDay.Items.Add(oItemS);
            foreach (Entities.Day oDay in listDays)
            {
                ListItem oItem = new ListItem(oDay.description, oDay.code.ToString());
                cboDay.Items.Add(oItem);
            }
        }

        protected void clearControls()
        {
            txtCode.Text = "";
            cboDay.Items.Clear();
            cboFunctionary.Items.Clear();
            txtPosition.Text = "";
            txtWorkPlace.Text = "";
            txtStartDesignation.Text = "";
            txtEndDesignation.Text = "";
            txtEnd.Text = "";
            txtStart.Text = "";
            txtHoursDisignation.Text = "";
        }
    }
}