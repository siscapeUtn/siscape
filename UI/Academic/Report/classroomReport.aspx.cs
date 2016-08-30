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
    public partial class classroomReport : System.Web.UI.Page
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
                List<Entities.ClassRoom> listClassRoom = ReportBLL.getInstance().reportClassRoom(period_id);
                getReport(listClassRoom);
            }
            
        }

        protected void getReport(List<Entities.ClassRoom> listClassRoom)
        {
            try
            {
                List<Entities.ClassRoom> listRoom = listClassRoom;
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
                title.Add("\n\n Reporte de Aulas\n\n\n\n");
                pdfDoc.Add(title);
                
                PdfPTable oPTable = new PdfPTable(6);
                oPTable.TotalWidth = 100;
                oPTable.SpacingBefore = 20f;
                oPTable.SpacingAfter = 30f;
                oPTable.AddCell("Descripción");
                oPTable.AddCell("Capacidad");
                oPTable.AddCell("Programa");
                oPTable.AddCell("Tipo de Aula");
                oPTable.AddCell("Localizacion");
                oPTable.AddCell("Estado");

                if (listRoom.Count > 0)
                {
                    foreach (Entities.ClassRoom pRoom in listRoom)
                    {
                        oPTable.AddCell(pRoom.num_room);
                        oPTable.AddCell(pRoom.size.ToString());
                        oPTable.AddCell(pRoom.oProgram.name);
                        oPTable.AddCell(pRoom.oClassRoomsType.description);
                        oPTable.AddCell(pRoom.oLocation.oHeadquarters.description + " - " + pRoom.oLocation.building + " - " + pRoom.oLocation.module);
                        oPTable.AddCell((pRoom.state == 1 ? "Activo" : "Inactivo"));
                    }
                }
                else
                {
                    PdfPCell cell = new PdfPCell(new text::Phrase("No existen aulas registrados."));
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
                Response.AddHeader("Content-Disposition", "attachment; filename=Aulas.pdf");
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

        protected void btnReturn_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("../AcademicGroups/gReport.aspx");
        }
    }
}