using BLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI.Administration.Slider
{
    public partial class slider : System.Web.UI.Page
    {
        static Int32 slider_id = -1;
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
            txtCode.Text = SliderBLL.getInstance().getNextCode().ToString();
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            Int32 records = 0;
                       
            if (this.validateData())
            {
                int iLen = flLoadImage.PostedFile.ContentLength;
                byte[] btArr = new byte[iLen];
                flLoadImage.PostedFile.InputStream.Read(btArr, 0, iLen);
                String base64String = Convert.ToBase64String(btArr);
                String imageURL = "data:image/png;base64," + base64String;

                Entities.Slider oSlider = new Entities.Slider();
                oSlider.code = Convert.ToInt32(txtCode.Text);
                oSlider.description = txtName.Text;
                oSlider.image = imageURL;
                oSlider.state = Convert.ToInt16(cboState.SelectedValue);

                if (SliderBLL.getInstance().exists(oSlider.code)) //If the program exists in the database
                {
                    records = SliderBLL.getInstance().modify(oSlider);//To modify the program
                }
                else
                {
                    records = SliderBLL.getInstance().insert(oSlider);//To insert a program
                }

                if (records > 0)
                {
                    lblMessage.Text = "Datos almacenados correctamente.";
                }

            }
            loadData();
            blockControls();
        }

        protected void btnCancel_Click(object sender, ImageClickEventArgs e)
        {
            blockControls();
        }

        protected void btnReturn_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("../AdministrationGroups/administration.aspx");
        }
        
        private Boolean validateData()
        {
            Boolean ind = true;

            if (txtName.Text.Trim() == "")
            {
                ind = false;
                lblNameMessage.Text = "Debe ingresar una descripción correcta.";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorName", "$('#ContentPlaceHolder1_txtName').addClass('has-error');", true);
            }
            else
            {
                lblNameMessage.Text = "";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorName", "$('#ContentPlaceHolder1_txtName').removeClass('has-error');", true);
            }

            if (flLoadImage.HasFile)
            {
                String fileExt = System.IO.Path.GetExtension(flLoadImage.FileName);

                if( fileExt.ToLower() == ".png" ||
                    fileExt.ToLower() == ".jpg" ||
                    fileExt.ToLower() == "jpeg" )
                {
                    lblImageError.Text = "";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorImage", "$('#ContentPlaceHolder1_uploadFile').removeClass('has-error');", true);
                }
                else
                {
                    ind = false;
                    lblImageError.Text = "Debe subir una imagen.";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorImage", "$('#ContentPlaceHolder1_uploadFile').addClass('has-error');", true);
                }
            }
            else
            {
                ind = false;
                lblImageError.Text = "Debe subir una imagen.";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorImage", "$('#ContentPlaceHolder1_uploadFile').addClass('has-error');", true);
            }

            return ind;
        }

        protected void loadData()
        {
            gvSlider.DataSource = SliderBLL.getInstance().getAll();
            gvSlider.DataBind();
        }

        private void blockControls()
        {
            txtName.Enabled = false;
            flLoadImage.Enabled = false;
            cboState.Enabled = false;
            btnCancel.Enabled = false;
            btnSave.Enabled = false;
            btnNew.Enabled = true;
            clearControls();
        }

        private void unlockControls()
        {
            txtName.Enabled = true;
            flLoadImage.Enabled = true;
            cboState.Enabled = true;
            btnCancel.Enabled = true;
            btnSave.Enabled = true;
            btnNew.Enabled = false;
            clearControls();
        }

        private void clearControls()
        {
            txtCode.Text = "";
            txtName.Text = "";
            flLoadImage.Attributes.Clear();
            uploadFile.Text = "";
            cboState.SelectedValue = "0";
        }

        protected void gvSlider_RowEditing(object sender, GridViewEditEventArgs e)
        {
            unlockControls();
            Int32 code = Convert.ToInt32(gvSlider.Rows[e.NewEditIndex].Cells[0].Text);
            Entities.Slider oSlider = SliderBLL.getInstance().getSider(code);
            txtCode.Text = oSlider.code.ToString();
            txtName.Text = oSlider.description.ToString();
            imgUpload.ImageUrl = oSlider.image;
           
            try
            {
                cboState.SelectedValue = oSlider.state.ToString();
            }
            catch (Exception)
            {
                cboState.SelectedValue = "1";
            }
        }

        protected void gvSlider_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            String sliderName = gvSlider.Rows[e.RowIndex].Cells[1].Text;
            slider_id = Convert.ToInt32(gvSlider.Rows[e.RowIndex].Cells[0].Text);
            lblSliderName.Text = sliderName;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "confirmMessage", "$('#confirmMessage').modal();", true);
            confirmModal.Update();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Int32 records = SliderBLL.getInstance().delete(slider_id);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "closeConfirmMessage", "$('#confirmMessage').modal('toggle');", true);

            if (records > 0)
            {
                lblMessage.Text = "Imagen eliminada correctamente.";
            }
            loadData();
        }
    }
}