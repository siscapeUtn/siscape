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

namespace UI.Academic.Report
{
    public partial class externalDesignationReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            getReport();
        }

        protected void getReport()
        {
            try
            {
                List<Entities.ExternalDesignation> listExternalDesignation = ExternalDesignationBLL.getInstance().getAll();
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
                title.Add("\n\n Reporte de Nombramientos Externos\n\n");
                pdfDoc.Add(title);
               

                if (listExternalDesignation.Count > 0)
                {
                    foreach (Entities.ExternalDesignation pExternalDesignation in listExternalDesignation)
                    {
                        PdfPTable oPTable = new PdfPTable(5);
                        oPTable.TotalWidth = 100;
                        oPTable.AddCell("Funcionario");
                        oPTable.AddCell("Cargo");
                        oPTable.AddCell("Lugar de Trabajo");
                        oPTable.AddCell("Inicio - Fin");
                        oPTable.AddCell("Horas");
                        oPTable.AddCell(pExternalDesignation.oTeacher.name + " " + pExternalDesignation.oTeacher.lastName );
                        oPTable.AddCell(pExternalDesignation.position);
                        oPTable.AddCell(pExternalDesignation.location);
                        oPTable.AddCell(pExternalDesignation.initial_day.ToShortDateString() + " " + pExternalDesignation.final_day.ToShortDateString());
                        oPTable.AddCell(pExternalDesignation.hours.ToString());
                        pdfDoc.Add(oPTable);

                        PdfPTable oPTable2 = new PdfPTable(7);
                        oPTable2.TotalWidth = 100;
                        oPTable2.SpacingAfter = 30f;
                        oPTable2.SpacingBefore = 5f;
                        oPTable2.AddCell("Lunes");
                        oPTable2.AddCell("Martes");
                        oPTable2.AddCell("Miércoles");
                        oPTable2.AddCell("Jueves");
                        oPTable2.AddCell("Viernes");
                        oPTable2.AddCell("Sábado");
                        oPTable2.AddCell("Domingo");

                        Int32 count = 1;
                        while (count <= 7)
                        {
                            Boolean ind = false;
                            
                            for( Int32 i = 0; i < pExternalDesignation.journeys.Count; i++ )
                            {
                                if ( pExternalDesignation.journeys[i].day.code == count )
                                {
                                    ind = true;
                                    oPTable2.AddCell(pExternalDesignation.journeys[i].start + " / " + pExternalDesignation.journeys[i].finish);
                                }
                            }

                            if ( !ind )
                            {
                                oPTable2.AddCell("00:00" + " / " + "00:00");
                            }
                            count++;
                        }

                        pdfDoc.Add(oPTable2);
          
                    }
                }
                else
                {
                    PdfPTable oPTable = new PdfPTable(5);
                    oPTable.TotalWidth = 100;
                    oPTable.SpacingBefore = 20f;
                    oPTable.SpacingAfter = 30f;
                    oPTable.AddCell("Funcionario");
                    oPTable.AddCell("Cargo");
                    oPTable.AddCell("Lugar de Trabajo");
                    oPTable.AddCell("Inicio - Fin");
                    oPTable.AddCell("Horas");
                    PdfPCell cell = new PdfPCell(new text::Phrase("No existen nombramientos externos registrados."));
                    cell.Colspan = 5;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    oPTable.AddCell(cell);
                    pdfDoc.Add(oPTable);
                }

                
                pdfDoc.Close();
                
                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=Nombramiento_Externo.pdf");
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