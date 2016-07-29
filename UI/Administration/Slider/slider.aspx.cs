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
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    blockControls();
            //}
            //loadData();
        }

        protected void btnNew_Click(object sender, ImageClickEventArgs e)
        {
            unlockControls();
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            Int32 records = 0;

            if (this.validateData())
            {
                int iLen = flLoadImage.PostedFile.ContentLength;
                byte[] btArr = new byte[iLen];
                flLoadImage.PostedFile.InputStream.Read(btArr, 0, iLen);

                Entities.Slider oSlider = new Entities.Slider();
                oSlider.code = Convert.ToInt32(txtCode.Text);
                oSlider.description = txtName.Text;
                oSlider.image = Convert.ToBase64String(btArr);
                oSlider.state = 1;

                records = SliderBLL.getInstance().insert(oSlider);

                if (records > 0)
                {
                    lblMessage.Text = "Datos almacenados correctamente.";
                }

            }            
            //blockControls();
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

        private void blockControls()
        {
            txtName.Enabled = false;
            flLoadImage.Enabled = false;
            cboState.Enabled = false;
            btnCancel.Enabled = false;
            btnSave.Enabled = false;
            btnNew.Enabled = true;
        }

        private void unlockControls()
        {
            txtName.Enabled = true;
            flLoadImage.Enabled = true;
            cboState.Enabled = true;
            btnCancel.Enabled = true;
            btnSave.Enabled = true;
            btnNew.Enabled = false;
        }
    }
}