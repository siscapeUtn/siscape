using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Entities;
using text = iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.IO;

namespace UI.Academic
{
    public partial class headquarters : System.Web.UI.Page
    {
        static Int32 headquarters_id = -1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if ( !IsPostBack )
            {
                blockControls();
            }
            loadData();
        }

        protected void btnNew_Click(object sender, ImageClickEventArgs e)
        {
            unlockControls();
            txtCode.Text = HeadquartersBLL.getInstance().getNextCode().ToString();
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            Int32 records = 0;
            if ( validateData() )
            {
                Entities.Headquarters oHeadquarters = new Headquarters();
                oHeadquarters.code = Convert.ToInt32(txtCode.Text);
                oHeadquarters.description = txtDescription.Text;
                oHeadquarters.state = Convert.ToInt32(cboState.SelectedValue);
                if (HeadquartersBLL.getInstance().exists(oHeadquarters.code))
                {
                    records = HeadquartersBLL.getInstance().modify(oHeadquarters);
                }
                else
                {
                    records = HeadquartersBLL.getInstance().insert(oHeadquarters);
                }

                blockControls();
                loadData();

                if ( records > 0 )
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
            gvHeadquarters.DataSource = HeadquartersBLL.getInstance().getAll();
            gvHeadquarters.DataBind();
        }

        protected Boolean validateData()
        {
            Boolean ind = true;

            if (txtDescription.Text.Trim() == "")
            {
                ind = false;
                lblMessageDescription.Text = "Debe digitar una descripción correcta.";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorName", "$('#ContentPlaceHolder1_txtDescription').addClass('has-error');", true);
            }
            else
            {
                lblMessageDescription.Text = "";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorName", "$('#ContentPlaceHolder1_txtDescription').removeClass('has-error');", true);
            }

            return ind;
        }

        protected void gvHeadquarters_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            String headquarterDescription = gvHeadquarters.Rows[e.RowIndex].Cells[1].Text;
            headquarters_id = Convert.ToInt32(gvHeadquarters.Rows[e.RowIndex].Cells[0].Text);
            lblHeadquarterDescription.Text = headquarterDescription;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirmMessage", "$('#confirmMessage').modal();", true);
            confirmModal.Update();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Int32 records = HeadquartersBLL.getInstance().delete(headquarters_id);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirmMessage", "$('#confirmMessage').modal('toggle');", true);

            if (records > 0)
            {
                lblMessage.Text = "Sede eliminada correctamente.";
            }
            loadData();
        }

        protected void gvHeadquarters_RowEditing(object sender, GridViewEditEventArgs e)
        {
            unlockControls();
            Int32 code = Convert.ToInt32(gvHeadquarters.Rows[e.NewEditIndex].Cells[0].Text);
            Entities.Headquarters oHeadquarters = HeadquartersBLL.getInstance().getHeadquarters(code);
            txtCode.Text = oHeadquarters.code.ToString();
            txtDescription.Text = oHeadquarters.description;
            cboState.SelectedValue = oHeadquarters.state.ToString();
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
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorName", "$('#ContentPlaceHolder1_txtDescription').removeClass('has-error');", true);
        }

        protected void gvHeadquarters_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvHeadquarters.PageIndex = e.NewPageIndex;
            loadData();
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                List<Entities.Headquarters> listHeadquarters = HeadquartersBLL.getInstance().getAll();
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
                title.Add("\n\n Reporte de Sedes\n\n");
                pdfDoc.Add(title);
                
                PdfPTable oPTable = new PdfPTable(2);
                oPTable.TotalWidth = 100;
                oPTable.SpacingBefore = 20f;
                oPTable.SpacingAfter = 30f;
                oPTable.AddCell("Descripción");
                oPTable.AddCell("Estado");

                if (listHeadquarters.Count > 0)
                {
                    foreach (Entities.Headquarters pHeadquarters in listHeadquarters)
                    {
                        oPTable.AddCell(pHeadquarters.description);
                        oPTable.AddCell((pHeadquarters.state == 1 ? "Activo" : "Inactivo"));
                    }
                }
                else
                {
                    PdfPCell cell = new PdfPCell(new text::Phrase("No existen sedes registradas."));
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
                Response.AddHeader("Content-Disposition", "attachment; filename=Sedes.pdf");
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