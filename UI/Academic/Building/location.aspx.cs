using BLL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI.Academic
{
    public partial class location : System.Web.UI.Page
    {
        static Int32 location_id = -1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if( !IsPostBack ){
                blockControls();
                loadHeadquarters();
            }
            loadData();
        }

        protected void btnNew_Click(object sender, ImageClickEventArgs e)
        {
            unlockControls();
            txtCode.Text = LocationBLL.getInstance().getNextCode().ToString();
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            Int32 records = 0;

            if (validateData())
            {
                Entities.Location oLocation = new Entities.Location();
                Entities.Headquarters oHeadquarters = new Entities.Headquarters();
                oLocation.code = Convert.ToInt32(txtCode.Text);
                oHeadquarters.code = Convert.ToInt32(cboHeadquarters.SelectedValue);
                oLocation.oHeadquarters = oHeadquarters;
                oLocation.building = txtBuilding.Text;
                oLocation.module = txtModule.Text;
                oLocation.State = Convert.ToInt16(cboState.SelectedValue);

                if (LocationBLL.getInstance().exists(oLocation.code))
                {
                    records = LocationBLL.getInstance().modify(oLocation);
                }
                else
                {
                    records = LocationBLL.getInstance().insert(oLocation);
                }

                blockControls();
                loadData();

                if (records > 0)
                {
                    lblMessage.Text = "Datos almacenados correctamente";
                }
            }
        }

        protected void btnCancel_Click(object sender, ImageClickEventArgs e)
        {
            blockControls();
        }

        protected void btnReturn_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("../AcademicGroups/gBuilding.aspx");
        }

        protected void loadData()
        {
            gvLocation.DataSource = LocationBLL.getInstance().getAll();
            gvLocation.DataBind();
        }

        protected Boolean validateData() 
        {
            Boolean ind = true;

            if( Convert.ToInt32(cboHeadquarters.SelectedValue) == 0 ){
                ind = false;
                lblMessageHeadquarters.Text =  "Debe seleccionar una sede.";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorHeadquarter", "$('#ContentPlaceHolder1_cboHeadquarters').addClass('has-error');", true);
            }else{
                lblMessageHeadquarters.Text =  "";
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

            return ind;
        }

        protected void loadHeadquarters()
        {
            List<Entities.Headquarters> listHeadquarters = new List<Entities.Headquarters>();
            listHeadquarters = HeadquartersBLL.getInstance().getAllActive();
            ListItem oItemS = new ListItem("---- Seleccione ----", "0");
            cboHeadquarters.Items.Add(oItemS);
            foreach (Entities.Headquarters oHeadquarters in listHeadquarters)
            {
                ListItem oItem = new ListItem(oHeadquarters.description, oHeadquarters.code.ToString());
                cboHeadquarters.Items.Add(oItem);
            }
        }

        protected void gvLocation_RowEditing(object sender, GridViewEditEventArgs e)
        {
            unlockControls();
            Int32 code = Convert.ToInt32(gvLocation.Rows[e.NewEditIndex].Cells[0].Text);
            Entities.Location oLocation = LocationBLL.getInstance().getLocation(code);
            txtCode.Text = oLocation.code.ToString();
            cboHeadquarters.SelectedValue = oLocation.oHeadquarters.code.ToString();
            txtBuilding.Text = oLocation.building;
            txtModule.Text = oLocation.module;
            cboState.SelectedValue = oLocation.State.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "$('html, body').animate({ scrollTop: $('body').offset().top });", true);
        }

        protected void gvLocation_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            String locationDescription = gvLocation.Rows[e.RowIndex].Cells[1].Text + " - " +
                 gvLocation.Rows[e.RowIndex].Cells[2].Text + " - " + gvLocation.Rows[e.RowIndex].Cells[3].Text;
            location_id = Convert.ToInt32(gvLocation.Rows[e.RowIndex].Cells[0].Text);
            lblLocationDescription.Text = locationDescription;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirmMessage", "$('#confirmMessage').modal();", true);
            confirmModal.Update();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Int32 records = LocationBLL.getInstance().delete(location_id);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirmMessage", "$('#confirmMessage').modal('toggle');", true);

            if (records > 0)
            {
                lblMessage.Text = "Localización eliminada correctamente.";
            }
            loadData();
        }

        protected void blockControls()
        {
            txtCode.Enabled = false;
            cboHeadquarters.Enabled = false;
            txtBuilding.Enabled = false;
            txtModule.Enabled = false;
            cboHeadquarters.Enabled = false;
            cboState.Enabled = false;
            btnNew.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            clearControls();
        }

        protected void unlockControls()
        {
            txtCode.Enabled = true;
            cboHeadquarters.Enabled = true;
            txtBuilding.Enabled = true;
            txtModule.Enabled = true;
            cboHeadquarters.Enabled = true;
            cboState.Enabled = true;
            btnNew.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            clearControls();
        }

        protected void clearControls()
        {
            txtCode.Text = "";
            txtBuilding.Text = "";
            txtModule.Text = "";
            cboHeadquarters.SelectedValue = "0";
            cboState.SelectedValue = "1";
            lblMessage.Text = "";
            lblMessageHeadquarters.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorName", "$('#ContentPlaceHolder1_cboHeadquarters').removeClass('has-error');", true);
            lblMessageBuilding.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorName", "$('#ContentPlaceHolder1_txtBuilding').removeClass('has-error');", true);
            lblMessageModule.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorName", "$('#ContentPlaceHolder1_txtModule').removeClass('has-error');", true);
        }
    }
}