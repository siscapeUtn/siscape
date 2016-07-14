using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace UI.Administration
{
    public partial class PeriodType : System.Web.UI.Page
    {
        static Int32 periodType_id = -1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                blockControls();
            }
            loadData();
        }

        protected void btnNew_Click(object sender, ImageClickEventArgs e)
        {
            unlockControls();
            txtCode.Text = PeriodTypeBLL.getInstance().getNextCode().ToString();
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            Int32 records = 0;
            if (validateData())
            {
                Entities.PeriodType oPeriodType = new Entities.PeriodType();
                oPeriodType.code = Convert.ToInt32(txtCode.Text);
                oPeriodType.description = txtName.Text;
                oPeriodType.state = Convert.ToInt16(cboState.SelectedValue);
                if (PeriodTypeBLL.getInstance().exists(oPeriodType.code))
                {
                    records = PeriodTypeBLL.getInstance().modify(oPeriodType);
                }
                else
                {
                    records = PeriodTypeBLL.getInstance().insert(oPeriodType);
                }
                blockControls();
                loadData();

                if (records > 0)
                {
                    lblMessage.Text = "Datos almacenados correactamente";
                }
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

        protected void gvPeriodType_RowEditing(object sender, GridViewEditEventArgs e)
        {
            unlockControls();
            Int32 code = Convert.ToInt32(gvPeriodType.Rows[e.NewEditIndex].Cells[0].Text);
            Entities.PeriodType oPeriodType = PeriodTypeBLL.getInstance().getPeriod(code);
            txtCode.Text = oPeriodType.code.ToString();
            txtName.Text = oPeriodType.description.ToString();
            try
            {
                cboState.SelectedValue = oPeriodType.state.ToString();
            }
            catch (Exception)
            {
                cboState.SelectedValue = "1";
            }
        }

        protected void gvPeriodType_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            String periodTypeDescription = gvPeriodType.Rows[e.RowIndex].Cells[1].Text;
            periodType_id = Convert.ToInt32(gvPeriodType.Rows[e.RowIndex].Cells[0].Text);
            lblPeriodTypeDescription.Text = periodTypeDescription;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirmMessage", "$('#confirmMessage').modal();", true);
            confirmModal.Update();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Int32 records = PeriodTypeBLL.getInstance().delete(periodType_id);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirmMessage", "$('#confirmMessage').modal('toggle');", true);

            if (records > 0)
            {
                lblMessage.Text = "Tipo de periodo eliminado correctamente.";
            }
            loadData();
        }

        protected Boolean validateData()
        {
            Boolean ind = true;

            if (txtName.Text.Trim() == "")
            {
                ind = false;
                lblNameMessage.Text = "Debe digitar una descripción correcta.";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorName", "$('#ContentPlaceHolder1_txtName').addClass('has-error');", true);
            }
            else
            {
                lblNameMessage.Text = "";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorName", "$('#ContentPlaceHolder1_txtName').removeClass('has-error');", true);
            }

            return ind;
        }

        /**
         * Method to get period types registered in the database and to load in a GridView
        */ 
        protected void loadData()
        {
            gvPeriodType.DataSource = PeriodTypeBLL.getInstance().getAll();
            gvPeriodType.DataBind();
        } //End loadData()

        /** 
         * Method to block the fields
        */
        protected void blockControls()
        {
            clearControls();
            txtName.Enabled = false;
            cboState.Enabled = false;
            btnNew.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
        } //End blockControls()

        /**
         * Method to unlock the form fields
        */
        protected void unlockControls()
        {
            clearControls();
            txtName.Enabled = true;
            cboState.Enabled = true;
            btnNew.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
        } //End unlockControls()

        /**
         * Method to clear the form fields 
        */
        protected void clearControls()
        {
            txtCode.Text = "";
            txtName.Text = "";
            cboState.SelectedValue = "1";
            lblMessage.Text = "";
            lblNameMessage.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorName", "$('#ContentPlaceHolder1_txtName').removeClass('has-error');", true);
        }//End clearControls()
    } //End periodType
}