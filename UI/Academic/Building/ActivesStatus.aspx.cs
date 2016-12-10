using BLL;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using text = iTextSharp.text;

namespace UI.Academic.Building
{
    public partial class ActivesStatus : System.Web.UI.Page
    {
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
            txtCode.Text = ActivesStatusBLL.getInstance().getNextCode().ToString();
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            Int32 records = 0;

            if (validateData())
            {
                Entities.ActivesStatus oActivesStatus = new Entities.ActivesStatus();
                oActivesStatus.activesSatus_ID = Convert.ToInt32(txtCode.Text);
                oActivesStatus.description = txtDescription.Text;

                if (ActivesStatusBLL.getInstance().exists(oActivesStatus))
                {
                    records = ActivesStatusBLL.getInstance().modify(oActivesStatus);
                }
                else
                {
                    records = ActivesStatusBLL.getInstance().insert(oActivesStatus);
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
            gvActiveStatus.DataSource = ActivesStatusBLL.getInstance().getAll();
            gvActiveStatus.DataBind();
        }

        protected void gvActiveStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvActiveStatus.PageIndex = e.NewPageIndex;
            loadData();
        }
        protected void blockControls()
        {
            txtCode.Enabled = false;
            txtDescription.Enabled = false;
            btnNew.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            clearControls();
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
            else if (ActivesStatusBLL.getInstance().existsName(txtDescription.Text))
            {
                ind = false;
                lblMessageDescription.Text = "Esta descripción ya está en uso.";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorDescription", "$('#ContentPlaceHolder1_txtDescription').addClass('has-error');", true);
            }
            else
            {
                lblMessageDescription.Text = "";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorDescription", "$('#ContentPlaceHolder1_txtDescription').removeClass('has-error');", true);
            }

            return ind;
        }

        protected void unlockControls()
        {
            txtCode.Enabled = true;
            txtDescription.Enabled = true;
            btnNew.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            clearControls();
        }

        protected void clearControls()
        {
            txtCode.Text = "";
            txtDescription.Text = "";
            lblMessage.Text = "";
            lblMessageDescription.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorDescription", "$('#ContentPlaceHolder1_txtDescription').removeClass('has-error');", true);
        }

        protected void gvActiveStatus_RowEditing(object sender, GridViewEditEventArgs e)
        {
            unlockControls();
            Int32 code = Convert.ToInt32(gvActiveStatus.Rows[e.NewEditIndex].Cells[0].Text);
            Entities.ActivesStatus oActivesStatus = ActivesStatusBLL.getInstance().getActivesStatus(code);
            txtCode.Text = oActivesStatus.activesSatus_ID.ToString();
            txtDescription.Text = oActivesStatus.description;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "$('html, body').animate({ scrollTop: $('body').offset().top });", true);
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                List<Entities.ActivesStatus> listActivesStatus = ActivesStatusBLL.getInstance().getAll();
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
                title.Add("\n\n Reporte de Estado de Activos\n\n\n\n");
                pdfDoc.Add(title);

                PdfPTable oPTable = new PdfPTable(2);
                oPTable.TotalWidth = 100;
                oPTable.SpacingBefore = 20f;
                oPTable.SpacingAfter = 30f;
                oPTable.AddCell("Id");
                oPTable.AddCell("Descripción");

                if (listActivesStatus.Count > 0)
                {
                    foreach (Entities.ActivesStatus pActivesStatus in listActivesStatus)
                    {
                        oPTable.AddCell(pActivesStatus.activesSatus_ID.ToString());
                        oPTable.AddCell((pActivesStatus.description));
                    }
                }
                else
                {
                    PdfPCell cell = new PdfPCell(new text::Phrase("No existen tipos de aula registrados."));
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
                Response.AddHeader("Content-Disposition", "attachment; filename=StatusActives.pdf");
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