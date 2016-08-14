using BLL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI.Academic
{
    public partial class teacher : System.Web.UI.Page
    {
        static Int32 teacher_id = -1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                blockControls();
                loadModality();
            }
            loadData();
        }

        protected void btnNew_Click(object sender, ImageClickEventArgs e)
        {
            unlockControls();
            txtCode.Text = TeacherBLL.getInstance().getNextCode().ToString();
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            Int32 records = -1;
            if( validateData() ){
                Entities.Teacher oTeacher = new Entities.Teacher();
                Entities.InternalDesignation oInternalDesignation = new Entities.InternalDesignation();
                oTeacher.code = Convert.ToInt32(txtCode.Text);
                oTeacher.id = txtId.Text;
                oTeacher.name = txtName.Text;
                oTeacher.lastName = txtLastName.Text;
                oInternalDesignation.code = Convert.ToInt32(cboModality.SelectedValue);
                oTeacher.homePhone = txtHomePhone.Text;
                oTeacher.cellPhone = txtCellPhone.Text;
                oTeacher.email = txtEmail.Text;
                oTeacher.state = Convert.ToInt16(cboState.SelectedValue);
                oTeacher.Position = oInternalDesignation;

                if (TeacherBLL.getInstance().exists(oTeacher.code))
                {
                    records = TeacherBLL.getInstance().modify(oTeacher);
                }
                else
                {
                    records = TeacherBLL.getInstance().insert(oTeacher);
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
            Response.Redirect("../AcademicGroups/gFunctionary.aspx");
        }

        protected void gvTeacher_RowEditing(object sender, GridViewEditEventArgs e)
        {
            unlockControls();
            Int32 code = Convert.ToInt32(gvTeacher.Rows[e.NewEditIndex].Cells[0].Text);
            Entities.Teacher oTeacher = TeacherBLL.getInstance().getTeacher(code);
            txtCode.Text = oTeacher.code.ToString();
            txtId.Text = oTeacher.id.ToString();
            txtName.Text = oTeacher.name.ToString();
            txtLastName.Text = oTeacher.lastName.ToString();
            txtHomePhone.Text = oTeacher.homePhone.ToString();
            txtCellPhone.Text = oTeacher.cellPhone.ToString();
            txtEmail.Text = oTeacher.email.ToString();
            cboModality.SelectedValue = oTeacher.Position.code.ToString();
            cboState.SelectedValue = oTeacher.state.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "$('html, body').animate({ scrollTop: $('body').offset().top });", true);
        }

        protected void gvTeacher_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            String nameTeacher = gvTeacher.Rows[e.RowIndex].Cells[1].Text + " " + gvTeacher.Rows[e.RowIndex].Cells[2].Text;
            teacher_id = Convert.ToInt32(gvTeacher.Rows[e.RowIndex].Cells[0].Text);
            lblTeacherDescription.Text = nameTeacher;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirmMessage", "$('#confirmMessage').modal();", true);
            confirmModal.Update();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Int32 records = TeacherBLL.getInstance().delete(teacher_id);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirmMessage", "$('#confirmMessage').modal('toggle');", true);

            if (records > 0)
            {
                lblMessage.Text = "Profesor eliminado correctamente.";
            }
            loadData();
        }

        //Method to load the gridview with the all teachers registered into the dabatabase
        protected void loadData()
        {
            gvTeacher.DataSource = TeacherBLL.getInstance().getAll();
            gvTeacher.DataBind();
        }

        //Method to load the cboModality with the modalities registered into the database
        protected void loadModality()
        {
            List<Entities.InternalDesignation> listInternalDesignation = new List<Entities.InternalDesignation>();
            listInternalDesignation = InternalDesignationBLL.getInstance().getAllActive();
            ListItem oItemS = new ListItem("---- Seleccione ----", "0");
            cboModality.Items.Add(oItemS);
            foreach (Entities.InternalDesignation oInternalDesignation in listInternalDesignation)
            {
                ListItem oItem = new ListItem(oInternalDesignation.description, oInternalDesignation.code.ToString());
                cboModality.Items.Add(oItem);
            }
        }

        protected Boolean validateData()
        {
            Boolean ind = true;
            
            if (txtId.Text.Trim() == "")
            {
                ind = false;
                lblMessageId.Text = "Debe digitar un número de identifiación correcto.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "addHasErrorId", "$('#ContentPlaceHolder1_txtId').addClass('has-error');", true);
            }
            else
            {
                lblMessageId.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorId", "$('#ContentPlaceHolder1_txtId').removeClass('has-error');", true);
            }

            if (txtName.Text.Trim() == "")
            {
                ind = false;
                lblMessageName.Text = "Debe digitar un nombre correcto.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "addHasErrorName", "$('#ContentPlaceHolder1_txtName').addClass('has-error');", true);
            }
            else
            {
                lblMessageName.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorName", "$('#ContentPlaceHolder1_txtName').removeClass('has-error');", true);
            }

            if (txtLastName.Text.Trim() == "")
            {
                ind = false;
                lblMessageLastName.Text = "Debe digitar apellidos correcto";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "addHasErrorLastName", "$('#ContentPlaceHolder1_txtLastName').addClass('has-error');", true);
            }
            else
            {
                lblMessageLastName.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorLastName", "$('#ContentPlaceHolder1_txtLastName').removeClass('has-error');", true);
            }

            if (cboModality.SelectedValue == "0")
            {
                ind = false;
                lblMessageModality.Text = "Debe seleccionar una modalidad.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "addHasErrorModality", "$('#ContentPlaceHolder1_cboModality').addClass('has-error');", true);
            }
            else
            {
                lblMessageModality.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorModality", "$('#ContentPlaceHolder1_cboModality').removeClass('has-error');", true);
            }

            try
            {
                Convert.ToInt32(txtHomePhone.Text);
                lblMessageHomePhone.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorHomePhone", "$('#ContentPlaceHolder1_txtHomePhone').removeClass('has-error');", true);
            }
            catch (Exception ex)
            {
                ind = false;
                lblMessageHomePhone.Text = "Debe digitar un número de teléfono residencial correcto.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "addHasErrorHomePhone", "$('#ContentPlaceHolder1_txtHomePhone').addClass('has-error');", true);
            }

            try
            {
                Convert.ToInt32(txtCellPhone.Text);
                lblMessageCellPhone.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorCellPhone", "$('#ContentPlaceHolder1_txtCellPhone').removeClass('has-error');", true);
            }
            catch (Exception ex)
            {
                ind = false;
                lblMessageCellPhone.Text = "Debe digitar un número de teléfono celular correcto.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "addHasErrorCellPhone", "$('#ContentPlaceHolder1_txtCellPhone').addClass('has-error');", true);
            }

            if (txtEmail.Text.Trim() == "" || !txtEmail.Text.Contains('@'))
            {
                ind = false;
                lblMessageEmail.Text = "Debe digitar un correo electrónico correcto.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "addHasErrorEmail", "$('#ContentPlaceHolder1_txtEmail').addClass('has-error');", true);
            }
            else
            {
                lblMessageEmail.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorEmail", "$('#ContentPlaceHolder1_txtEmail').removeClass('has-error');", true);
            }

            return ind;
        }

        protected void blockControls()
        {
            txtCode.Enabled = false;
            txtId.Enabled = false;
            txtName.Enabled = false;
            txtLastName.Enabled = false;
            cboModality.Enabled = false;
            txtHomePhone.Enabled = false;
            txtCellPhone.Enabled = false;
            txtEmail.Enabled = false;
            cboState.Enabled = false;
            btnNew.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            clearControls();
        }

        protected void unlockControls()
        {
            txtCode.Enabled = true;
            txtId.Enabled = true;
            txtName.Enabled = true;
            txtLastName.Enabled = true;
            cboModality.Enabled = true;
            txtHomePhone.Enabled = true;
            txtCellPhone.Enabled = true;
            txtEmail.Enabled = true;
            cboState.Enabled = true;
            btnNew.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            clearControls();
        }

        protected void clearControls()
        {
            txtCode.Text = "";
            txtId.Text = "";
            lblMessageId.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorId", "$('#ContentPlaceHolder1_txtId').removeClass('has-error');", true);
            txtName.Text = "";
            lblMessageName.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorName", "$('#ContentPlaceHolder1_txtName').removeClass('has-error');", true);
            txtLastName.Text = "";
            lblMessageLastName.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorLastName", "$('#ContentPlaceHolder1_txtLastName').removeClass('has-error');", true);
            cboModality.SelectedValue = "0";
            lblMessageModality.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorModality", "$('#ContentPlaceHolder1_cboModality').removeClass('has-error');", true);
            txtHomePhone.Text = "";
            lblMessageHomePhone.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorHomePhone", "$('#ContentPlaceHolder1_txtHomePhone').removeClass('has-error');", true);
            txtCellPhone.Text = "";
            lblMessageCellPhone.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorCellPhone", "$('#ContentPlaceHolder1_txtCellPhone').removeClass('has-error');", true);
            txtEmail.Text = "";
            lblMessageEmail.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorEmail", "$('#ContentPlaceHolder1_txtEmail').removeClass('has-error');", true);
            cboState.SelectedValue = "1";
        }

        protected void gvTeacher_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTeacher.PageIndex = e.NewPageIndex;
            loadData();
        }
    }
}