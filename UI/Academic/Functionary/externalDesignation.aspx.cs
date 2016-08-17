using BLL;
using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using text = iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.IO;

namespace UI.Academic.Functionary
{
    public partial class externalDesignation : System.Web.UI.Page
    {
        static Int32 externalDesignation_id = -1;
        DataTable oDataTable = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
               // Session.RemoveAll();
                blockControls();
            }
            fillGvDesignation();
        }


        protected void fillGvDesignation()
        {
            gvExternalDesignation.DataSource = ExternalDesignationBLL.getInstance().getAll();
            gvExternalDesignation.DataBind();
        }
        /*Trae los dias de la semana*/
        protected void loadDay()
        {
            List<Entities.Day> listDays = new List<Entities.Day>();
            listDays = DayBLL.getInstance().getAll();
            ListItem oItemS = new ListItem("---Seleccione---", "0");
            cboDay.Items.Add(oItemS);
            foreach (Entities.Day oDay in listDays)
            {
                ListItem oItem = new ListItem(oDay.description, oDay.code.ToString());
                cboDay.Items.Add(oItem);
            }
        }
        /*Trae los profesores*/
        public void getFunctionary()
        {
            List<Entities.Teacher> listTeacher = new List<Entities.Teacher>();
            listTeacher = TeacherBLL.getInstance().getAllActive();
            ListItem oItemS = new ListItem("---Seleccione---", "0");
            cboFunctionary.Items.Add(oItemS);
            foreach (Entities.Teacher oTeacher in listTeacher)
            {
                ListItem oItem = new ListItem(oTeacher.name, oTeacher.code.ToString());
                cboFunctionary.Items.Add(oItem);
            }
            ListItem oItemS2 = new ListItem("---Seleccione---", "0");
            cboHoursDisignation.Items.Add(oItemS2);
        }
        /*Metodo que llama al obtener horas en external designation bll, llena el combo de dias con las horas restanes*/
        public void getHoursTeacher()
        {
           
        }

        /*Este metodo simplemente llama al obtener horas del profesor*/
        protected void cboFunctionary_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int32 hours = 0;
            cboHoursDisignation.Items.Clear();
            hours = ExternalDesignationBLL.getInstance().getHours(Int32.Parse(cboFunctionary.SelectedValue));
            ListItem oItemS = new ListItem("---Seleccione---", "0");
            cboHoursDisignation.Items.Add(oItemS);
            int i = hours/5;
            for(int j = 5; j<=hours; j=j+5){
                 ListItem oItem = new ListItem(j.ToString()+ " horas",j.ToString());
                 cboHoursDisignation.Items.Add(oItem);
            }
        }

        /*Este metodo obtiene los datos del formulario y los envia al BLL para agregarlos a la bd*/
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            

            Int32 records = -1;

            if (validateData())
            {
                ExternalDesignation oExternalDesignation = new ExternalDesignation();
                oExternalDesignation.code = Int32.Parse(txtCode.Text);
                oExternalDesignation.oTeacher = new Teacher();
                oExternalDesignation.oTeacher.code = Int32.Parse(cboFunctionary.SelectedValue);
                oExternalDesignation.position = txtPosition.Text;
                oExternalDesignation.location = txtWorkPlace.Text;
                string[] formats = {"M/d/yyyy h:mm:ss tt", "M/d/yyyy h:mm tt", 
                         "MM/dd/yyyy hh:mm:ss", "M/d/yyyy h:mm:ss", 
                         "M/d/yyyy hh:mm tt", "M/d/yyyy hh tt", 
                         "M/d/yyyy h:mm", "M/d/yyyy h:mm", 
                         "MM/dd/yyyy hh:mm", "M/dd/yyyy hh:mm"};
                DateTime initial;
                /*Este if convierte el formato de la fecha que toma del usuario de mes/dia/año a  dia/mes/año*/
                if (DateTime.TryParseExact(txtStartDesignation.Text + " 00:00:00", formats, new CultureInfo("es-ES"),
                                   DateTimeStyles.None, out initial))
                {
                    oExternalDesignation.initial_day = initial;
                }
                DateTime final;
                if (DateTime.TryParseExact(txtEndDesignation.Text + " 00:00:00", formats, new CultureInfo("es-ES"),
                                  DateTimeStyles.None, out final))
                {
                    oExternalDesignation.final_day = final;
                }

                oExternalDesignation.hours = Int32.Parse(cboHoursDisignation.SelectedValue);
                oExternalDesignation.journeys = (List<Entities.Journey>)Session["listDesignation"];

                if (ExternalDesignationBLL.getInstance().exists(oExternalDesignation.code))
                {
                    //if we want implement modify
                    //records = ExternalDesignationBLL.getInstance().modify(oExternalDesignation);
                }
                else
                {
                    records = ExternalDesignationBLL.getInstance().insert(oExternalDesignation);
                }

                blockControls();
                fillGvDesignation();
                Session.RemoveAll();
                Response.Redirect("externalDesignation.aspx");
                if (records > 0)
                {
                    lblMessage.Text = "Datos almacenados correctamente.";
                }
            }
        }
        /*Este metodo desbloque los controloes y obtiene el codigo siguiente del nombramiento externo*/
        protected void btnNew_Click(object sender, ImageClickEventArgs e)
        {
            unlockControls();
            loadDay();
            getFunctionary();
            txtCode.Text = ExternalDesignationBLL.getInstance().getNextCode().ToString();
        }

        protected void btnCancel_Click(object sender, ImageClickEventArgs e)
        {
            blockControls();
        }

        protected void btnReturn_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("../AcademicGroups/gFunctionary.aspx");
        } 

        /*Obtiene los datos de los campos y crea o agrega el dato al list en la session*/
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (validateDataAdd())
            {
                Int32 code = Convert.ToInt32(cboDay.SelectedValue.ToString());
                String day = cboDay.SelectedItem.Text;
                String hourStart = txtStart.Text.ToString();
                String hourFinish = txtEnd.Text.ToString();
                List<Entities.Journey> list = new List<Entities.Journey>();
                Entities.Journey designation = new Entities.Journey(code, day, hourStart, hourFinish);

                if (Session["listDesignation"] != null)
                {
                    list = (List<Entities.Journey>)Session["listDesignation"];
                }
                list.Add(designation);
                Session["listDesignation"] = list;
                fillGv();
                //cboDay.SelectedItem.Attributes.Remove(cboDay.SelectedValue.ToString());
                lblMsjHours.Text = "";
            }
        }

        /*LLenar la tabla de dias con el list de dias en la session*/
        protected void fillGv()
        {
            List<Entities.Journey> list = new List<Entities.Journey>();
            if (Session["listDesignation"] != null)
            {
                list = (List<Entities.Journey>)Session["listDesignation"];
            }
            DataColumn code = oDataTable.Columns.Add("code", typeof(int));
            DataColumn day = oDataTable.Columns.Add("day", typeof(string));
            DataColumn startHour = oDataTable.Columns.Add("startHour", typeof(string));
            DataColumn FinishHour = oDataTable.Columns.Add("FinishHour", typeof(string));

            DataRow oDataRow;
            for (int i = 0; i < list.Count; i++)
            {
                oDataRow = oDataTable.NewRow();
                oDataRow["code"] = list[i].day.code;
                oDataRow["day"] = list[i].day.description;
                oDataRow["startHour"] = list[i].start;
                oDataRow["FinishHour"] = list[i].finish;
                oDataTable.Rows.Add(oDataRow);
            }
            grvDias.DataSource = oDataTable;
            grvDias.DataBind();
        }
        /*Elimina la fila en la tabla temporal de dias*/
        protected void grvDias_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int code = Convert.ToInt32(grvDias.Rows[e.RowIndex].Cells[0].Text);
            List<Entities.Journey> list = new List<Entities.Journey>();
            if (Session["listDesignation"] != null)
            {
                list = (List<Entities.Journey>)Session["listDesignation"];

                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].day.code == code)
                    {
                        list.RemoveAt(i);
                        break;
                    }
                }
                Session["listDesignation"] = list;
                fillGv();
            }
        }

        private bool validateDataAdd()
        {
            //lblMsjHours.Text = "";
            bool bandera = true;
            //evalua que el combo no este en seleccione
            if (cboDay.SelectedValue.ToString() == "0")
            {
                bandera = false;
            }
            //evalua que el campo de la hora inicial no este vacio
            if (txtStart.Text.Equals(""))
            {
                bandera = false;
            }
            //evalua que el campo de la hora final no este vacio
            if (txtEnd.Text.Equals(""))
            {
                bandera = false;
            }

            List<Entities.Journey> list = new List<Entities.Journey>();
            if (Session["listDesignation"] != null)
            {
                list = (List<Entities.Journey>)Session["listDesignation"];

                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].day.code == Convert.ToInt32(cboDay.SelectedValue.ToString()))
                    {
                        lblMsjHours.Text = "** Este día ya se ingresó.";
                        bandera = false;
                        break;
                    }
                }
            }


            return bandera;
        }

        protected Boolean validateData()
        {
            Boolean ind = true;
            /*************************************/
            string[] formats = {"M/d/yyyy h:mm:ss tt", "M/d/yyyy h:mm tt", 
                         "MM/dd/yyyy hh:mm:ss", "M/d/yyyy h:mm:ss", 
                         "M/d/yyyy hh:mm tt", "M/d/yyyy hh tt", 
                         "M/d/yyyy h:mm", "M/d/yyyy h:mm", 
                         "MM/dd/yyyy hh:mm", "M/dd/yyyy hh:mm"};
            if (txtPosition.Text.Trim() == "")
            {
                ind = false;
                //lblMessageDescription.Text = "Debe digitar una descripción correcta.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "addHasErrorPosition", "$('#ContentPlaceHolder1_txtPosition').addClass('has-error');", true);
            }
            else
            {
                //lblMessageDescription.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorPosition", "$('#ContentPlaceHolder1_txtPosition').removeClass('has-error');", true);
            }
            /*************************************/
            if (txtWorkPlace.Text.Trim() == "")
            {
                ind = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "addHasErrorWorkPlace", "$('#ContentPlaceHolder1_txtWorkPlace').addClass('has-error');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorWorkPlace", "$('#ContentPlaceHolder1_txtWorkPlace').removeClass('has-error');", true);
            }
            /*************************************/
            try
            {
               
                DateTime initial;
                if (DateTime.TryParseExact(txtStartDesignation.Text + " 00:00:00", formats, new CultureInfo("es-ES"),
                                   DateTimeStyles.None, out initial))
                {}
                //lblMessageSalary.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorStartDesignation", "$('#ContentPlaceHolder1_txtStartDesignation').removeClass('has-error');", true);
            }
            catch (Exception)
            {
                ind = false;
                //lblMessageSalary.Text = "Debe digitar un salario correcto.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "addHasErrorStartDesignation", "$('#ContentPlaceHolder1_txtStartDesignation').addClass('has-error');", true);
            }
            /*************************************/
            try
            {
                DateTime final;
                if (DateTime.TryParseExact(txtEndDesignation.Text + " 00:00:00", formats, new CultureInfo("es-ES"),
                                  DateTimeStyles.None, out final))
                {}
                //lblMessageSalary.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorEndDesignation", "$('#ContentPlaceHolder1_txtEndDesignation').removeClass('has-error');", true);
            }
            catch (Exception)
            {
                ind = false;
                //lblMessageSalary.Text = "Debe digitar un salario correcto.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "addHasErrorEndDesignation", "$('#ContentPlaceHolder1_txtEndDesignation').addClass('has-error');", true);
            }
            /*************************************/
            try
            {
                Convert.ToInt32(cboHoursDisignation.SelectedValue);
                //lblMessageAnnuality.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorHoursDisignation", "$('#ContentPlaceHolder1_txtHoursDisignation').removeClass('has-error');", true);
            }
            catch (Exception)
            {
                ind = false;
                //lblMessageAnnuality.Text = "Debe digitar una anualidad correcta.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "addHasErrorHoursDisignation", "$('#ContentPlaceHolder1_txtHoursDisignation').addClass('has-error');", true);
            }
            //List<Entities.Journey> list = new List<Entities.Journey>();
            if (Session["listDesignation"] == null)
            {
                ind = false;  

            }
            return ind;
        }

        protected void gvExternalDesignation_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            String externalDesignationDescription = gvExternalDesignation.Rows[e.RowIndex].Cells[1].Text;
            externalDesignation_id = Convert.ToInt32(gvExternalDesignation.Rows[e.RowIndex].Cells[0].Text);
            lblExternalDesignationDescription.Text = externalDesignationDescription;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirmMessage", "$('#confirmMessage').modal();", true);
            confirmModal.Update();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Int32 records = ExternalDesignationBLL.getInstance().delete(externalDesignation_id);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirmMessage", "$('#confirmMessage').modal('toggle');", true);

            if (records > 0)
            {
                lblMessage.Text = "Nombramiento externo eliminado correctamente.";
            }
            Response.Redirect("externalDesignation.aspx");
            fillGvDesignation();
        }

        public void blockControls()
        {
            txtCode.Enabled = false;
            cboFunctionary.Enabled = false;
            txtPosition.Enabled = false;
            txtWorkPlace.Enabled = false;
            txtStartDesignation.Enabled = false;
            txtEndDesignation.Enabled = false;
            cboHoursDisignation.Enabled = false;
            cboDay.Enabled = false;
            txtStart.Enabled = false;
            txtEnd.Enabled = false;

            btnAdd.Enabled = false;

            btnNew.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            clearControls();
        }

        public void unlockControls()
        {
            txtCode.Enabled = false;
            cboFunctionary.Enabled = true;
            txtPosition.Enabled = true;
            txtWorkPlace.Enabled = true;
            txtStartDesignation.Enabled = true;
            txtEndDesignation.Enabled = true;
            cboHoursDisignation.Enabled = true;
            cboDay.Enabled = true;
            txtStart.Enabled = true;
            txtEnd.Enabled = true;

            btnAdd.Enabled = true;

            btnNew.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;

            clearControls();
        }

        protected void clearControls()
        {
            txtCode.Text = "";
            cboDay.Items.Clear();
            cboFunctionary.Items.Clear();
            txtPosition.Text = "";
            txtWorkPlace.Text = "";
            txtStartDesignation.Text = "";
            txtEndDesignation.Text = "";
            txtEnd.Text = "";
            txtStart.Text = "";
            cboHoursDisignation.SelectedIndex = -1;
        }

        protected void gvExternalDesignation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvExternalDesignation.PageIndex = e.NewPageIndex;
            fillGvDesignation();
        }

        protected void btnReport_Click(object sender, EventArgs e)
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
                
                PdfPTable oPTable = new PdfPTable(5);
                oPTable.TotalWidth = 100;
                oPTable.SpacingBefore = 20f;
                oPTable.SpacingAfter = 30f;
                oPTable.AddCell("Funcionario");
                oPTable.AddCell("Cargo");
                oPTable.AddCell("Lugar de Trabajo");
                oPTable.AddCell("Inicio - Fin");
                oPTable.AddCell("Horas");

                if (listExternalDesignation.Count > 0)
                {
                    foreach (Entities.ExternalDesignation pExternalDesignation in listExternalDesignation)
                    {
                        oPTable.AddCell(pExternalDesignation.oTeacher.name + " " + pExternalDesignation.oTeacher.lastName );
                        oPTable.AddCell(pExternalDesignation.journeys.ToString());
                        oPTable.AddCell(pExternalDesignation.location);
                        oPTable.AddCell(pExternalDesignation.initial_day.ToShortDateString() + " " + pExternalDesignation.final_day.ToShortDateString());
                        oPTable.AddCell(pExternalDesignation.hours.ToString());
                    }
                }
                else
                {
                    PdfPCell cell = new PdfPCell(new text::Phrase("No existen nombramientos externos registrados."));
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