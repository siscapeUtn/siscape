using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using text = iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.IO;
using BLL;

namespace UI.Academic.Report
{
    public partial class waitingReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getPeriod();
                lblMessage.Text = "";
            }
        }

        protected Boolean validate()
        {
            Boolean ind = true;

            if (Convert.ToInt32(cboPeriod.SelectedValue) == 0)
            {
                ind = false;
                lblMessage.Text = "Debe seleccionar el período.";
            }
            else
            {
                lblMessage.Text = "";
            }

            return ind;
        }

        public void getPeriod()
        {
            List<Entities.Period> listPeriod = new List<Entities.Period>();
            listPeriod = BLL.PeriodBLL.getInstance().getAll();
            ListItem oItemS = new ListItem("---- Seleccione ----", "0");
            cboPeriod.Items.Add(oItemS);
            foreach (Entities.Period olistPeriod in listPeriod)
            {
                ListItem oItem = new ListItem(olistPeriod.name + " " + olistPeriod.oPeriodType.description, olistPeriod.code.ToString());
                cboPeriod.Items.Add(oItem);
            }
        }

        protected void btnReport_Click(object sender, ImageClickEventArgs e)
        {
            Int32 period_id = Convert.ToInt32(cboPeriod.SelectedValue);
            Int32 isContact = Convert.ToInt32(cboOptions.SelectedValue);

            if (validate())
            {
                List<Entities.WaitingList> listWaitingList = ReportBLL.getInstance().reportWaitingList(period_id, isContact);
                getReport(listWaitingList);
            }
            
        }

        protected void getReport(List<Entities.WaitingList> pListWaitingList)
        {
            try
            {
                List<Entities.WaitingList> listWaitingList = pListWaitingList;

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
                title.Add("\n\n Reporte de lista de espera\n\n");
                pdfDoc.Add(title);

                PdfPTable oPTable = new PdfPTable(6);
                oPTable.TotalWidth = 100;
                oPTable.SpacingBefore = 20f;
                oPTable.SpacingAfter = 30f;
                oPTable.AddCell("Nombre completo");
                oPTable.AddCell("# Residencial");
                oPTable.AddCell("# Celular");
                oPTable.AddCell("Correo electrónico");
                oPTable.AddCell("Curso");
                oPTable.AddCell("Día");

                if (listWaitingList.Count > 0)
                {
                    foreach (Entities.WaitingList pWaitingList in listWaitingList)
                    {
                        oPTable.AddCell(pWaitingList.name + " " + pWaitingList.lastName);
                        oPTable.AddCell(pWaitingList.homePhone);
                        oPTable.AddCell(pWaitingList.cellPhone);
                        oPTable.AddCell(pWaitingList.email);
                        oPTable.AddCell(pWaitingList.course_name);
                        oPTable.AddCell(pWaitingList.day);
                    }
                }
                else
                {
                    PdfPCell cell = new PdfPCell(new text::Phrase("No existen datos registrados."));
                    cell.Colspan = 6;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    oPTable.AddCell(cell);
                }

                pdfDoc.Add(oPTable);
                pdfDoc.Close();

                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=waitingreportmain.pdf");
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