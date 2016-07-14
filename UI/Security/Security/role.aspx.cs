using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI.Administration.Security
{
    public partial class Role : System.Web.UI.Page
    {
        static Int32 role_id = -1;
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
            txtCode.Text = RoleBLL.getInstance().getNextCode().ToString();
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            Int32 records = 0;
            if (validateData())
            {
                Entities.Role oRole = new Entities.Role();           
                oRole.Role_Id=Convert.ToInt32(txtCode.Text);
                oRole.Description=txtName.Text;
                oRole.State=Convert.ToInt32(cboState.SelectedValue);
                oRole.oListSystemModule=FillList();
                if (RoleBLL.getInstance().exists(oRole.Role_Id))
                {
                    records = RoleBLL.getInstance().modify(oRole);
                }
                else
                {
                    if (RoleBLL.getInstance().existsName(txtName.Text) == false)
                    {
                        records = RoleBLL.getInstance().insert(oRole);//To insert a role
                    }
                    else
                    {
                        lblMessage.Text = "Debe Utilizar otra descrpcion.";
                    }
                }
                blockControls();
                loadData();
                if (records > 0)
                {
                    lblMessage.Text = "Datos almacenados correctamente.";
                }
            }
        }

        private List<Entities.SystemModule> FillList()
        {
            List<Entities.SystemModule> oListModules=new List<Entities.SystemModule>();
            foreach (ListItem itemActual in chkModules.Items)
            {

                if (itemActual.Selected)
                {
                 Entities.SystemModule Module = new Entities.SystemModule();
                 Module.SystemModule_Id=Convert.ToInt32(itemActual.Value);
                 oListModules.Add(Module);  
                }

            }
            return oListModules;
        }


        protected void btnCancel_Click(object sender, ImageClickEventArgs e)
        {
            blockControls();
        }

        protected void btnReturn_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("../SecurityGroups/security.aspx");
        }

        private void loadData()
        {
            gvRole.DataSource = RoleBLL.getInstance().getAll();
            gvRole.DataBind();
        }

        private bool validateData()
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
            if (chkModules.SelectedItem == null)
            {
                ind = false;
                lblchkModulesMessage.Text = "Debe selecionar minimo un modulo.";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorchkModules", "$('#ContentPlaceHolder1_chkModules').addClass('has-error');", true);
            }
            else
            {
                lblNameMessage.Text = "";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorchkModules", "$('#ContentPlaceHolder1_chkModules').removeClass('has-error');", true);
            }

            return ind;
        }

        private void blockControls()
        {
            clearControls();
            lblCode.Visible = false;
            txtCode.Visible = false;
            txtName.Enabled = false;
            cboState.Enabled = false;
            chkModules.Enabled = false;
            btnCancel.Enabled = false;
            btnSave.Enabled = false;
            btnNew.Enabled = true;
        }

        private void clearControls()
        {
            txtCode.Text = "";
            txtName.Text = "";
            cboState.SelectedValue = "1";
            chkModules.ClearSelection();
            lblMessage.Text = "";
            lblNameMessage.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorName", "$('#ContentPlaceHolder1_txtName').removeClass('has-error');", true);
            lblchkModulesMessage.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorchkModules", "$('#ContentPlaceHolder1_chkModules').removeClass('has-error');", true);
        }

        public void unlockControls()
        {
            clearControls();
            lblCode.Visible = false;
            txtCode.Visible = false;
            txtName.Enabled = true;
            chkModules.Enabled = true;
            cboState.Enabled = true;
            btnCancel.Enabled = true;
            btnSave.Enabled = true;
            btnNew.Enabled = false;
        }

        protected void gvRole_RowEditing(object sender, GridViewEditEventArgs e)
        {
            unlockControls();
            Int32 code = Convert.ToInt32(gvRole.Rows[e.NewEditIndex].Cells[0].Text);
            Entities.Role oRole = RoleBLL.getInstance().getRole(code);
            txtCode.Text = oRole.Role_Id.ToString();
            txtName.Text = oRole.Description.ToString();
            selectModules(oRole.oListSystemModule);
            try
            {
                cboState.SelectedValue = oRole.State.ToString();
            }
            catch (Exception)
            {
                cboState.SelectedValue = "1";
            }
        }

        protected void gvRole_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            String roleName = gvRole.Rows[e.RowIndex].Cells[1].Text;
            role_id = Convert.ToInt32(gvRole.Rows[e.RowIndex].Cells[0].Text);
            lblRoleName.Text = roleName;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "confirmMessage", "$('#confirmMessage').modal();", true);
            confirmModal.Update();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Int32 records = RoleBLL.getInstance().delete(role_id);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "closeConfirmMessage", "$('#confirmMessage').modal('toggle');", true);

            if (records > 0)
            {
                lblMessage.Text = "Rol eliminado correctamente.";
            }
            loadData();
        }


        private void selectModules(List<Entities.SystemModule> olistModule)
        {

           foreach (ListItem itemActual in chkModules.Items)
            {
                for (int J = 0; J < olistModule.Count; J++)
                {
                    Entities.SystemModule oModule = olistModule[J];
                    if (itemActual.Value == oModule.SystemModule_Id.ToString())
                    {
                        itemActual.Selected = true;
                    }
                }


            }


        }



    }
}