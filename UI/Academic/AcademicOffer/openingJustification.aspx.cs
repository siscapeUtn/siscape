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

namespace UI.Academic.AcademicOffer
{
    public partial class OpeningJustification : System.Web.UI.Page
    {
        public bool offerAcademic { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                blockControls();
            }

        }

        protected void showOfferAcademic()
        {
            this.offerAcademic = Convert.ToBoolean(Session["OfferAcademic"].ToString());
            if (this.offerAcademic == false)
            {
                Response.Redirect("../../index.aspx");
            }
        }

        public void getFunctionary()
        {
            List<Entities.Teacher> listTeacher = new List<Entities.Teacher>();
            listTeacher = TeacherBLL.getInstance().getAllActive();
            ListItem oItemS = new ListItem("Seleccione", "0");
            cboTeacher.Items.Add(oItemS);
            foreach (Entities.Teacher oTeacher in listTeacher)
            {
                ListItem oItem = new ListItem(oTeacher.name + " " + oTeacher.lastName, oTeacher.code.ToString());
                cboTeacher.Items.Add(oItem);
            }
        }

        public void getCourse()
        {
            List<Entities.Course> listCourse = new List<Entities.Course>();
            listCourse = CourseBLL.getInstance().getAllActived();
            ListItem oItemS = new ListItem("Seleccione", "0");
            cboCourse.Items.Add(oItemS);
            foreach (Entities.Course oCourse in listCourse)
            {
                ListItem oItem = new ListItem(oCourse.description, oCourse.id.ToString());
                cboCourse.Items.Add(oItem);
            }
        }

        protected void btnNew_Click(object sender, ImageClickEventArgs e)
        {
            unlockControls();
            getFunctionary();
            getCourse();
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, ImageClickEventArgs e)
        {
            blockControls();
        }

        protected void btnReturn_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("../AcademicGroups/gAcademicOffer.aspx");
        }

        private void blockControls()
        {
            clearControls();
            cboTeacher.Enabled = false;
            cboCourse.Enabled = false;
            txtDesignationHours.Enabled = false;
            txtSalary.Enabled = false;
            txtAnnuality.Enabled = false;
            txtFifty.Enabled = false;
            txtOther.Enabled = false;
            txtTotalIncome.Enabled = false;
            txtTotalIncomeMonth.Enabled = false;
            txtIncome.Enabled = false;
            txtValue.Enabled = false;
            txtStudents.Enabled = false;
            txtDifference.Enabled = false;
            btnNew.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
        }

        private void unlockControls()
        {
            clearControls();
            cboTeacher.Enabled = true;
            cboCourse.Enabled = true;
            txtDesignationHours.Enabled = true;
            txtSalary.Enabled = true;
            txtAnnuality.Enabled = true;
            txtFifty.Enabled = true;
            txtOther.Enabled = true;
            txtTotalIncome.Enabled = true;
            txtTotalIncomeMonth.Enabled = true;
            txtIncome.Enabled = true;
            txtValue.Enabled = true;
            txtStudents.Enabled = true;
            txtDifference.Enabled = true;
            btnNew.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
        }

        private void clearControls()
        {
            cboCourse.Items.Clear();
            cboTeacher.Items.Clear();
            txtAnnuality.Text = "";
            txtCode.Text = "";
            txtDesignationHours.Text = "";
            txtDifference.Text = "";
            txtFifty.Text = "";
            txtIncome.Text = "";
            txtOther.Text = "";
            txtSalary.Text = "";
            txtStudents.Text = "";
            txtTotalIncome.Text = "";
            txtTotalIncomeMonth.Text = "";
            txtValue.Text = "";
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                List<Entities.Schedule> listSchedule = ScheduleBLL.getInstance().getAll();
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
                title.Add("\n\n Reporte de Horarios\n\n");
                pdfDoc.Add(title);
                
                PdfPTable oPTable = new PdfPTable(5);
                oPTable.TotalWidth = 100;
                oPTable.SpacingBefore = 20f;
                oPTable.SpacingAfter = 30f;
                oPTable.AddCell("Días");
                oPTable.AddCell("Horarios");
                oPTable.AddCell("Hora de Inicio");
                oPTable.AddCell("Hora de Fin");
                oPTable.AddCell("Estado");

                if (listSchedule.Count > 0)
                {
                    foreach (Entities.Schedule pSchedule in listSchedule)
                    {
                        oPTable.AddCell(pSchedule.name);
                        oPTable.AddCell(pSchedule.typeSchedule);
                        oPTable.AddCell(pSchedule.startTime.ToShortTimeString());
                        oPTable.AddCell(pSchedule.endTime.ToShortTimeString());
                        oPTable.AddCell((pSchedule.state == 1 ? "Activo" : "Inactivo"));
                    }
                }
                else
                {
                    PdfPCell cell = new PdfPCell(new text::Phrase("No existen horarios registrados."));
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
                Response.AddHeader("Content-Disposition", "attachment; filename=Horarios.pdf");
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