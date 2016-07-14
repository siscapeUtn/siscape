using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI.Academic
{
    public partial class functionary : System.Web.UI.Page
    {
        static Int32 functionary_id = -1;
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
            txtCode.Text = FunctionaryBLL.getInstance().getNextCode().ToString();
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            Int32 records = -1;
            if (validateData())
            {
                Entities.Functionary oFunctionary = new Entities.Functionary();
                oFunctionary.code = Convert.ToInt32(txtCode.Text);
                oFunctionary.id = txtId.Text;
                oFunctionary.name = txtName.Text;
                oFunctionary.lastName = txtLastName.Text;
                oFunctionary.homePhone = txtHomePhone.Text;
                oFunctionary.cellPhone = txtCellPhone.Text;
                oFunctionary.email = txtEmail.Text;
                oFunctionary.state = Convert.ToInt16(cboState.SelectedValue);

                if (FunctionaryBLL.getInstance().exists(oFunctionary.code))
                {
                    records = FunctionaryBLL.getInstance().modify(oFunctionary);
                }
                else
                {
                    records = FunctionaryBLL.getInstance().insert(oFunctionary);
                }


                blockControls();
                loadData();
                if (records > 0)
                {
                    lblMessage.Text = "Datos almacenados correctamente.";
                }

                 //no c para que es esto
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "comboBox", "comboBox();", true);
                }
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

        protected void gvFunctionary_RowEditing(object sender, GridViewEditEventArgs e)
        {
            unlockControls();
            Int32 code = Convert.ToInt32(gvFunctionary.Rows[e.NewEditIndex].Cells[0].Text);
            Entities.Functionary oFunctionary = FunctionaryBLL.getInstance().getFunctionary(code);
            txtCode.Text = oFunctionary.code.ToString();
            txtId.Text = oFunctionary.id.ToString();
            txtName.Text = oFunctionary.name.ToString();
            txtLastName.Text = oFunctionary.lastName.ToString();
            txtHomePhone.Text = oFunctionary.homePhone.ToString();
            txtCellPhone.Text = oFunctionary.cellPhone.ToString();
            txtEmail.Text = oFunctionary.email.ToString();
            cboState.SelectedValue = oFunctionary.state.ToString();
        }

        protected void gvFunctionary_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            String nameFunctionary = gvFunctionary.Rows[e.RowIndex].Cells[1].Text + " " + gvFunctionary.Rows[e.RowIndex].Cells[2].Text;
            functionary_id = Convert.ToInt32(gvFunctionary.Rows[e.RowIndex].Cells[0].Text);
            lblFunctionaryDescription.Text = nameFunctionary;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirmMessage", "$('#confirmMessage').modal();", true);
            confirmModal.Update();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Int32 records = FunctionaryBLL.getInstance().delete(functionary_id);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirmMessage", "$('#confirmMessage').modal('toggle');", true);

            if (records > 0)
            {
                lblMessage.Text = "Funcionario eliminado correctamente.";
            }
            loadData();
        }

        protected void loadData()
        {
            gvFunctionary.DataSource = FunctionaryBLL.getInstance().getAll();
            gvFunctionary.DataBind();
        }

        protected Boolean validateData()
        {
            Boolean ind = true;

            try
            {
                Convert.ToInt32(txtId.Text);
                lblMessageId.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorId", "$('#ContentPlaceHolder1_txtId').removeClass('has-error');", true);
            }
            catch (Exception ex)
            {
                ind = false;
                lblMessageId.Text = "Debe digitar un número de identificación correcto.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "addHasErrorId", "$('#ContentPlaceHolder1_txtId').addClass('has-error');", true);
            }

            if( txtName.Text.Trim() == "" )
            {
                ind = false;
                lblMessageName.Text = "Debe digitar el nombre.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "addHasErrorName", "$('#ContentPlaceHolder1_txtName').addClass('has-error');", true);
            }
            else
            {
                lblMessageName.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorName", "$('#ContentPlaceHolder1_txtName').removeClass('has-error');", true);
            }

            if( txtLastName.Text.Trim() == "" )
            {
                ind = false;
                lblMessageLastName.Text = "Debe digitar los apellidos.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "addHasErrorLastName", "$('#ContentPlaceHolder1_txtLastName').addClass('has-error');", true);
            }
            else
            {
                lblMessageLastName.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorLastName", "$('#ContentPlaceHolder1_txtLastName').removeClass('has-error');", true);
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

            if( txtEmail.Text.Trim() == "" || !txtEmail.Text.Contains("@") )
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
    }
}