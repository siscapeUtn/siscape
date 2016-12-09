using BLL;
using Entities;
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
    public partial class Actives : System.Web.UI.Page
    {
        static UserSystem oUser = null;
        static Int32 actives_id = -1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                blockControls();
                loadUser();
            }
            loadData();
        }


        private void loadUser()
        {
            oUser = (UserSystem)Session["User"];
            if (oUser == null)
            {
                Response.Redirect("../../login.aspx");
            }
        }

        protected void btnNew_Click(object sender, ImageClickEventArgs e)
        {
            loadCombos();
            unlockControls();
            txtCode.Text = ActivesBLL.getInstance().getNextCode().ToString();
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            Int32 records = 0;
            if (validateData())
            {
                Entities.ClassRoom oClassRoom = new Entities.ClassRoom();
                Entities.Program oProgram = new Entities.Program();
                Entities.ActivesStatus oActivesStatus = new Entities.ActivesStatus();
                Entities.Actives oActivesState = new Entities.Actives();
                oActivesState.code = Convert.ToInt32(txtCode.Text.ToString());
                oActivesState.codeAlphaNumeric = txtcodeAlphaNumeric.Text;
                oActivesState.description = txtDescription.Text;
                oClassRoom.code = Convert.ToInt32(cboClassroom.SelectedValue.ToString());
                oProgram.code = Convert.ToInt32(cboprogram.SelectedValue.ToString());
                oActivesStatus.activesSatus_ID = Convert.ToInt32(cboStatus.SelectedValue.ToString());
                oActivesState.status = oActivesStatus;
                oActivesState.oClassRoom = oClassRoom;
                oActivesState.oProgram = oProgram;

                if (ActivesBLL.getInstance().exists(oActivesState.code))
                {
                    records = ActivesBLL.getInstance().modify(oActivesState);
                }
                else
                {
                    if (ActivesBLL.getInstance().existsCodeAlphanumeric(txtcodeAlphaNumeric.Text))
                    {
                        lblMessagecodeAlphaNumeric.Text = "Este codigo ya se ha utilizado";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorcodeAlphaNumeric", "$('#ContentPlaceHolder1_txtcodeAlphaNumeric').removeClass('has-error');", true);
                        return;
                    }
                    else { 
                    records = ActivesBLL.getInstance().insert(oActivesState);
                    }
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

        protected void cboprogram_SelectedIndexChanged(object sender, EventArgs e)
        {
            int cod = Convert.ToInt32(cboprogram.SelectedValue);
            getClassRoom(cod);
        }

        protected void gvActives_RowEditing(object sender, GridViewEditEventArgs e)
        {
            loadCombos();
            unlockControls();
            Int32 code = Convert.ToInt32(gvActives.Rows[e.NewEditIndex].Cells[0].Text);
            Entities.Actives oActivesStatus = ActivesBLL.getInstance().getActive(code);
            getClassRoom(oActivesStatus.oProgram.code);
            txtCode.Text = oActivesStatus.code.ToString();
            txtcodeAlphaNumeric.Text = oActivesStatus.codeAlphaNumeric;
            txtcodeAlphaNumeric.Enabled = false;
            txtDescription.Text = oActivesStatus.description;
            cboprogram.SelectedValue = oActivesStatus.oProgram.code.ToString();
            cboClassroom.SelectedValue = oActivesStatus.oClassRoom.code.ToString();
            cboStatus.SelectedValue = oActivesStatus.status.activesSatus_ID.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "$('html, body').animate({ scrollTop: $('body').offset().top });", true);
        }

        protected void gvActives_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            String locationDescription = gvActives.Rows[e.RowIndex].Cells[1].Text + " - " +
             gvActives.Rows[e.RowIndex].Cells[2].Text;
            actives_id = Convert.ToInt32(gvActives.Rows[e.RowIndex].Cells[0].Text);
            lblActivesdescription.Text = locationDescription;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirmmessage", "$('#confirmmessage').modal();", true);
            confirmmodal.Update();
        }

        protected void btndelete_click(object sender, EventArgs e)
        {
            Int32 records = ActivesBLL.getInstance().delete(actives_id); ;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirmmessage", "$('#confirmmessage').modal('toggle');", true);

            if (records > 0)
            {
                lblMessage.Text = "Activo eliminado correctamente.";
            }
            loadData();
        }

        private void loadCombos()
        {
            loadActivesStatus();
            loadPrograms();
        }

        private void loadData()
        {
            int code = oUser.oProgram.code;
            if (code == 1)
            {
                gvActives.DataSource = ActivesBLL.getInstance().getAll();
            }
            else
            {
                gvActives.DataSource = ActivesBLL.getInstance().getAllByProgram(code);
            }

            gvActives.DataBind();
        }

        private bool validateData()
        {
            clearMssagesj();
            Boolean ind = true;

            if (txtcodeAlphaNumeric.Text.Trim() == "")
            {
                ind = false;
                lblMessagecodeAlphaNumeric.Text = "Debe digitar el código alfanumérico";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorcodeAlphaNumeric", "$('#ContentPlaceHolder1_txtcodeAlphaNumeric').removeClass('has-error');", true);
            } 
        
            if (txtDescription.Text.Trim() == "")
            {
                ind = false;
                lblMessageDescription.Text = "Debe digitar la descripción del activo";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorDescription", "$('#ContentPlaceHolder1_txtDescription').addClass('has-error');", true);
            }
            if (Convert.ToInt32(cboprogram.SelectedValue) == 0)
            {
                ind = false;
                lblmessageprogram.Text = "Debe seleccionar un programa";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorprogram", "$('#ContentPlaceHolder1_lblprogram').removeClass('has-error');", true);
            }
            try
            {
                if (Convert.ToInt32(cboClassroom.SelectedValue) == 0)
                {
                    ind = false;
                    lblMesageClassroom.Text = "Debe seleccionar un aula";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorClassroom", "$('#ContentPlaceHolder1_cboClassroom').removeClass('has-error');", true);
                }
            }
            catch (Exception)
            {
                ind = false;
                lblMesageClassroom.Text = "Debe seleccionar un aula";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorClassroom", "$('#ContentPlaceHolder1_cboClassroom').removeClass('has-error');", true);
            }
            try
            {
                if (Convert.ToInt32(cboStatus.SelectedValue) == 0)
                {
                    ind = false;
                    lblMessageStatus.Text = "Debe seleccionar el estatus del activo";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorStatus", "$('#ContentPlaceHolder1_cboStatus').removeClass('has-error');", true);
                }
            }
            catch (Exception)
            {
                ind = false;
                lblMessageStatus.Text = "Debe seleccionar un aula";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorStatus", "$('#ContentPlaceHolder1_cboStatus').removeClass('has-error');", true);
            }
            return ind;
        }

        protected void loadActivesStatus()
        {
            cboStatus.Items.Clear();
            List<Entities.ActivesStatus> listActivesStatus = new List<Entities.ActivesStatus>();
            listActivesStatus = ActivesStatusBLL.getInstance().getAll();
            ListItem oItemS = new ListItem("---- Seleccione ----", "0");
            cboStatus.Items.Add(oItemS);
            foreach (Entities.ActivesStatus oActivesStatus in listActivesStatus)
            {
                ListItem oItem = new ListItem(oActivesStatus.description, oActivesStatus.activesSatus_ID.ToString());
                cboStatus.Items.Add(oItem);
            }

        }

        protected void loadPrograms()
        {
            cboprogram.Items.Clear();
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

        public void getClassRoom(int cod)
        {
            cboClassroom.Items.Clear();
            List<Entities.ClassRoom> listClassRoom = new List<Entities.ClassRoom>();
            listClassRoom = BLL.ClassRoomBLL.getInstance().getAllByProgram(cod);
            ListItem oItemS = new ListItem("---- Seleccione ----", "0");
            cboClassroom.Items.Add(oItemS);
            foreach (Entities.ClassRoom olistClassRoom in listClassRoom)
            {
                ListItem oItem = new ListItem(olistClassRoom.num_room + " (" + olistClassRoom.oLocation.oHeadquarters.description + " " + olistClassRoom.oLocation.building + " " + olistClassRoom.oLocation.module + ") ", olistClassRoom.code.ToString());
                cboClassroom.Items.Add(oItem);
            }
            cboClassroom.Enabled = true;
        }

        protected void cboProgramValue()
        {
            if (oUser.oProgram.code != 1)
            {
                cboprogram.SelectedValue = oUser.oProgram.code.ToString();
                getClassRoom(oUser.oProgram.code);
            }
        }
        protected void unlockControls()
        {
            clearControls();
            txtCode.Enabled = true;
            txtDescription.Enabled = true;
            txtcodeAlphaNumeric.Enabled = true;
            if (oUser.oProgram.code == 1)
            {
                cboprogram.Enabled = true;
            }
            else
            {
                cboProgramValue();
            }
            cboStatus.Enabled = true;
            btnNew.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
        }

        protected void blockControls()
        {
            txtCode.Enabled = false;
            cboprogram.Enabled = false;
            txtDescription.Enabled = false;
            txtcodeAlphaNumeric.Enabled = false;
            cboClassroom.Enabled = false;
            cboStatus.Enabled = false;
            btnNew.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            clearControls();
        }

        protected void clearControls()
        {
            txtCode.Text = "";
            txtDescription.Text = "";
            txtcodeAlphaNumeric.Text = "";
            cboStatus.SelectedValue = "0";
            cboprogram.SelectedValue = "0";
            lblMessage.Text = "";
            cboClassroom.SelectedValue = "0";
            clearMssagesj();
        }

        private void clearMssagesj()
        {
            lblMessagecodeAlphaNumeric.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorcodeAlphaNumeric", "$('#ContentPlaceHolder1_txtcodeAlphaNumeric').removeClass('has-error');", true);
            lblMessageDescription.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorDescription", "$('#ContentPlaceHolder1_txtDescription').removeClass('has-error');", true);
            lblMesageClassroom.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorClassroom", "$('#ContentPlaceHolder1_cboClassroom').removeClass('has-error');", true);
            lblMessageStatus.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorStatus", "$('#ContentPlaceHolder1_cboStatus').removeClass('has-error');", true);
            lblmessageprogram.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorprogram", "$('#ContentPlaceHolder1_lblprogram').removeClass('has-error');", true);
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                int code = oUser.oProgram.code;
                List<Entities.Actives> listActives;
                if (code == 1)
                {
                    listActives = ActivesBLL.getInstance().getAll();
                }
                else
                {
                    listActives = ActivesBLL.getInstance().getAllByProgram(code);
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
                title.Add("\n\n Reporte de Activos\n\n");
                pdfDoc.Add(title);

                PdfPTable oPTable = new PdfPTable(5);
                oPTable.TotalWidth = 100;
                oPTable.SpacingBefore = 20f;
                oPTable.SpacingAfter = 30f;
                oPTable.AddCell("Código");
                oPTable.AddCell("Detalle");
                oPTable.AddCell("Aula");
                oPTable.AddCell("Prográma");
                oPTable.AddCell("Estado");


                if (listActives.Count > 0)
                {
                    foreach (Entities.Actives pActives in listActives)
                    {
                        oPTable.AddCell(pActives.codeAlphaNumeric);
                        oPTable.AddCell(pActives.description);
                        oPTable.AddCell(pActives.oClassRoom.num_room);
                        oPTable.AddCell(pActives.oProgram.name);
                        oPTable.AddCell(pActives.status.description);
                    }
                }
                else
                {
                    PdfPCell cell = new PdfPCell(new text::Phrase("No existen Activos registrados."));
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
                Response.AddHeader("Content-Disposition", "attachment; filename=Activos.pdf");
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