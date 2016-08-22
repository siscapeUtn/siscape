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
using BLL;

namespace UI.Academic.Report
{
    public partial class teacherReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getPeriod();
                lblMessage.Text = "";
            }
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

        protected void btnReport_Click(object sender, ImageClickEventArgs e)
        {
            Int32 period_id = Convert.ToInt32(cboPeriod.SelectedValue);

            if (validate())
            {
                List<Entities.AcademicOffer> pListAcademicOffer = ReportBLL.getInstance().reportTeacher(period_id);
                printReport(pListAcademicOffer);
            }
            
        }

        protected void printReport(List<Entities.AcademicOffer> pListAcademicOffer)
        {
            try
            {
                List<Entities.AcademicOffer> listAcademicOffer = pListAcademicOffer;
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
                title.Add("\n\n Reporte de Profesores \n\n\n\n");
                pdfDoc.Add(title);
                
                PdfPTable oPTable = new PdfPTable(5);
                oPTable.TotalWidth = 100;
                oPTable.SpacingBefore = 20f;
                oPTable.SpacingAfter = 30f;
                oPTable.AddCell("Nombre Completo");
                oPTable.AddCell("Días");
                oPTable.AddCell("Hora de Inicio - Final");
                oPTable.AddCell("Cant. Horas");
                oPTable.AddCell("Estado");

                if (listAcademicOffer.Count > 0)
                {
                    foreach (Entities.AcademicOffer pAcademicOffer in listAcademicOffer)
                    {
                        oPTable.AddCell(pAcademicOffer.oteacher.name + " " + pAcademicOffer.oteacher.lastName);
                        oPTable.AddCell(pAcademicOffer.oSchedule.name);
                        oPTable.AddCell(pAcademicOffer.oSchedule.startTime.ToShortTimeString() + " " + pAcademicOffer.oSchedule.endTime.ToShortTimeString() );
                        oPTable.AddCell(pAcademicOffer.hours.ToString());
                        oPTable.AddCell((pAcademicOffer.oteacher.state == 1 ? "Activo" : "Inactivo"));
                    }
                }
                else
                {
                    PdfPCell cell = new PdfPCell(new text::Phrase("No existen profesores registrados."));
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
                Response.AddHeader("Content-Disposition", "attachment; filename=Profesores.pdf");
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