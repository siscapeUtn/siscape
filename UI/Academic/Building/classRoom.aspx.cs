using BLL;
using Entities;
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

namespace UI.Academic
{
    public partial class classRoom : System.Web.UI.Page
    {
        static Int32 classRoom_id = -1;
        static UserSystem oUser=null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                blockControls();
                loadClassroomType();
                loadUser();
                loadPrograms();
                loadLocation();
            }
            loadData();
        }

        private void loadUser()
        {
            oUser =(UserSystem)Session["User"];
            if (oUser == null)
            {
                Response.Redirect("../../login.aspx");
            }
        }

        protected void btnNew_Click(object sender, ImageClickEventArgs e)
        {
            unlockControls();
            txtCode.Text = ClassRoomBLL.getInstance().getNextCode().ToString();
        }
        
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            Int32 records = 0;
            if (validateData())
            {
                Entities.ClassRoom oClassRoom = new Entities.ClassRoom();
                Entities.ClassRoomsType oClassRoomsType = new Entities.ClassRoomsType();
                Entities.Location oLocation = new Entities.Location();
                Entities.Program oProgram = new Entities.Program();
                oClassRoom.code = Convert.ToInt32(txtCode.Text);
                oClassRoom.num_room = txtDescription.Text;
                oClassRoomsType.code = Convert.ToInt32(cboClassRoomType.SelectedValue);
                oLocation.code = Convert.ToInt32(cboLocation.SelectedValue);
                oClassRoom.size = Convert.ToInt32(txtSize.Text);
                oProgram.code = Convert.ToInt32(cboprogram.SelectedValue);
                oClassRoom.state = Convert.ToInt16(cboState.SelectedValue);
                oClassRoom.oClassRoomsType = oClassRoomsType;
                oClassRoom.oLocation = oLocation;
                oClassRoom.oProgram = oProgram;

                if (ClassRoomBLL.getInstance().exists(oClassRoom.code))
                {
                    records = ClassRoomBLL.getInstance().modify(oClassRoom);
                }
                else
                {
                    records = ClassRoomBLL.getInstance().insert(oClassRoom);
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

        protected void gvClassRoom_RowEditing(object sender, GridViewEditEventArgs e)
        {
            unlockControls();
            Int32 code = Convert.ToInt32(gvClassRoom.Rows[e.NewEditIndex].Cells[0].Text);
            Entities.ClassRoom oClassRoom = ClassRoomBLL.getInstance().getClassRoom(code);
            txtCode.Text = oClassRoom.code.ToString();
            txtDescription.Text = oClassRoom.num_room;
            txtSize.Text = oClassRoom.size.ToString();
            cboState.SelectedValue = oClassRoom.state.ToString();

            try
            {
                cboClassRoomType.SelectedValue = oClassRoom.oClassRoomsType.code.ToString();
            }
            catch(Exception )
            {
                cboClassRoomType.SelectedValue = "0";
            }

            try
            {
                cboLocation.SelectedValue = oClassRoom.oLocation.code.ToString();
            }
            catch (Exception )
            {
                cboLocation.SelectedValue = "0";    
            }

            try
            {
                cboprogram.SelectedValue = oClassRoom.oProgram.code.ToString();
            }
            catch (Exception )
            {
                cboprogram.SelectedValue = "0";
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "$('html, body').animate({ scrollTop: $('body').offset().top });", true);
        }

        protected void gvClassRoom_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            String classRoomDescription = gvClassRoom.Rows[e.RowIndex].Cells[1].Text;
            classRoom_id = Convert.ToInt32(gvClassRoom.Rows[e.RowIndex].Cells[0].Text);
            lblClassRoomDescription.Text = classRoomDescription;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirmMessage", "$('#confirmMessage').modal();", true);
            confirmModal.Update();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Int32 records = ClassRoomBLL.getInstance().delete(classRoom_id);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirmMessage", "$('#confirmMessage').modal('toggle');", true);

            if (records > 0)
            {
                lblMessage.Text = "Sede eliminada correctamente.";
            }
            loadData();
        }

        protected void loadData()
        {
            gvClassRoom.DataSource = ClassRoomBLL.getInstance().getAll();
            gvClassRoom.DataBind();
        }

        protected Boolean validateData()
        {
            Boolean ind = true;

            if (txtDescription.Text.Trim() == "")
            {
                ind = false;
                lblMessageDescription.Text = "Debe digitar el código del aula";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorDescription", "$('#ContentPlaceHolder1_txtDescription').addClass('has-error');", true);
            }
            else
            {
                lblMessageDescription.Text = "";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorDescription", "$('#ContentPlaceHolder1_txtDescription').removeClass('has-error');", true);
            }

            if (Convert.ToInt32(cboClassRoomType.SelectedValue) == 0)
            {
                ind = false;
                lblMessageClassRoomType.Text = "Debe seleccionar un tipo de aula";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorClassRoomType", "$('#ContentPlaceHolder1_cboClassRoomType').addClass('has-error');", true);
            }
            else
            {
                lblMessageClassRoomType.Text = "";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorClassRoomType", "$('#ContentPlaceHolder1_cboClassRoomType').removeClass('has-error');", true);
            }

            if ( Convert.ToInt32(cboLocation.SelectedValue) == 0 )
            {
                ind = false;
                lblMesageLocation.Text = "Debe seleccionar una locación";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorLocation", "$('#ContentPlaceHolder1_cboLocation').addClass('has-error');", true);
            }
            else
            {
                lblMesageLocation.Text = "";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorLocation", "$('#ContentPlaceHolder1_cboLocation').removeClass('has-error');", true);
            }

            try
            {
                Convert.ToInt32(txtSize.Text);
                lblMessageSize.Text = "";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorSize", "$('#ContentPlaceHolder1_txtSize').removeClass('has-error');", true);
            }
            catch (Exception ex)
            {
                ind = false;
                lblMessageSize.Text = "Debe digitar una capacidad correcta.";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorSize", "$('#ContentPlaceHolder1_txtSize').addClass('has-error');", true);
            }

            if (Convert.ToInt32(cboprogram.SelectedValue) == 0 || Convert.ToInt32(cboprogram.SelectedValue) == 1)
            {
                ind = false;
                lblmessageprogram.Text = "Debe seleccionar el programa.";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorProgram", "$('#ContentPlaceHolder1_cboProgram').addClass('has-error');", true);
            }
            else
            {
                lblmessageprogram.Text = "";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorProgram", "$('#ContentPlaceHolder1_cboProgram').removeClass('has-error');", true);
            }

            return ind;
        }

        protected void loadClassroomType()
        {
            List<Entities.ClassRoomsType> listClassRoomsType = new List<Entities.ClassRoomsType>();
            listClassRoomsType = ClassRoomsTypeBLL.getInstance().getAllActived();
            ListItem oItemS = new ListItem("---- Seleccione ----", "0");
            cboClassRoomType.Items.Add(oItemS);
            foreach (Entities.ClassRoomsType oClassRoomsType in listClassRoomsType)
            {
                ListItem oItem = new ListItem(oClassRoomsType.description, oClassRoomsType.code.ToString());
                cboClassRoomType.Items.Add(oItem);
            }
        }

        protected void loadLocation()
        {
            List<Entities.Location> listLocation = new List<Entities.Location>();
            listLocation = LocationBLL.getInstance().getAllActive();
            ListItem oItemS = new ListItem("---- Seleccione ----", "0");
            cboLocation.Items.Add(oItemS);
            foreach (Entities.Location oLocation in listLocation)
            {
                ListItem oItem = new ListItem(oLocation.oHeadquarters.description + " - " + oLocation.building + " - " + oLocation.module, oLocation.code.ToString());
                cboLocation.Items.Add(oItem);
            }
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
        protected void blockControls()
        {
            txtCode.Enabled = false;
            txtDescription.Enabled = false;
            cboClassRoomType.Enabled = false;
            cboLocation.Enabled = false;
            txtSize.Enabled = false;
            cboprogram.Enabled = false;
            cboState.Enabled = false;
            btnNew.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            clearControls();
        }

        protected void unlockControls()
        {
            txtCode.Enabled = true;
            txtDescription.Enabled = true;
            cboClassRoomType.Enabled = true;
            cboLocation.Enabled = true;
            txtSize.Enabled = true;
            if (oUser.oProgram.code == 1) {
                cboprogram.Enabled = true;
            }
            else
            {
                cboProgramValue();
            }
            cboState.Enabled = true;
            btnNew.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            clearControls();
        }

        protected void clearControls()
        {
            txtCode.Text = "";
            txtDescription.Text = "";
            txtSize.Text = "";
            cboState.SelectedValue = "1";
            lblMessage.Text = "";
            cboLocation.SelectedValue = "0";
            cboprogram.SelectedValue = "0";
            cboClassRoomType.SelectedValue = "0";
            lblMesageLocation.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorLocation", "$('#ContentPlaceHolder1_cboLocation').removeClass('has-error');", true);
            lblMessageClassRoomType.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorClassRoomType", "$('#ContentPlaceHolder1_cboClassRoomType').removeClass('has-error');", true);
            lblMessageDescription.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorDescription", "$('#ContentPlaceHolder1_txtDescription').removeClass('has-error');", true);
            lblmessageprogram.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorProgram", "$('#ContentPlaceHolder1_cboProgram').removeClass('has-error');", true);
            lblMessageSize.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorSize", "$('#ContentPlaceHolder1_txtSize').removeClass('has-error');", true);
        }

        protected void gvClassRoom_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvClassRoom.PageIndex = e.NewPageIndex;
            loadData();
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                List<Entities.ClassRoom> listRoom = ClassRoomBLL.getInstance().getAll();
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
    }
}