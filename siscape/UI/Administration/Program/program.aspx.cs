using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI.Administration
{
    public partial class Program : System.Web.UI.Page
    {
        static Int32 program_id = -1;

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
            txtCode.Text = ProgramBLL.getInstance().getNextCode().ToString();
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            Int32 records = 0;
           
            if (validateData())
            {
                Entities.Program oProgram = new Entities.Program();
                oProgram.code = Convert.ToInt32(txtCode.Text);
                oProgram.name = txtName.Text.ToString();
                oProgram.unit = Convert.ToInt64(txtUnit.Text);
                oProgram.state = Convert.ToInt16(cboState.SelectedValue);

                if (ProgramBLL.getInstance().exists(oProgram.code)) //If the program exists in the database
                {
                    records = ProgramBLL.getInstance().modify(oProgram);//To modify the program
                }
                else
                {
                    records = ProgramBLL.getInstance().insert(oProgram);//To insert a program
                }
                blockControls();
                loadData();
                if (records > 0)
                {
                    lblMessage.Text = "Datos almacenados correctamente.";
                }
            }
        }

        protected void btnCancel_Click(object sender, ImageClickEventArgs e)
        {
            blockControls();
        }

        protected void btnReturn_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("../AdministrationGroups/gProgram.aspx");
        }

        /**
         * Method to get programs registered in the database and load in a gridView
        */
        protected void loadData()
        {
            gvProgram.DataSource = ProgramBLL.getInstance().getAll();
            gvProgram.DataBind();
        }

        protected void gvProgram_RowEditing(object sender, GridViewEditEventArgs e)
        {
            unlockControls();
            Int32 code = Convert.ToInt32(gvProgram.Rows[e.NewEditIndex].Cells[0].Text);
            Entities.Program oProgram = ProgramBLL.getInstance().getProgram(code);
            txtCode.Text = oProgram.code.ToString();
            txtName.Text = oProgram.name.ToString();
            txtUnit.Text = oProgram.unit.ToString();
            try
            {
                cboState.SelectedValue = oProgram.state.ToString();
            }
            catch (Exception)
            {
                cboState.SelectedValue = "1";
            }
        }

        protected void gvProgram_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            String programName = gvProgram.Rows[e.RowIndex].Cells[1].Text;
            program_id = Convert.ToInt32(gvProgram.Rows[e.RowIndex].Cells[0].Text);
            lblProgramName.Text = programName;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "confirmMessage", "$('#confirmMessage').modal();", true);
            confirmModal.Update();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Int32 records = ProgramBLL.getInstance().delete(program_id);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "closeConfirmMessage", "$('#confirmMessage').modal('toggle');", true);
            
            if (records > 0)
            {
                lblMessage.Text = "Programa eliminado correctamente.";
            }
            loadData();
        }

        /**
         * Method to validate the data information 
        */
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

            try
            {
                Convert.ToInt32(txtUnit.Text);
                lblUnitMessage.Text = "";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorUnit", "$('#ContentPlaceHolder1_txtUnit').removeClass('has-error');", true);
            }
            catch (Exception ex)
            {
                ind = false;
                lblUnitMessage.Text = "Debe digitar una unidad ejecutora correcta.";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorUnit", "$('#ContentPlaceHolder1_txtUnit').addClass('has-error');", true);
            }

            return ind;
        }

        /**
         * Method to block the controls 
        */
        public void blockControls()
        {
            clearControls();
            lblCode.Visible = false;
            txtCode.Visible = false;
            txtName.Enabled = false;
            txtUnit.Enabled = false;
            cboState.Enabled = false;
            btnCancel.Enabled = false;
            btnSave.Enabled = false;
            btnNew.Enabled = true;
        }//End blockControls()

        /**
         * Method to unlock the controls 
        */
        public void unlockControls()
        {
            clearControls();
            lblCode.Visible = false;
            txtCode.Visible = false;
            txtName.Enabled = true;
            txtUnit.Enabled = true;
            cboState.Enabled = true;
            btnCancel.Enabled = true;
            btnSave.Enabled = true;
            btnNew.Enabled = false;
        }//End unlockControls()

        /**
         * Method to clear the controls
        */
        public void clearControls()
        {
            txtCode.Text = "";
            txtName.Text = "";
            txtUnit.Text = "";
            cboState.SelectedValue = "1";
            lblMessage.Text = "";
            lblNameMessage.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorName", "$('#ContentPlaceHolder1_txtName').removeClass('has-error');", true);
            lblUnitMessage.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorUnit", "$('#ContentPlaceHolder1_txtName').removeClass('has-error');", true);
        }//End clearControls()
    }//End program
}