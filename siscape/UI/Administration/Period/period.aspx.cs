using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace UI.Administration
{
    public partial class period : System.Web.UI.Page
    {
        static Int32 period_id = -1; 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadPeriodType();
                blockControls();
            }
            loadData();
        }

        protected void btnNew_Click(object sender, ImageClickEventArgs e)
        {
            unlockControls();
            txtCode.Text = PeriodBLL.getInstance().getNextCode().ToString();
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            Int32 records = 0;
            if (validateData())
            {
                Entities.Period oPeriod = new Entities.Period();
                Entities.PeriodType oPeriodType = new Entities.PeriodType();
                oPeriod.code = Convert.ToInt32(txtCode.Text);
                oPeriod.name = txtDescription.Text;
                oPeriod.startDate = DateTime.Parse(txtStartDate.Text);
                oPeriod.finalDate = DateTime.Parse(txtFinishDate.Text);
                oPeriodType.state = Convert.ToInt16(cboState.SelectedValue);
                oPeriodType.code = Convert.ToInt32(cboModality.SelectedValue);
                oPeriod.oPeriodType = oPeriodType;

                if (PeriodBLL.getInstance().exists(oPeriod.code))
                {
                    records = PeriodBLL.getInstance().modify(oPeriod);
                }
                else
                {
                    records = PeriodBLL.getInstance().insert(oPeriod);
                }
                blockControls();
                loadData();

                if (records > 0)
                {
                    lblMessage.Text = "Datos almacenados correactamente";
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "comboBox", "comboBox();", true);
            }
        }

        protected void btnCancel_Click(object sender, ImageClickEventArgs e)
        {
            blockControls();
        }

        protected void btnReturn_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("../AdministrationGroups/gPeriod.aspx");
        }

        protected void loadData()
        {
            gvPeriod.DataSource = PeriodBLL.getInstance().getAll();
            gvPeriod.DataBind();
        }
        
        protected void loadPeriodType()
        {
            List<Entities.PeriodType> listPeriodType = new List<Entities.PeriodType>();
            listPeriodType = PeriodTypeBLL.getInstance().getAllActived();
            ListItem oItemS = new ListItem("Seleccione", "0");
            cboModality.Items.Add(oItemS);
            foreach (Entities.PeriodType oPeriodType in listPeriodType)
            {
                ListItem oItem = new ListItem(oPeriodType.description , oPeriodType.code.ToString());
                cboModality.Items.Add(oItem);
            }
        }

        protected Boolean validateData()
        {
            Boolean ind = true;
            Boolean indDates = true;

            if( txtDescription.Text.Trim() == "" ){
                ind = false;
                lblMessageDescription.Text = "Debe digitar una descripción correcta.";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorName", "$('#ContentPlaceHolder1_txtDescription').addClass('has-error');", true);
            }else{
                lblMessageDescription.Text = "";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorName", "$('#ContentPlaceHolder1_txtDescription').removeClass('has-error');", true);
            }

            string cbo = cboModality.SelectedValue;
            if (Convert.ToInt32(cboModality.SelectedValue) == 0 || Convert.ToInt32(cboModality.SelectedValue) == -1)
            {
                ind = false;
                lblMessageModality.Text = "Debe seleccionar una modalidad.";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorModality", "$('#ContentPlaceHolder1_cboModality').addClass('has-error');", true);
            }else{
                lblMessageModality.Text = "";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorModality", "$('#ContentPlaceHolder1_cboModality').removeClass('has-error');", true);
            }
            
            try
            {
                DateTime startDate = Convert.ToDateTime(txtStartDate.Text);
                lblMessageStartDate.Text = "";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorStartDate", "$('#ContentPlaceHolder1_txtStartDate').removeClass('has-error');", true);
            }
            catch (Exception )
            {
                ind = false;
                indDates = false;
                lblMessageStartDate.Text = "Debe seleccionar una fecha correcta.";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorStartDate", "$('#ContentPlaceHolder1_txtStartDate').addClass('has-error');", true);
            }

            try 
	        {
                DateTime finishDate = Convert.ToDateTime(txtFinishDate.Text);
                lblMessageFinalDate.Text = "";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasFinishDate", "$('#ContentPlaceHolder1_txtFinishDate').removeClass('has-error');", true);
	        }
	        catch (Exception)
	        {
                ind = false;
                indDates = true;
                lblMessageFinalDate.Text = "Debe seleccionar una fecha correcta.";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorFinishDate", "$('#ContentPlaceHolder1_txtFinishDate').addClass('has-error');", true);
	        }

            if ( indDates )
            {
                DateTime startDate = Convert.ToDateTime(txtStartDate.Text);
                DateTime finishDate = Convert.ToDateTime(txtFinishDate.Text);

                if (startDate > finishDate)
                {
                    ind = false;
                    lblMessageFinalDate.Text = "Debe seleccionar posterior a la fecha de inicio.";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorFinishDate", "$('#ContentPlaceHolder1_txtFinishDate').addClass('has-error');", true);
                }
                else
                {
                    lblMessageFinalDate.Text = "";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasFinishDate", "$('#ContentPlaceHolder1_txtFinishDate').removeClass('has-error');", true);
                }
            }

            return ind;
        }

        protected void gvPeriod_RowEditing(object sender, GridViewEditEventArgs e)
        {
            unlockControls();
            Int32 code = Convert.ToInt32(gvPeriod.Rows[e.NewEditIndex].Cells[0].Text);
            Entities.Period oPeriod = PeriodBLL.getInstance().getPeriod(code);
            txtCode.Text = oPeriod.code.ToString();
            txtDescription.Text = oPeriod.name;
            cboModality.SelectedValue = oPeriod.oPeriodType.code.ToString();
            txtStartDate.Text = oPeriod.startDate.ToShortDateString();
            txtFinishDate.Text = oPeriod.finalDate.ToShortDateString();
            cboState.SelectedValue = oPeriod.state.ToString();
        }

        protected void gvPeriod_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            String periodDescription = gvPeriod.Rows[e.RowIndex].Cells[1].Text;
            period_id = Convert.ToInt32(gvPeriod.Rows[e.RowIndex].Cells[0].Text);
            lblPeriodDescription.Text = periodDescription;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirmMessage", "$('#confirmMessage').modal();", true);
            confirmModal.Update();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Int32 records = PeriodBLL.getInstance().delete(period_id);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirmMessage", "$('#confirmMessage').modal('toggle');", true);

            if (records > 0)
            {
                lblMessage.Text = "Tipo de periodo eliminado correctamente.";
            }
            loadData();
        }

        protected void blockControls()
        {
            txtCode.Enabled = false;
            txtDescription.Enabled = false;
            cboModality.Enabled = false;
            txtStartDate.Enabled = false;
            txtFinishDate.Enabled = false;
            cboState.Enabled = false;
            btnNew.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            clearControls();
        }

        protected void unlockControls()
        {
            txtCode.Enabled = true;
            txtDescription.Enabled = true;
            cboModality.Enabled = true;
            txtStartDate.Enabled = true;
            txtFinishDate.Enabled = true;
            cboState.Enabled = true;
            btnNew.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            clearControls();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "comboBox", "comboBox();", true);
        }

        protected void clearControls()
        {
            txtCode.Text = "";
            txtDescription.Text = "";
            txtStartDate.Text = "";
            txtFinishDate.Text = "";
            cboState.SelectedValue = "1";
            lblMessage.Text = "";
            cboModality.SelectedValue = "0";
            lblMessageDescription.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorName", "$('#ContentPlaceHolder1_txtDescription').removeClass('has-error');", true);
            lblMessageModality.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorModality", "$('#ContentPlaceHolder1_cboModality').removeClass('has-error');", true);
            lblMessageStartDate.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorStartDate", "$('#ContentPlaceHolder1_txtStartDate').removeClass('has-error');", true);
            lblMessageFinalDate.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorFinishDate", "$('#ContentPlaceHolder1_txtFinishDate').removeClass('has-error');", true);
        }
    }
}