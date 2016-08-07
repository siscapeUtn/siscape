using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI.Academic.AcademicOffer
{
    public partial class schedule : System.Web.UI.Page
    {

        static Int32 schedule_id = -1;
        private static string codDays = "";

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
            txtCode.Text = ScheduleBLL.getInstance().getNextCode().ToString();
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            Int32 records = -1;
            if (validateData())
            {
                Entities.Schedule oSchedule = new Entities.Schedule();
                oSchedule.code = Convert.ToInt32(txtCode.Text);
                oSchedule.name = txtDescription.Text;
                oSchedule.typeSchedule = cboTypeSchedule.SelectedValue;
                oSchedule.startTime = Convert.ToDateTime(txtStart.Text);
                oSchedule.endTime = Convert.ToDateTime(txtEndHour.Text);
                oSchedule.state = Convert.ToInt32(cboState.SelectedValue);
                oSchedule.oProgram.code = 1;
                oSchedule.codday = codDays;

                if (ScheduleBLL.getInstance().exists(oSchedule.code))
                {
                    records = ScheduleBLL.getInstance().modify(oSchedule);
                }
                else
                {
                    records = ScheduleBLL.getInstance().insert(oSchedule);
                }

                blockControls();
                loadData();

                if (records > 0)
                {
                    lblMessage.Text = "Datos almacenados correctamente.";
                }
            }
            //no c para que es esto
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
            Response.Redirect("../AcademicGroups/gAcademicOffer.aspx");
        }

        protected void loadData()
        {
            gvSchedule.DataSource = ScheduleBLL.getInstance().getAll();
            gvSchedule.DataBind();
        }

        protected Boolean validateData()
        {
           Boolean ind = true;
            /* 
                        if (Convert.ToInt32(cboHeadquarters.SelectedValue) == 0)
                        {
                            ind = false;
                            lblMessageHeadquarters.Text = "Debe seleccionar una sede.";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorHeadquarter", "$('#ContentPlaceHolder1_cboHeadquarters').addClass('has-error');", true);
                        }
                        else
                        {
                            lblMessageHeadquarters.Text = "";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorHeadquarter", "$('#ContentPlaceHolder1_cboHeadquarters').removeClass('has-error');", true);
                        }

                        if (txtBuilding.Text.Trim() == "")
                        {
                            ind = false;
                            lblMessageBuilding.Text = "Debe digitar un nombre de edificio.";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorBuilding", "$('#ContentPlaceHolder1_txtBuilding').addClass('has-error');", true);
                        }
                        else
                        {
                            lblMessageBuilding.Text = "";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorBuilding", "$('#ContentPlaceHolder1_txtBuilding').removeClass('has-error');", true);
                        }

                        if (txtModule.Text.Trim() == "")
                        {
                            ind = false;
                            lblMessageModule.Text = "Debe digitar el nombre de un modulo.";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorModule", "$('#ContentPlaceHolder1_txtModule').addClass('has-error');", true);
                        }
                        else
                        {
                            lblMessageModule.Text = "";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorModule", "$('#ContentPlaceHolder1_txtModule').removeClass('has-error');", true);
                        }

                      */
           return ind;
        }

        protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
        {
           String days = "";
            codDays = "";
            foreach (ListItem itemActual in chkld.Items)
            {

                if (itemActual.Selected)
                {
                    days = days + itemActual.Text;
                    codDays = codDays + itemActual.Value;
                    days = days + ",";
                }

            }
            txtDescription.Text = days;
        }



        protected void gvSchedule_RowEditing(object sender, GridViewEditEventArgs e)
        {
            unlockControls();
            Int32 code = Convert.ToInt32(gvSchedule.Rows[e.NewEditIndex].Cells[0].Text);
            Entities.Schedule oSchedule = ScheduleBLL.getInstance().getSchedule(code);
            txtCode.Text = oSchedule.code.ToString();
            txtDescription.Text = oSchedule.name;
            cboTypeSchedule.SelectedValue = oSchedule.typeSchedule;
            txtStart.Text = String.Format("{0:t}", oSchedule.startTime);
            txtEndHour.Text = String.Format("{0:t}", oSchedule.endTime);
            cboState.SelectedValue = oSchedule.state.ToString();
            SelectchkDays(oSchedule.codday);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "$('html, body').animate({ scrollTop: $('body').offset().top });", true);
        }

        protected void gvSchedule_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
           String locationDescription = gvSchedule.Rows[e.RowIndex].Cells[1].Text + " - " +
            gvSchedule.Rows[e.RowIndex].Cells[2].Text;
            schedule_id = Convert.ToInt32(gvSchedule.Rows[e.RowIndex].Cells[0].Text);
            lblScheduleDescription.Text = locationDescription;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirmMessage", "$('#confirmMessage').modal();", true);
            confirmModal.Update();
       }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Int32 records = ScheduleBLL.getInstance().delete(schedule_id);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirmMessage", "$('#confirmMessage').modal('toggle');", true);

            if (records > 0)
            {
                lblMessage.Text = "Horario eliminado correctamente.";
            }
            loadData();
        }

        protected void blockControls()
        {
            chkld.Enabled = false;
            txtCode.Enabled = false;
            txtDescription.Enabled = false;
            cboTypeSchedule.Enabled = false;
            cboState.Enabled = false;
            txtStart.Enabled = false;
            txtEndHour.Enabled = false;
            btnNew.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            clearControls();
        }

        protected void unlockControls()
        {
            txtCode.Enabled = false;
            chkld.Enabled = true;
            txtDescription.Enabled = false;
            txtStart.Enabled = true;
            txtEndHour.Enabled = true;
            cboTypeSchedule.Enabled = true;
            cboState.Enabled = true;
            btnNew.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            clearControls();
        }

        protected void clearControls()
        {
            codDays = "";
            txtCode.Text = "";
            txtDescription.Text = "";
            txtStart.Text = "";
            txtEndHour.Text = "";
            cboState.SelectedValue = "1";
            lblMessage.Text = "";
            cleanUpchkDays();
            cboTypeSchedule.SelectedValue = "Mañana";
            lblMessageDescription.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorName", "$('#ContentPlaceHolder1_txtDescription').removeClass('has-error');", true);
            lblMessageTypeSchedule.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorName", "$('#ContentPlaceHolder1_cboTypeSchedule').removeClass('has-error');", true);
            lblMessageStartHour.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorName", "$('#ContentPlaceHolder1_txtStart').removeClass('has-error');", true);
            lblMessageEndHour.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorName", "$('#ContentPlaceHolder1_txtEndHour').removeClass('has-error');", true);
        }

        //This method work for clean up the list of checkbox
        private void cleanUpchkDays()
        {
            foreach (ListItem itemActual in chkld.Items)
            {
                itemActual.Selected = false;
            }
        }

        private void SelectchkDays(string oCodDays)
        {
            int days = oCodDays.Trim().Length;
            int[] array=selectDays(oCodDays);
            int i=1;
            int j = 1;
            foreach (ListItem itemActual in chkld.Items)
            {
                
                if (j == array[i - 1])
                {
                    itemActual.Selected = true;
                    i++;
                }
                j++;
            }
        }

        private int[] selectDays(string days)
        {
            int[] save = new int[7];
            int aument = 0;
            while (6 >= aument)
            {
                if (days.Trim().Length <= aument)
                {
                    save[aument] = 9;
                }
                else
                {
                    string output = days.Substring(aument, 1);
                    save[aument] = Convert.ToInt32(output);
                }
                aument++;
            }

            return save;
        }

    }
}