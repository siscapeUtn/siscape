using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI.Administration.Security
{
    public partial class User : System.Web.UI.Page
    {
        static Int32 resetUserSystem_id = -1;
        static Int32 userSystem_id = -1;
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
            startCombos();
            txtCode.Text = BLL.UserSystemBLL.getInstance().getNextCode().ToString();
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            Int32 records = -1;
            if (validateData())
            {
                Entities.UserSystem oUser = new Entities.UserSystem();
                Entities.Program oProgram = new Entities.Program();
                Entities.Role oRole = new Entities.Role();
                oUser.code = Convert.ToInt32(txtCode.Text);
                oUser.id = txtId.Text;
                oUser.name = txtName.Text;
                oUser.lastName = txtLastName.Text;
                oUser.homePhone = txtHomePhone.Text;
                oUser.cellPhone = txtCellPhone.Text;
                oUser.email = txtEmail.Text;
                oProgram.code = Convert.ToInt16(cboProgram.SelectedValue);
                oUser.Password = txtId.Text;
                oRole.Role_Id = Convert.ToInt16(cboRole.SelectedValue);
                oUser.oProgram = oProgram;
                oUser.oRole = oRole;
                oUser.state = Convert.ToInt16(cboState.SelectedValue);

                if (BLL.UserSystemBLL.getInstance().exists(oUser.code))
                {
                    records = BLL.UserSystemBLL.getInstance().modify(oUser);
                }
                else
                {
                    records = BLL.UserSystemBLL.getInstance().insert(oUser);
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
            Response.Redirect("../AdministrationGroups/gSecurity.aspx");
        }

        protected void loadData()
        {
            gvUserSystem.DataSource = BLL.UserSystemBLL.getInstance().getAll();
            gvUserSystem.DataBind();
        }

        protected void gvUserSystem_RowEditing(object sender, GridViewEditEventArgs e)
        {
            unlockControls();
            Int32 code = Convert.ToInt32(gvUserSystem.Rows[e.NewEditIndex].Cells[0].Text);
            Entities.UserSystem oUser = UserSystemBLL.getInstance().getUserSystem(code);
            getProgram();
            getRole();
            txtCode.Text = oUser.code.ToString();
            txtId.Text = oUser.id.ToString();
            txtName.Text = oUser.name.ToString();
            txtLastName.Text = oUser.lastName.ToString();
            txtHomePhone.Text = oUser.homePhone.ToString();
            txtCellPhone.Text = oUser.cellPhone.ToString();
            txtEmail.Text = oUser.email.ToString();
            cboProgram.SelectedValue = oUser.oProgram.code.ToString();
            cboRole.SelectedValue = oUser.oRole.Role_Id.ToString();
            try
            {
                cboState.SelectedValue = oUser.state.ToString();
            }
            catch (Exception)
            {
                cboState.SelectedValue = "1";
            }
        }

        protected void gvUserSystem_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            String UserName = gvUserSystem.Rows[e.RowIndex].Cells[1].Text;
            userSystem_id = Convert.ToInt32(gvUserSystem.Rows[e.RowIndex].Cells[0].Text);
            lblUserName.Text = UserName;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "confirmMessage", "$('#confirmMessage').modal();", true);
            confirmModal.Update();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Int32 records = UserSystemBLL.getInstance().delete(userSystem_id);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "closeConfirmMessage", "$('#confirmMessage').modal('toggle');", true);

            if (records > 0)
            {
                lblMessage.Text = "Usuario eliminado correctamente.";
            }
            loadData();
        }

        protected void gvUserSystem_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            String UserName = gvUserSystem.Rows[e.RowIndex].Cells[1].Text;
            resetUserSystem_id = Convert.ToInt32(gvUserSystem.Rows[e.RowIndex].Cells[0].Text);
            lblUserName.Text = UserName;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ResetPassword", "$('#ResetPassword').modal();", true);
            confirmModal.Update();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            //Int32 records = UserSystemBLL.getInstance().delete(userSystem_id);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "closeResetPassword", "$('#ResetPassword').modal('toggle');", true);

            //if (records > 0)
            //{
            //    lblMessage.Text = "Usuario eliminado correctamente.";
            //}
            loadData();
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

            if (txtName.Text.Trim() == "")
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

            if (txtLastName.Text.Trim() == "")
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

            if (txtEmail.Text.Trim() == "" || !txtEmail.Text.Contains("@"))
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

        private void startCombos()
        {
            getProgram();
            getRole();
        }

        public void getProgram()
        {
            cboProgram.Items.Clear();
            List<Entities.Program> listProgram = new List<Entities.Program>();
            listProgram = BLL.ProgramBLL.getInstance().getAllActived();
            ListItem oItemS = new ListItem("---- Seleccione ----", "0");
            cboProgram.Items.Add(oItemS);
            foreach (Entities.Program olistProgram in listProgram)
            {
                ListItem oItem = new ListItem(olistProgram.name, olistProgram.code.ToString());
                cboProgram.Items.Add(oItem);
            }
        }

        public void getRole()
        {
            cboRole.Items.Clear();
            List<Entities.Role> listRole = new List<Entities.Role>();
            listRole = BLL.RoleBLL.getInstance().getAll();
            ListItem oItemS = new ListItem("---- Seleccione ----", "0");
            cboRole.Items.Add(oItemS);
            foreach (Entities.Role olistRole in listRole)
            {
                ListItem oItem = new ListItem(olistRole.Description, olistRole.Role_Id.ToString());
                cboRole.Items.Add(oItem);
            }
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
            cboProgram.Enabled = false;
            cboRole.Enabled = false;
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
            cboProgram.Enabled = true;
            cboRole.Enabled = true;
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