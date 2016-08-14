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
    public partial class classRoomType : System.Web.UI.Page
    {
        static Int32 classRoomType_id = -1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if( !IsPostBack ){
                blockControls();
            }
            loadData();
        }

        protected void btnNew_Click(object sender, ImageClickEventArgs e)
        {
            unlockControls();
            txtCode.Text = ClassRoomsTypeBLL.getInstance().getNextCode().ToString();
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            Int32 records = 0;

            if (validateData())
            {
                Entities.ClassRoomsType oClassRoomsType = new Entities.ClassRoomsType();
                oClassRoomsType.code = Convert.ToInt32(txtCode.Text);
                oClassRoomsType.description = txtDescription.Text;
                oClassRoomsType.state = Convert.ToInt16(cboState.SelectedValue);

                if (ClassRoomsTypeBLL.getInstance().exists(oClassRoomsType.code))
                {
                    records = ClassRoomsTypeBLL.getInstance().modify(oClassRoomsType);
                }
                else
                {
                    records = ClassRoomsTypeBLL.getInstance().insert(oClassRoomsType);
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
            gvClassRoomType.DataSource = ClassRoomsTypeBLL.getInstance().getAll();
            gvClassRoomType.DataBind();
        }

        protected Boolean validateData()
        {
            Boolean ind = true;

            if (txtDescription.Text.Trim() == "")
            {
                ind = false;
                lblMessageDescription.Text = "Debe digitar una descripción.";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorDescription", "$('#ContentPlaceHolder1_txtDescription').addClass('has-error');", true);
            }
            else
            {
                lblMessageDescription.Text = "";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorDescription", "$('#ContentPlaceHolder1_txtDescription').removeClass('has-error');", true);
            }

            return ind;
        }

        protected void gvClassRoomType_RowEditing(object sender, GridViewEditEventArgs e)
        {
            unlockControls();
            Int32 code = Convert.ToInt32(gvClassRoomType.Rows[e.NewEditIndex].Cells[0].Text);
            Entities.ClassRoomsType oClassRoomsType = ClassRoomsTypeBLL.getInstance().getClassRoomsType(code);
            txtCode.Text = oClassRoomsType.code.ToString();
            txtDescription.Text = oClassRoomsType.description;
            cboState.SelectedValue = oClassRoomsType.state.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "$('html, body').animate({ scrollTop: $('body').offset().top });", true);
        }

        protected void blockControls()
        {
            txtCode.Enabled = false;
            txtDescription.Enabled = false;
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
            cboState.Enabled = true;
            btnNew.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            clearControls();
        }

        protected void clearControls()
        {
            txtCode.Text = "";
            txtDescription.Text = "";
            cboState.SelectedValue = "1";
            lblMessage.Text = "";
            lblMessageDescription.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorDescription", "$('#ContentPlaceHolder1_txtDescription').removeClass('has-error');", true);
        }

        protected void gvClassRoomType_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            String classRoomTypeDescription = gvClassRoomType.Rows[e.RowIndex].Cells[1].Text;
            classRoomType_id = Convert.ToInt32(gvClassRoomType.Rows[e.RowIndex].Cells[0].Text);
            lblClassRoomTypeDescription.Text = classRoomTypeDescription;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirmMessage", "$('#confirmMessage').modal();", true);
            confirmModal.Update();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Int32 records = ClassRoomsTypeBLL.getInstance().delete(classRoomType_id);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirmMessage", "$('#confirmMessage').modal('toggle');", true);

            if (records > 0)
            {
                lblMessage.Text = "Tipo de aula eliminada correctamente eliminada correctamente.";
            }
            loadData();
        }

        protected void gvClassRoomType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvClassRoomType.PageIndex = e.NewPageIndex;
            loadData();
        }
    }
}