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
using Entities;

namespace UI.Academic
{
    public partial class functionary : System.Web.UI.Page
    {
        static Int32 functionary_id = -1;
        static UserSystem oUser = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                blockControls();
                loadUser();
                loadPrograms();
            }
            loadData();
        }

        private void loadUser()
        {
            oUser = (UserSystem)Session["User"];
            if (oUser == null)
            {
                Response.Redirect("../../index.aspx");
            }
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
                Entities.Program oProgram = new Program(); 
                oFunctionary.code = Convert.ToInt32(txtCode.Text);
                oFunctionary.id = txtId.Text;
                oFunctionary.name = txtName.Text;
                oFunctionary.lastName = txtLastName.Text;
                oFunctionary.homePhone = txtHomePhone.Text;
                oFunctionary.cellPhone = txtCellPhone.Text;
                oFunctionary.email = txtEmail.Text;
                oProgram.code= Convert.ToInt32(cboprogram.SelectedValue);
                oFunctionary.oProgram = oProgram;
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "$('html, body').animate({ scrollTop: $('body').offset().top });", true);
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
            int code = oUser.oProgram.code;
            if (code == 1)
            {
             gvFunctionary.DataSource = FunctionaryBLL.getInstance().getAll();
            }
            else { 
            gvFunctionary.DataSource = FunctionaryBLL.getInstance().getAllByProgram(code);
            }

            gvFunctionary.DataBind();
        }

        protected void loadPrograms()
        {
            List<Entities.Program> listPrograms = new List<Entities.Program>();
            listPrograms = ProgramBLL.getInstance().getAllActived();
            ListItem oItemS = new ListItem("---- Seleccione ----", "0");
            cboprogram.Items.Add(oItemS);
            foreach (Entities.Program oProgram in listPrograms)
            {
                ListItem oItem = new ListItem(oProgram.name, oProgram.code.ToString());
                cboprogram.Items.Add(oItem);
            }
            if (oUser.oProgram.code != 1)
            {
                cboprogram.SelectedValue = oUser.oProgram.code.ToString();
            }
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
            cboprogram.Enabled = false;
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
            if (oUser.oProgram.code == 1)
            {
                cboprogram.Enabled = true;
            }
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
            cboprogram.SelectedValue = "0";
            lblmessageprogram.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorProgram", "$('#ContentPlaceHolder1_cboProgram').removeClass('has-error');", true);
        }

        protected void gvFunctionary_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvFunctionary.PageIndex = e.NewPageIndex;
            loadData();
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                int code = oUser.oProgram.code;
                List<Entities.Functionary> listFunctionary;
                if (code == 1)
                {
                     listFunctionary = FunctionaryBLL.getInstance().getAll();
                }
                else
                {
                     listFunctionary = FunctionaryBLL.getInstance().getAllByProgram(code);
                }
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
                title.Add("\n\n Reporte de Funcionarios\n\n");
                pdfDoc.Add(title);
                
                PdfPTable oPTable = new PdfPTable(5);
                oPTable.TotalWidth = 100;
                oPTable.SpacingBefore = 20f;
                oPTable.SpacingAfter = 30f;
                oPTable.AddCell("Identificación");
                oPTable.AddCell("Nombre completo");
                oPTable.AddCell("Teléfono");
                oPTable.AddCell("Correo electrónico");
                oPTable.AddCell("Programa");
                oPTable.AddCell("Estado");

                if (listFunctionary.Count > 0)
                {
                    foreach (Entities.Functionary pFunctionary in listFunctionary)
                    {
                        oPTable.AddCell(pFunctionary.id);
                        oPTable.AddCell(pFunctionary.name + " " + pFunctionary.lastName);
                        oPTable.AddCell(pFunctionary.cellPhone);
                        oPTable.AddCell(pFunctionary.email);
                        oPTable.AddCell(pFunctionary.oProgram.name);
                        oPTable.AddCell((pFunctionary.state == 1 ? "Activo" : "Inactivo"));
                    }
                }
                else
                {
                    PdfPCell cell = new PdfPCell(new text::Phrase("No existen funcionarios registrados."));
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
                Response.AddHeader("Content-Disposition", "attachment; filename=Funcionarios.pdf");
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