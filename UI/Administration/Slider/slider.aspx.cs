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
            System.IO.Stream fs = flLoadImage.PostedFile.InputStream;
            System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
            Byte[] bytes = br.ReadBytes((Int32)fs.Length);
            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
            //Image1.ImageUrl = "data:image/png;base64," + base64String;
            //Image1.Visible = true;

            txtName.Text = "data:image/png;base64," + base64String;
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

        public String ImagetoBase64(String path)
        {
            String base64String = "";
            
            using (System.Drawing.Image image = System.Drawing.Image.FromFile(path))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();
                    base64String = Convert.ToBase64String(imageBytes);
                }
            }

            return base64String;
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

            if (flLoadImage.PostedFile != null)
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
            btnCancel.Enabled = false;
            btnSave.Enabled = false;
            btnNew.Enabled = true;
        }

        private void unlockControls()
        {
            txtName.Enabled = true;
            flLoadImage.Enabled = true;
            btnCancel.Enabled = true;
            btnSave.Enabled = true;
            btnNew.Enabled = false;
        }
    }
}