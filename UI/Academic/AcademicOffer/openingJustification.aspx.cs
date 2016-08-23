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
        public static int Aoffer =0;
        public bool offerAcademic { get; set; }
        public static Entities.OpeningJustification oJustification;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Verify();
                fillInfomration();
                btnReport.Enabled = false;
            }

        }

        private void fillInfomration()
        {
            oJustification = OpeningJustificationBLL.getInstance().getJustification(Aoffer);
            lblTeacherDescription.Text = oJustification.oAcademic.oteacher.name + " " + oJustification.oAcademic.oteacher.lastName;
            lblCourseDescription.Text = oJustification.oAcademic.oCourse.description;
            lblPositionDescription.Text = oJustification.oInternal.description;
            lblHoursDescription.Text = oJustification.oAcademic.hours.ToString();
            oJustification.getSalary();
            lblSalaryDescription.Text = oJustification.Salary.ToString("0.00");
            oJustification.getCCSS();
            lblCCSSDescription.Text = oJustification.CCSS.ToString("0.00");
            lblPublicityDescription.Text=oJustification.publicity.ToString("0.00");
            lblValueDescription.Text = oJustification.oAcademic.price.ToString("0.00");
        }

        private void Verify()
        {
            try
            {
                Aoffer = Convert.ToInt32(Session["Aoffer"].ToString());
                if (Aoffer == 0)
                {
                    oJustification = null;
                    Response.Redirect("../../index.aspx");
                }
            }
            catch
            {
                oJustification = null;
                Response.Redirect("../../index.aspx");
            }
        }

        protected void showOfferAcademic()
        {
            this.offerAcademic = Convert.ToBoolean(Session["OfferAcademic"].ToString());
            if (this.offerAcademic == false)
            {
                oJustification = null;
                Session["Aoffer"] = null;
                Response.Redirect("../../index.aspx");
            }
        }

        protected void btnReturn_Click(object sender, ImageClickEventArgs e)
        {
            oJustification = null;
            Session["Aoffer"] = null;
            Aoffer = 0;
            Response.Redirect("../AcademicGroups/gAcademicOffer.aspx");
        }


        protected void btnNew_Click(object sender, ImageClickEventArgs e)
        {
            if (getValidates())
            {
                int anuality = Convert.ToInt32(txtAnnuality.Text); 
                double others = Convert.ToDouble(txtOther.Text); ;
                int students = Convert.ToInt32(txtStudents.Text); ;
                oJustification.getCalc(anuality,others,students);
                lblTotalAnualityDescription.Text = oJustification.Anuality.ToString("0.00");
                txtOther.Text=oJustification.Others.ToString("0.00");
                lblTotaTotalMouthDescription.Text=oJustification.TotalMensual.ToString("0.00");
                lblTotalbimensualDescription.Text=oJustification.TotalBimensual.ToString("0.00");
                lblIncomeDescription.Text=oJustification.TotalIncome.ToString("0.00");
                lblDifferenceDescription.Text=oJustification.Diference.ToString("0.00");
                btnReport.Enabled = true;
            }
        }

        private void clearControls()
        {
            txtAnnuality.Text = "";
            txtOther.Text = "";
            txtStudents.Text = "";
        }

        private bool getValidates()
        {
            bool flag = true;
            if (txtAnnuality.Text == "")
            {
                txtAnnuality.Text = "0";
            }
            else
            {
                try
                {
                    Convert.ToInt32(txtAnnuality.Text);
                }
                catch
                {
                    flag = false;
                    lblMessageAnnuality.Text = "Debe digitar la cantidades de Anualidades";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorPrice", "$('#ContentPlaceHolder1_txtAnnuality').addClass('has-error');", true);
                }
            }

            if (txtOther.Text == "")
            {
                txtOther.Text = "0";
            }
            else
            {
                try
                {
                    Convert.ToDouble(txtOther.Text);
                }
                catch
                {
                    flag = false;
                    lblMessageOther.Text = "Debe digitar la cantidade en un formato correcto";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorPrice", "$('#ContentPlaceHolder1_txtOther').addClass('has-error');", true);
                }
            }

            if (txtStudents.Text == "")
            {
                flag = false;
                lblMessageStudents.Text = "Debe digitar la cantidades de estudiantes";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorPrice", "$('#ContentPlaceHolder1_txtStudents').addClass('has-error');", true);
            }
            else
            {
                try
                {
                    Convert.ToInt32(txtStudents.Text);
                }
                catch
                {
                    flag = false;
                    lblMessageStudents.Text = "Debe digitar la cantidades de estudiantes";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorPrice", "$('#ContentPlaceHolder1_txtStudents').addClass('has-error');", true);
                }
            }
            return flag;
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
                title.Add("\n\n Justificación de Apertura\n\n");
                pdfDoc.Add(title);
                
                PdfPTable oPTable = new PdfPTable(2);
                oPTable.TotalWidth = 100;
                oPTable.SpacingBefore = 20f;
                oPTable.SpacingAfter = 30f;
                oPTable.AddCell("Profesor: "+ oJustification.oAcademic.oteacher.name +" "+ oJustification.oAcademic.oteacher.lastName);
                oPTable.AddCell("Curso: "+ oJustification.oAcademic.oCourse.description);
                oPTable.AddCell("Categoría: " + oJustification.oInternal.description);
                oPTable.AddCell("Nombramiento: " + oJustification.oAcademic.hours.ToString());
                oPTable.AddCell("Anualidade: " + oJustification.CauntAnualities);
                oPTable.AddCell("Salario: "+ oJustification.Salary.ToString("0.00"));
                oPTable.AddCell("Anualidades: "+ oJustification.Anuality.ToString("0.00"));
                oPTable.AddCell("Cargos Sociales: "+ oJustification.CCSS.ToString("0.00"));
                oPTable.AddCell("Publicidad: "+ oJustification.publicity.ToString("0.00"));
                oPTable.AddCell("Otros: "+ oJustification.Others.ToString("0.00"));
                oPTable.AddCell("Total Mensual: "+ oJustification.TotalMensual.ToString("0.00"));
                oPTable.AddCell("Tatal Bimensual: " + oJustification.TotalBimensual.ToString("0.00"));
                oPTable.AddCell("Valor del curso: "+ oJustification.oAcademic.price.ToString("0.00"));
                oPTable.AddCell("Estudiantes: "+ oJustification.Students);
                oPTable.AddCell("Ingresos: "+ oJustification.TotalIncome.ToString("0.00"));
                oPTable.AddCell("Diferencia: "+ oJustification.Diference.ToString("0.00"));

                pdfDoc.Add(oPTable);
                pdfDoc.Close();
                
                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=JustificaciónApertura.pdf");
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