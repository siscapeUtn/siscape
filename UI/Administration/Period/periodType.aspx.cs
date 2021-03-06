﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using text = iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.IO;

namespace UI.Administration
{
    public partial class PeriodType : System.Web.UI.Page
    {
        static Int32 periodType_id = -1;
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
            txtCode.Text = PeriodTypeBLL.getInstance().getNextCode().ToString();
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            Int32 records = 0;
            if (validateData())
            {
                Entities.PeriodType oPeriodType = new Entities.PeriodType();
                oPeriodType.code = Convert.ToInt32(txtCode.Text);
                oPeriodType.description = txtName.Text;
                oPeriodType.state = Convert.ToInt16(cboState.SelectedValue);
                if (PeriodTypeBLL.getInstance().exists(oPeriodType.code))
                {
                    records = PeriodTypeBLL.getInstance().modify(oPeriodType);
                }
                else
                {
                    records = PeriodTypeBLL.getInstance().insert(oPeriodType);
                }
                blockControls();
                loadData();

                if (records > 0)
                {
                    lblMessage.Text = "Datos almacenados correactamente";
                }
            }
        }

        protected void btnCancel_Click(object sender, ImageClickEventArgs e)
        {
            blockControls();
        }

        protected void btnReturn_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("../AdministrationGroups/gPeriod.aspx");
        }

        protected void gvPeriodType_RowEditing(object sender, GridViewEditEventArgs e)
        {
            unlockControls();
            Int32 code = Convert.ToInt32(gvPeriodType.Rows[e.NewEditIndex].Cells[0].Text);
            Entities.PeriodType oPeriodType = PeriodTypeBLL.getInstance().getPeriod(code);
            txtCode.Text = oPeriodType.code.ToString();
            txtName.Text = oPeriodType.description.ToString();
            try
            {
                cboState.SelectedValue = oPeriodType.state.ToString();
            }
            catch (Exception)
            {
                cboState.SelectedValue = "1";
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "$('html, body').animate({ scrollTop: $('body').offset().top });", true);
        }

        protected void gvPeriodType_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            String periodTypeDescription = gvPeriodType.Rows[e.RowIndex].Cells[1].Text;
            periodType_id = Convert.ToInt32(gvPeriodType.Rows[e.RowIndex].Cells[0].Text);
            lblPeriodTypeDescription.Text = periodTypeDescription;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirmMessage", "$('#confirmMessage').modal();", true);
            confirmModal.Update();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Int32 records = PeriodTypeBLL.getInstance().delete(periodType_id);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirmMessage", "$('#confirmMessage').modal('toggle');", true);

            if (records > 0)
            {
                lblMessage.Text = "Tipo de periodo eliminado correctamente.";
            }
            loadData();
        }

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

            return ind;
        }

        /**
         * Method to get period types registered in the database and to load in a GridView
        */ 
        protected void loadData()
        {
            gvPeriodType.DataSource = PeriodTypeBLL.getInstance().getAll();
            gvPeriodType.DataBind();
        } //End loadData()

        /** 
         * Method to block the fields
        */
        protected void blockControls()
        {
            clearControls();
            txtName.Enabled = false;
            cboState.Enabled = false;
            btnNew.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
        } //End blockControls()

        /**
         * Method to unlock the form fields
        */
        protected void unlockControls()
        {
            clearControls();
            txtName.Enabled = true;
            cboState.Enabled = true;
            btnNew.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
        } //End unlockControls()

        /**
         * Method to clear the form fields 
        */
        protected void clearControls()
        {
            txtCode.Text = "";
            txtName.Text = "";
            cboState.SelectedValue = "1";
            lblMessage.Text = "";
            lblNameMessage.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorName", "$('#ContentPlaceHolder1_txtName').removeClass('has-error');", true);
        }

        protected void gvPeriodType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPeriodType.PageIndex = e.NewPageIndex;
            loadData();
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                List<Entities.PeriodType> listPeriodType = PeriodTypeBLL.getInstance().getAll();
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
                title.Add("\n\n Reporte de Tipo de Períodos\n\n\n\n");
                pdfDoc.Add(title);
                
                PdfPTable oPTable = new PdfPTable(2);
                oPTable.TotalWidth = 100;
                oPTable.SpacingBefore = 20f;
                oPTable.SpacingAfter = 30f;
                oPTable.AddCell("Descripción");
                oPTable.AddCell("Estado");

                if (listPeriodType.Count > 0)
                {
                    foreach (Entities.PeriodType pPeriodType in listPeriodType)
                    {
                        oPTable.AddCell(pPeriodType.description);
                        oPTable.AddCell((pPeriodType.state == 1 ? "Activo" : "Inactivo"));
                    }
                }
                else
                {
                    PdfPCell cell = new PdfPCell(new text::Phrase("No existen tipo de períodos registrados."));
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
                Response.AddHeader("Content-Disposition", "attachment; filename=Tipo_Periodo.pdf");
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
        }//End clearControls()
    } //End periodType
}