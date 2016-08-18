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

namespace UI.Administration
{
    public partial class Program : System.Web.UI.Page
    {
        static Int32 program_id = -1;

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
            txtCode.Text = ProgramBLL.getInstance().getNextCode().ToString();
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            Int32 records = 0;
           
            if (validateData())
            {
                Entities.Program oProgram = new Entities.Program();
                oProgram.code = Convert.ToInt32(txtCode.Text);
                oProgram.name = txtName.Text.ToString();
                oProgram.unit = Convert.ToInt64(txtUnit.Text);
                oProgram.state = Convert.ToInt16(cboState.SelectedValue);

                if (ProgramBLL.getInstance().exists(oProgram.code)) //If the program exists in the database
                {
                    records = ProgramBLL.getInstance().modify(oProgram);//To modify the program
                }
                else
                {
                    records = ProgramBLL.getInstance().insert(oProgram);//To insert a program
                }
                blockControls();
                loadData();
                if (records > 0)
                {
                    lblMessage.Text = "Datos almacenados correctamente.";
                }
            }
        }

        protected void btnCancel_Click(object sender, ImageClickEventArgs e)
        {
            blockControls();
        }

        protected void btnReturn_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("../AdministrationGroups/administration.aspx");
        }

        /**
         * Method to get programs registered in the database and load in a gridView
        */
        protected void loadData()
        {
            gvProgram.DataSource = ProgramBLL.getInstance().getAll();
            gvProgram.DataBind();
        }

        protected void gvProgram_RowEditing(object sender, GridViewEditEventArgs e)
        {
            unlockControls();
            Int32 code = Convert.ToInt32(gvProgram.Rows[e.NewEditIndex].Cells[0].Text);
            Entities.Program oProgram = ProgramBLL.getInstance().getProgram(code);
            txtCode.Text = oProgram.code.ToString();
            txtName.Text = oProgram.name.ToString();
            txtUnit.Text = oProgram.unit.ToString();
            try
            {
                cboState.SelectedValue = oProgram.state.ToString();
            }
            catch (Exception)
            {
                cboState.SelectedValue = "1";
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(),"redirect", "$('html, body').animate({ scrollTop: $('body').offset().top });", true);
        }

        protected void gvProgram_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            String programName = gvProgram.Rows[e.RowIndex].Cells[1].Text;
            program_id = Convert.ToInt32(gvProgram.Rows[e.RowIndex].Cells[0].Text);
            lblProgramName.Text = programName;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "confirmMessage", "$('#confirmMessage').modal();", true);
            confirmModal.Update();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Int32 records = ProgramBLL.getInstance().delete(program_id);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "closeConfirmMessage", "$('#confirmMessage').modal('toggle');", true);
            
            if (records > 0)
            {
                lblMessage.Text = "Programa eliminado correctamente.";
            }
            loadData();
        }

        /**
         * Method to validate the data information 
        */
        protected Boolean validateData()
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

            try
            {
                Convert.ToInt32(txtUnit.Text);
                lblUnitMessage.Text = "";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorUnit", "$('#ContentPlaceHolder1_txtUnit').removeClass('has-error');", true);
            }
            catch (Exception)
            {
                ind = false;
                lblUnitMessage.Text = "Debe digitar una unidad ejecutora correcta.";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorUnit", "$('#ContentPlaceHolder1_txtUnit').addClass('has-error');", true);
            }

            return ind;
        }

        /**
         * Method to block the controls 
        */
        public void blockControls()
        {
            clearControls();
            lblCode.Visible = false;
            txtCode.Visible = false;
            txtName.Enabled = false;
            txtUnit.Enabled = false;
            cboState.Enabled = false;
            btnCancel.Enabled = false;
            btnSave.Enabled = false;
            btnNew.Enabled = true;
        }//End blockControls()

        /**
         * Method to unlock the controls 
        */
        public void unlockControls()
        {
            clearControls();
            lblCode.Visible = false;
            txtCode.Visible = false;
            txtName.Enabled = true;
            txtUnit.Enabled = true;
            cboState.Enabled = true;
            btnCancel.Enabled = true;
            btnSave.Enabled = true;
            btnNew.Enabled = false;
        }//End unlockControls()

        /**
         * Method to clear the controls
        */
        public void clearControls()
        {
            txtCode.Text = "";
            txtName.Text = "";
            txtUnit.Text = "";
            cboState.SelectedValue = "1";
            lblMessage.Text = "";
            lblNameMessage.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorName", "$('#ContentPlaceHolder1_txtName').removeClass('has-error');", true);
            lblUnitMessage.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorUnit", "$('#ContentPlaceHolder1_txtName').removeClass('has-error');", true);
        }//End clearControls()

        protected void gvProgram_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvProgram.PageIndex = e.NewPageIndex;
            loadData();
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                List<Entities.Program> listProgram = ProgramBLL.getInstance().getAll();
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
                title.Add("\n\n Reporte de Programas\n\n\n\n");
                pdfDoc.Add(title);
                
                PdfPTable oPTable = new PdfPTable(3);
                oPTable.TotalWidth = 100;
                oPTable.SpacingBefore = 20f;
                oPTable.SpacingAfter = 30f;
                oPTable.AddCell("Descripción");
                oPTable.AddCell("Unidad Ejecutora");
                oPTable.AddCell("Estado");

                if (listProgram.Count > 0)
                {
                    foreach (Entities.Program pProgram in listProgram)
                    {
                        oPTable.AddCell(pProgram.name);
                        oPTable.AddCell(pProgram.unit.ToString());
                        oPTable.AddCell((pProgram.state == 1 ? "Activo" : "Inactivo"));
                    }
                }
                else
                {
                    PdfPCell cell = new PdfPCell(new text::Phrase("No existen programas registrados."));
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
                Response.AddHeader("Content-Disposition", "attachment; filename=Programas.pdf");
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
    }//End program
}