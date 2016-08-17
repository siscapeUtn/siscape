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
using Entities;

namespace UI.Academic.AcademicOffer
{
    public partial class schedule : System.Web.UI.Page
    {
        public bool offerAcademic { get; set; }
        static Int32 schedule_id = -1;
        private static string codDays = "";
        static UserSystem oUser = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                blockControls();
                loadUser();
                loadPrograms();
            }
            loadData();
        }

        private void loadUser()
        {
            oUser = (UserSystem)Session["User"];
            if (oUser == null)
            {
                Response.Redirect("../../index.aspx");
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

        protected void btnNew_Click(object sender, ImageClickEventArgs e)
        {
            unlockControls();
            txtCode.Text = ScheduleBLL.getInstance().getNextCode().ToString();
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            Int32 records = -1;
            if (validateData())
            {
                Entities.Schedule oSchedule = new Entities.Schedule();
                oSchedule.code = Convert.ToInt32(txtCode.Text);
                oSchedule.name = txtDescription.Text;
                oSchedule.typeSchedule = cboTypeSchedule.SelectedValue;
                oSchedule.startTime = Convert.ToDateTime(txtStart.Text);
                oSchedule.endTime = Convert.ToDateTime(txtEndHour.Text);
                oSchedule.state = Convert.ToInt32(cboState.SelectedValue);
                oSchedule.oProgram.code = Convert.ToInt32(cboprogram.SelectedValue);
                oSchedule.codday = codDays;

                if (ScheduleBLL.getInstance().exists(oSchedule.code))
                {
                    records = ScheduleBLL.getInstance().modify(oSchedule);
                }
                else
                {
                    records = ScheduleBLL.getInstance().insert(oSchedule);
                }

                blockControls();
                loadData();

                if (records > 0)
                {
                    lblMessage.Text = "Datos almacenados correctamente.";
                }
            }
            //no c para que es esto
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "comboBox", "comboBox();", true);
            }
        }

        protected void btnCancel_Click(object sender, ImageClickEventArgs e)
        {
           blockControls();
        }

        protected void btnReturn_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("../AcademicGroups/gAcademicOffer.aspx");
        }

        protected void loadData()
        {
            int code = oUser.oProgram.code;
            if (code == 1)
            {
                gvSchedule.DataSource = ScheduleBLL.getInstance().getAll();
            }
            else
            {
                gvSchedule.DataSource = ScheduleBLL.getInstance().getAllByPrgrams(code);
            }
            
            gvSchedule.DataBind();
        }

        protected void loadPrograms()
        {
            List<Entities.Program> listPrograms = new List<Entities.Program>();
            listPrograms = ProgramBLL.getInstance().getAllActived();
            ListItem oItemS = new ListItem("---- Seleccione ----", "0");
            cboprogram.Items.Add(oItemS);
            foreach (Entities.Program oProgram in listPrograms)
            {
                ListItem oItem = new ListItem(oProgram.name, oProgram.code.ToString());
                cboprogram.Items.Add(oItem);
            }
            cboProgramValue();
        }

        protected void cboProgramValue()
        {
            if (oUser.oProgram.code != 1)
            {
                cboprogram.SelectedValue = oUser.oProgram.code.ToString();
            }
        }
        protected Boolean validateData()
        {
           Boolean ind = true;
            /* 
                        if (Convert.ToInt32(cboHeadquarters.SelectedValue) == 0)
                        {
                            ind = false;
                            lblMessageHeadquarters.Text = "Debe seleccionar una sede.";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorHeadquarter", "$('#ContentPlaceHolder1_cboHeadquarters').addClass('has-error');", true);
                        }
                        else
                        {
                            lblMessageHeadquarters.Text = "";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorHeadquarter", "$('#ContentPlaceHolder1_cboHeadquarters').removeClass('has-error');", true);
                        }

                        if (txtBuilding.Text.Trim() == "")
                        {
                            ind = false;
                            lblMessageBuilding.Text = "Debe digitar un nombre de edificio.";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorBuilding", "$('#ContentPlaceHolder1_txtBuilding').addClass('has-error');", true);
                        }
                        else
                        {
                            lblMessageBuilding.Text = "";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorBuilding", "$('#ContentPlaceHolder1_txtBuilding').removeClass('has-error');", true);
                        }

                        if (txtModule.Text.Trim() == "")
                        {
                            ind = false;
                            lblMessageModule.Text = "Debe digitar el nombre de un modulo.";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorModule", "$('#ContentPlaceHolder1_txtModule').addClass('has-error');", true);
                        }
                        else
                        {
                            lblMessageModule.Text = "";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorModule", "$('#ContentPlaceHolder1_txtModule').removeClass('has-error');", true);
                        }

                      */
           return ind;
        }

        protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
        {
           String days = "";
            codDays = "";
            foreach (ListItem itemActual in chkld.Items)
            {

                if (itemActual.Selected)
                {
                    days = days + itemActual.Text;
                    codDays = codDays + itemActual.Value;
                    days = days + ",";
                }

            }
            txtDescription.Text = days;
        }



        protected void gvSchedule_RowEditing(object sender, GridViewEditEventArgs e)
        {
            unlockControls();
            Int32 code = Convert.ToInt32(gvSchedule.Rows[e.NewEditIndex].Cells[0].Text);
            Entities.Schedule oSchedule = ScheduleBLL.getInstance().getSchedule(code);
            txtCode.Text = oSchedule.code.ToString();
            txtDescription.Text = oSchedule.name;
            cboTypeSchedule.SelectedValue = oSchedule.typeSchedule;
            txtStart.Text = String.Format("{0:t}", oSchedule.startTime);
            txtEndHour.Text = String.Format("{0:t}", oSchedule.endTime);
            cboState.SelectedValue = oSchedule.state.ToString();
            SelectchkDays(oSchedule.codday);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "$('html, body').animate({ scrollTop: $('body').offset().top });", true);
        }

        protected void gvSchedule_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
           String locationDescription = gvSchedule.Rows[e.RowIndex].Cells[1].Text + " - " +
            gvSchedule.Rows[e.RowIndex].Cells[2].Text;
            schedule_id = Convert.ToInt32(gvSchedule.Rows[e.RowIndex].Cells[0].Text);
            lblScheduleDescription.Text = locationDescription;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirmMessage", "$('#confirmMessage').modal();", true);
            confirmModal.Update();
       }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Int32 records = ScheduleBLL.getInstance().delete(schedule_id);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirmMessage", "$('#confirmMessage').modal('toggle');", true);

            if (records > 0)
            {
                lblMessage.Text = "Horario eliminado correctamente.";
            }
            loadData();
        }

        protected void blockControls()
        {
            clearControls();
            chkld.Enabled = false;
            cboprogram.Enabled = false;
            txtCode.Enabled = false;
            txtDescription.Enabled = false;
            cboTypeSchedule.Enabled = false;
            cboState.Enabled = false;
            txtStart.Enabled = false;
            txtEndHour.Enabled = false;
            btnNew.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
        }

        protected void unlockControls()
        {
            txtCode.Enabled = false;
            chkld.Enabled = true;
            if (oUser.oProgram.code == 1)
            {
                cboprogram.Enabled = true;
            }
            else
            {
                cboProgramValue();
            }
            txtDescription.Enabled = false;
            txtStart.Enabled = true;
            txtEndHour.Enabled = true;
            cboTypeSchedule.Enabled = true;
            cboState.Enabled = true;
            btnNew.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            clearControls();
        }

        protected void clearControls()
        {
            codDays = "";
            txtCode.Text = "";
            txtDescription.Text = "";
            txtStart.Text = "";
            txtEndHour.Text = "";
            cboState.SelectedValue = "1";
            lblMessage.Text = "";
            cleanUpchkDays();
            cboTypeSchedule.SelectedValue = "Mañana";
            lblMessageDescription.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorName", "$('#ContentPlaceHolder1_txtDescription').removeClass('has-error');", true);
            lblMessageTypeSchedule.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorName", "$('#ContentPlaceHolder1_cboTypeSchedule').removeClass('has-error');", true);
            lblMessageStartHour.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorName", "$('#ContentPlaceHolder1_txtStart').removeClass('has-error');", true);
            lblMessageEndHour.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorName", "$('#ContentPlaceHolder1_txtEndHour').removeClass('has-error');", true);
            cboprogram.SelectedValue = "0";
            lblmessageprogram.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorProgram", "$('#ContentPlaceHolder1_cboProgram').removeClass('has-error');", true);
        }

        //This method work for clean up the list of checkbox
        private void cleanUpchkDays()
        {
            foreach (ListItem itemActual in chkld.Items)
            {
                itemActual.Selected = false;
            }
        }

        private void SelectchkDays(string oCodDays)
        {
            int days = oCodDays.Trim().Length;
            int[] array=selectDays(oCodDays);
            int i=1;
            int j = 1;
            foreach (ListItem itemActual in chkld.Items)
            {
                
                if (j == array[i - 1])
                {
                    itemActual.Selected = true;
                    i++;
                }
                j++;
            }
        }

        private int[] selectDays(string days)
        {
            int[] save = new int[7];
            int aument = 0;
            while (6 >= aument)
            {
                if (days.Trim().Length <= aument)
                {
                    save[aument] = 9;
                }
                else
                {
                    string output = days.Substring(aument, 1);
                    save[aument] = Convert.ToInt32(output);
                }
                aument++;
            }

            return save;
        }

        protected void gvSchedule_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSchedule.PageIndex = e.NewPageIndex;
            loadData();
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                int code = oUser.oProgram.code;
                List<Entities.Schedule> listSchedule;
                if (code == 1)
                {
                    listSchedule = ScheduleBLL.getInstance().getAll();
                }
                else
                {
                    listSchedule = ScheduleBLL.getInstance().getAllByPrgrams(code);
                }
                 
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
                oPTable.AddCell("Programa");
                oPTable.AddCell("Estado");

                if (listSchedule.Count > 0)
                {
                    foreach (Entities.Schedule pSchedule in listSchedule)
                    {
                        oPTable.AddCell(pSchedule.name);
                        oPTable.AddCell(pSchedule.typeSchedule);
                        oPTable.AddCell(pSchedule.startTime.ToShortTimeString());
                        oPTable.AddCell(pSchedule.endTime.ToShortTimeString());
                        oPTable.AddCell(pSchedule.oProgram.name);
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