using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using text = iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.IO;

namespace UI.Administration.Security
{
    public partial class User : System.Web.UI.Page
    {
        static Int32 resetUserSystem_id = -1;
        static string resetUserSystemid_id = "";
        static Int32 userSystem_id = -1;
        static Int32 userAttempts = 0;
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
                if(oProgram.code == 0)
                {
                    oProgram.code = 1;
                }
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

                    if (records > 0)
                    {
                        Entities.Email oEmail = new Entities.Email();
                        String body = messageDesign(oUser.email);
                        oEmail.correoContacto(oUser.email, body, "Bienvenido a Siscape");
                    }
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

        protected String messageDesign(String email)
        {
            String body = "Bienvenido a SISCAPE. \n";
            body += "Se ha creado una cuenta en el sistema SISCAPE. \n";
            body += "Su correo electrónico para accessar: " + email;
            return body;
        }

        protected void btnCancel_Click(object sender, ImageClickEventArgs e)
        {
            blockControls();
        }

        protected void btnReturn_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("../SecurityGroups/security.aspx");
        }

        protected void loadData()
        {
            gvUserSystem.DataSource = BLL.UserSystemBLL.getInstance().getAll();
            gvUserSystem.DataBind();
        }

        protected void gvUserSystem_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Int32 code = Convert.ToInt32(gvUserSystem.Rows[e.NewEditIndex].Cells[0].Text);
            if (code != 1)
            {
                unlockControls();

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
                if (oUser.oProgram.code == 1)
                {
                    cboProgram.SelectedValue = "0";
                }else { 
                cboProgram.SelectedValue = oUser.oProgram.code.ToString();
                }
                cboRole.SelectedValue = oUser.oRole.Role_Id.ToString();
                try
                {
                    cboState.SelectedValue = oUser.state.ToString();
                }
                catch (Exception)
                {
                    cboState.SelectedValue = "1";
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "$('html, body').animate({ scrollTop: $('body').offset().top });", true);
            }
            else
            {
                lblMessage.Text = "Este usuario no se puede modificar.";
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
            resetUserSystemid_id = gvUserSystem.Rows[e.RowIndex].Cells[1].Text;
            lblUserName.Text = UserName;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ResetPassword", "$('#ResetPassword').modal();", true);
            confirmModal.Update();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {

            Entities.UserSystem oUser = (Entities.UserSystem)Session["User"];

            if(txtModalUser.Text == oUser.email && txtModalPassword.Text == oUser.Password)
            {
                Int32 records = UserSystemBLL.getInstance().resetPasswordSecurity(resetUserSystem_id, resetUserSystemid_id);
                userAttempts = 0;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "closeResetPassword", "$('#ResetPassword').modal('toggle');", true);
                loadData();
                if (records > 0)
                {
                    Entities.Email oEmail = new Entities.Email();
                    String body = messageDesignReset(oUser.email);
                    oEmail.correoContacto(oUser.email, body, "Restablecer contraseña");
                    lblMessage.Text = "Se ha restablecido la contraseña correctamente.";
                }
            }
            else
            {
                txtModalUser.Text = "";
                txtModalPassword.Text = "";
                userAttempts++;
                if (userAttempts>=3)
                {
                    Response.Redirect("../../logOut.aspx");
                }
            }            
        }

        protected String messageDesignReset(String email)
        {
            String body = "Su cuenta se ha restablecido su contraseña. \n";
            body += "Se ha restablecido su contraseña en el sistema SISCAPE. \n";
            body += "Su correo electrónico para accessar: " + email + "\n";
            return body;
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
            catch (Exception)
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
            catch (Exception)
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
            catch (Exception)
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
            lblMessage.Text = "";
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
            cboProgram.SelectedValue = "0";
            lblMessageProgram.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorEmail", "$('#ContentPlaceHolder1_cboProgram').removeClass('has-error');", true);
            cboRole.SelectedValue = "0";
            lblMessageRole.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorEmail", "$('#ContentPlaceHolder1_cboRole').removeClass('has-error');", true);
            cboState.SelectedValue = "1";
        }

        protected void gvUserSystem_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUserSystem.PageIndex = e.NewPageIndex;
            loadData();
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                List<Entities.UserSystem> listUserSystem = UserSystemBLL.getInstance().getAll();
                System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
                text::Document pdfDoc = new text::Document(text::PageSize.A4, 10, 10, 10, 10);
                pdfDoc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
                PdfWriter.GetInstance(pdfDoc, memoryStream);
                pdfDoc.Open();

                String imagepath = Server.MapPath("../../images/page-icons");
                iTextSharp.text.Image deas = iTextSharp.text.Image.GetInstance(imagepath + "/DEAS-logo.jpg");
                deas.ScaleToFit(140f, 120f);
                //Give space before image
                deas.SpacingBefore = 10f;
                //Give some space after the image
                deas.SpacingAfter = 1f;
                deas.Alignment = text::Element.ALIGN_LEFT;
                pdfDoc.Add(deas);

                text::Paragraph title = new text::Paragraph();
                title.Font = text::FontFactory.GetFont("dax-black", 32, new text::BaseColor(0, 51, 102));
                title.Alignment = text::Element.ALIGN_CENTER;
                title.Add("\n\n Reporte de Usuarios\n\n");
                pdfDoc.Add(title);
                
                PdfPTable oPTable = new PdfPTable(5);
                oPTable.TotalWidth = 100;
                oPTable.SpacingBefore = 20f;
                oPTable.SpacingAfter = 30f;
                oPTable.AddCell("Nombre completo");
                oPTable.AddCell("Rol");
                oPTable.AddCell("Programa");
                oPTable.AddCell("Coreo electrónico");
                oPTable.AddCell("Estado");

                if (listUserSystem.Count > 0)
                {
                    foreach (Entities.UserSystem pUserSystem in listUserSystem)
                    {
                        oPTable.AddCell(pUserSystem.name + " " + pUserSystem.lastName);
                        oPTable.AddCell(pUserSystem.oRole.Description);
                        oPTable.AddCell(pUserSystem.oProgram.name);
                        oPTable.AddCell(pUserSystem.email);
                        oPTable.AddCell((pUserSystem.state == 1 ? "Activo" : "Inactivo"));
                    }
                }
                else
                {
                    PdfPCell cell = new PdfPCell(new text::Phrase("No existen usuarios registrados."));
                    cell.Colspan = 5;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    oPTable.AddCell(cell);
                }

                pdfDoc.Add(oPTable);
                pdfDoc.Close();
                
                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=Usuarios.pdf");
                Response.ContentType = "application/pdf";
                Response.Buffer = true;
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(bytes);
                Response.End();
                Response.Close();
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }
    }
}