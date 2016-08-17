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
    public partial class AcademicOffer : System.Web.UI.Page
    {
        public bool offerAcademic { get; set; } 
        static Int32 AcademicOffer_id = -1;
        static UserSystem oUser = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                blockControls();
                loadUser();
                startCombos();
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
            txtCode.Text = BLL.AcademicOfferBLL.getInstance().getNextCode().ToString();
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            if (validateData())
            {
                Entities.Period oPeriod = new Entities.Period();
                Entities.Program oProgram = new Entities.Program();
                Entities.Course oCourse = new Entities.Course();
                Entities.ClassRoom oClassRoom = new Entities.ClassRoom();
                Entities.Schedule oSchedule = new Entities.Schedule();
                Entities.Teacher oteacher = new Entities.Teacher();
                Int32 code = Convert.ToInt32(txtCode.Text.ToString());
                oPeriod.code = Convert.ToInt32(cboPeriod.SelectedValue.ToString());
                oProgram.code = Convert.ToInt32(cboProgram.SelectedValue.ToString());
                oCourse.id = Convert.ToInt32(cboCourse.SelectedValue.ToString());
                Decimal price = Convert.ToDecimal(txtPrice.Text.ToString());
                oSchedule.code = Convert.ToInt32(cboSchedule.SelectedValue.ToString());
                oClassRoom.code = Convert.ToInt32(cboRoom.SelectedValue.ToString());
                oteacher.code = Convert.ToInt32(cboTeacher.SelectedValue.ToString());
                Int32 hour = Convert.ToInt32(cboHours.SelectedValue.ToString());
                Entities.AcademicOffer oAcademicOffer = new Entities.AcademicOffer(code, oPeriod, oProgram, oCourse, price, oClassRoom, oSchedule, oteacher, hour);
                if (BLL.AcademicOfferBLL.getInstance().exists(code))
                {
                    BLL.AcademicOfferBLL.getInstance().modify(oAcademicOffer);
                }
                else
                {
                    BLL.AcademicOfferBLL.getInstance().insert(oAcademicOffer);
                    insertClasroomSchedule(oAcademicOffer);
                    insertTeacherSchedule(oAcademicOffer);
                }

                blockControls();
                loadData();
            }
        }
        //this method insert the new schedule for the classrooms
        public void insertClasroomSchedule(Entities.AcademicOffer oAcademic)
        {
            Int32 cod = Convert.ToInt32(cboSchedule.SelectedValue);
            Entities.Schedule oSchedule = BLL.ScheduleBLL.getInstance().getSchedule(cod);
            int[] days = selectDays(oSchedule.codday);

            for (int i = 0; i < days.Count(); i++)
            {
                if (Convert.ToInt32(days[i]) != 9)
                {
                    int newCod = Convert.ToInt32(ClassRoomScheduleBLL.getInstance().getNextCode().ToString());
                    int day = Convert.ToInt32(days[i]);
                    ClassRoomScheduleBLL.getInstance().insert(oAcademic, oSchedule, newCod, day);
                }
            }
        }

        //this method insert the new schedule for the teacher
        public void insertTeacherSchedule(Entities.AcademicOffer oAcademic)
        {
            Int32 cod = Convert.ToInt32(cboSchedule.SelectedValue);
            Entities.Schedule oSchedule = BLL.ScheduleBLL.getInstance().getSchedule(cod);
            int[] days = selectDays(oSchedule.codday);

            for (int i = 0; i < days.Count(); i++)
            {
                if (Convert.ToInt32(days[i]) != 9)
                {
                    int newCod = Convert.ToInt32(TeacherScheduleBLL.getInstance().getNextCode().ToString());
                    TeacherScheduleBLL.getInstance().insert(oAcademic, oSchedule, newCod, Convert.ToInt32(days[i]));
                }
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

        protected void gvAcademicOffer_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            String CourseDescription = gvAcademicOffer.Rows[e.RowIndex].Cells[1].Text;
            AcademicOffer_id = Convert.ToInt32(gvAcademicOffer.Rows[e.RowIndex].Cells[0].Text);
            lblAcademicOfferDescription.Text = CourseDescription;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirmMessage", "$('#confirmMessage').modal();", true);
            confirmModal.Update();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Int32 records = BLL.AcademicOfferBLL.getInstance().delete(AcademicOffer_id);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirmMessage", "$('#confirmMessage').modal('toggle');", true);

            if (records > 0)
            {
                lblMessage.Text = "Oferta Academica fue eliminado correctamente.";
            }
            loadData();
        }

        /**
         * Method to get period types registered in the database and to load in a GridView
       */
        protected void loadData()
        {
            gvAcademicOffer.DataSource = BLL.AcademicOfferBLL.getInstance().getGridView();
            gvAcademicOffer.DataBind();
        } //End loadData()




        protected void chkEspecial_CheckedChanged(object sender, EventArgs e)
        {
            //selecciona el valor del combo para utilizarlo si desactiva el chekbox
            int idSchedule = 0;
            try
            {
                idSchedule = Convert.ToInt32(cboSchedule.SelectedValue);
            }
            catch (Exception){ }

            cboRoom.Items.Clear();

            if (chkEspecial.Checked)
            {
                getRoomEspecial(idSchedule);
            }
            else
            {
                if (idSchedule > 0)
                {
                    getRoombySchedule(idSchedule);
                }
            }
        }

        // to fill in the cmbs
        private void startCombos()
        {
            getProgram();
            getPeriod();
            fillhours();
            cboProgramValue();
        }



        //its when the cmboProgram Changed, the course have to changed
        protected void cboProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            int cod = Convert.ToInt32(cboProgram.SelectedValue);
            cboProgramAction(cod);
        }

        protected void cboProgramAction(int cod)
        {
            cboRoom.Items.Clear();
            cboSchedule.Items.Clear();
            if (chkEspecial.Checked == false)
            {
                cboCourse.Items.Clear();
                getCourseProgram(cod);
            }
            getScheduleProgram(cod);
        }

        //when the user selected the schedule, the cmbRoom wil full with the parameters 
        protected void cboSchedule_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idSchedule = Convert.ToInt32(cboSchedule.SelectedValue);
            if (idSchedule != 0)
            {
                chkEspecial.Enabled = true;
                cboRoom.Items.Clear();
                cboTeacher.Items.Clear();
                getRoombySchedule(idSchedule);
                getTeacherSchedule(idSchedule);
            }
            else
            {
                chkEspecial.Enabled = false;
            }
        }


        //it's to fill in the cmbPeriod 
        public void getPeriod()
        {
            List<Entities.Period> listPeriod = new List<Entities.Period>();
            listPeriod = BLL.PeriodBLL.getInstance().getAllActive();
            ListItem oItemS = new ListItem("---- Seleccione ----", "0");
            cboPeriod.Items.Add(oItemS);
            foreach (Entities.Period olistPeriod in listPeriod)
            {
                ListItem oItem = new ListItem(olistPeriod.name + " " + olistPeriod.oPeriodType.description, olistPeriod.code.ToString());
                cboPeriod.Items.Add(oItem);
            }
        }

        //it's to fill in the cmbProgram 
        public void getProgram()
        {
            List<Entities.Program> listProgram = new List<Entities.Program>();
            listProgram = BLL.ProgramBLL.getInstance().getAllActived();
            ListItem oItemS = new ListItem("---- Seleccione ----", "0");
            cboProgram.Items.Add(oItemS);
            foreach (Entities.Program olistProgram in listProgram)
            {
                ListItem oItem = new ListItem(olistProgram.name, olistProgram.code.ToString());
                cboProgram.Items.Add(oItem);
            }
        }

        protected void cboProgramValue()
        {
            if (oUser.oProgram.code != 1)
            {
                cboProgram.SelectedValue = oUser.oProgram.code.ToString();
                cboPeriod.SelectedValue = Session["period"].ToString();
                cboProgramAction(oUser.oProgram.code);
            }
        }

        //it's the all courses, with state active, of the program selected 
        public void getCourseProgram(Int32 cod)
        {
            List<Entities.Course> listSubject = new List<Entities.Course>();
            listSubject = BLL.CourseBLL.getInstance().getAllActivedProgram(cod);
            ListItem oItemS = new ListItem("---- Seleccione ----", "0");
            cboCourse.Items.Add(oItemS);
            foreach (Entities.Course olistSubject in listSubject)
            {
                ListItem oItem = new ListItem(olistSubject.description, olistSubject.id.ToString());
                cboCourse.Items.Add(oItem);
            }
        }

        //when the chkEspecial is selected the cmbCourse is going to fill in with the  courses of the all programs
        public void getRoomEspecial(Int32 cod)
        {
            Entities.Schedule oSchedule = BLL.ScheduleBLL.getInstance().getSchedulebyCodeActive(cod);
            int[] days = selectDays(oSchedule.codday);
            int periodId = Convert.ToInt32(cboPeriod.SelectedValue);
            List<Entities.ClassRoom> listClassRoom = new List<Entities.ClassRoom>();
            listClassRoom = BLL.ClassRoomScheduleBLL.getInstance().getAllActiveByScheduleEspecial(periodId,oSchedule, days);
            ListItem oItemS = new ListItem("---- Seleccione ----", "0");
            cboRoom.Items.Add(oItemS);
            foreach (Entities.ClassRoom olistClassRoom in listClassRoom)
            {
                ListItem oItem = new ListItem(olistClassRoom.num_room + " Sede " + olistClassRoom.oLocation.oHeadquarters.description + " " + olistClassRoom.oLocation.module + " " + olistClassRoom.oClassRoomsType.description, olistClassRoom.code.ToString());
                cboRoom.Items.Add(oItem);
            }

        }

        //thit method will select the schedule of the program selected
        public void getScheduleProgram(Int32 id)
        {
            List<Entities.Schedule> listschelude = new List<Entities.Schedule>();
            listschelude = BLL.ScheduleBLL.getInstance().getAllActiveByPrgram(id);
            ListItem oItemS = new ListItem("---- Seleccione ----", "0");
            cboSchedule.Items.Add(oItemS);
            foreach (Entities.Schedule olistschelude in listschelude)
            {
                ListItem oItem = new ListItem(olistschelude.name + " " + olistschelude.typeSchedule, olistschelude.code.ToString());
                cboSchedule.Items.Add(oItem);
            }
        }

        ///this method will select the rooms, who are available for use 
        public void getRoombySchedule(Int32 cod)
        {
            Entities.Schedule oSchedule = BLL.ScheduleBLL.getInstance().getSchedulebyCodeActive(cod);
            int[] days = selectDays(oSchedule.codday);
            int programid = Convert.ToInt32(cboProgram.SelectedValue);
            int periodId = Convert.ToInt32(cboPeriod.SelectedValue);
            List<Entities.ClassRoom> listClassRoom = new List<Entities.ClassRoom>();
            listClassRoom = BLL.ClassRoomScheduleBLL.getInstance().getAllActiveBySchedule(periodId, programid, oSchedule, days);
            ListItem oItemS = new ListItem("---- Seleccione ----", "0");
            cboRoom.Items.Add(oItemS);
            foreach (Entities.ClassRoom olistClassRoom in listClassRoom)
            {
                ListItem oItem = new ListItem(olistClassRoom.num_room + " Sede " + olistClassRoom.oLocation.oHeadquarters.description + " " + olistClassRoom.oLocation.module + " " + olistClassRoom.oClassRoomsType.description, olistClassRoom.code.ToString());
                cboRoom.Items.Add(oItem);
            }
        }

        //To select the teachers that have free time and can give class
        public void getTeacherSchedule(Int32 cod)
        {
            Entities.Schedule oSchedule = BLL.ScheduleBLL.getInstance().getSchedulebyCodeActive(cod);
            int[] days = selectDays(oSchedule.codday);
            int programid = Convert.ToInt32(cboProgram.SelectedValue);
            int periodId = Convert.ToInt32(cboPeriod.SelectedValue);
            List<Entities.Teacher> listTeacher = new List<Entities.Teacher>();
            listTeacher = BLL.TeacherScheduleBLL.getInstance().getAllActiveTeacherBySchedule(periodId, oSchedule, days);
            ListItem oItemS = new ListItem("---- Seleccione ----", "0");
            cboTeacher.Items.Add(oItemS);
            foreach (Entities.Teacher oTeacher in listTeacher)
            {
                ListItem oItem = new ListItem(oTeacher.name + " " + oTeacher.lastName, oTeacher.code.ToString());
                cboTeacher.Items.Add(oItem);
            }
        }

        private void fillhours()
        {

            ListItem oItemS = new ListItem("---- Seleccione ----", "0");
            for (int i = 5; i <= 40; i = i + 5)
            {
                ListItem oItem = new ListItem(i.ToString(), i.ToString());
                cboHours.Items.Add(oItem);
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

        //in this method, the all controls are verify, to avoid errors
        private bool validateData()
        {
            Boolean ind = true;
            //validate price
            if (txtPrice.Text.Trim() == "")
            {
                ind = false;
                lblMessagePrice.Text = "Debe digitar el precio del curso";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorPrice", "$('#ContentPlaceHolder1_txtPrice').addClass('has-error');", true);
            }
            //validate period 
            if (Convert.ToInt32(cboPeriod.SelectedValue) == 0)
            {
                ind = false;
                lblMessagePeriod.Text = "Debe seleccionar un periodo";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorPeriod", "$('#ContentPlaceHolder1_cboPeriod').addClass('has-error');", true);
            }
            //validate program
            if (Convert.ToInt32(cboProgram.SelectedValue) == 0)
            {
                ind = false;
                lblMessageProgram.Text = "Debe seleccionar un programa";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorProgram", "$('#ContentPlaceHolder1_cboProgram').addClass('has-error');", true);
            }
            //validate course
            if (Convert.ToInt32(cboCourse.SelectedValue) == 0)
            {
                ind = false;
                lblMessageCourse.Text = "Debe seleccionar un curso";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorCourse", "$('#ContentPlaceHolder1_cboCourse').addClass('has-error');", true);
            }
            //validate ClassRoom
            try
            {
                if (Convert.ToInt32(cboRoom.SelectedValue) == 0)
                {
                    ind = false;
                    lblMessageRoom.Text = "Debe seleccionar un aula";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorRoom", "$('#ContentPlaceHolder1_cboRoom').addClass('has-error');", true);
                }
            }
            catch (Exception)
            {
                ind = false;
                lblMessageRoom.Text = "Debe seleccionar un aula";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorRoom", "$('#ContentPlaceHolder1_cboRoom').addClass('has-error');", true);
            }
            //validate teacher
            try
            {
                if (Convert.ToInt32(cboTeacher.SelectedValue) != 0)
                {
                    lblMessageTeacher.Text = "";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorTeacher", "$('#ContentPlaceHolder1_cboTeacher').removeClass('has-error');", true);
                }
                else
                {
                    ind = false;
                    lblMessageTeacher.Text = "Debe seleccionar un profesor";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorTeacher", "$('#ContentPlaceHolder1_cboTeacher').addClass('has-error');", true);
                }
            }
            catch (Exception)
            {
                ind = false;
                lblMessageTeacher.Text = "Debe seleccionar un profesor";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorTeacher", "$('#ContentPlaceHolder1_cboTeacher').addClass('has-error');", true);
            }
            //validate schedule
            try
            {
                if (Convert.ToInt32(cboSchedule.SelectedValue) != 0)
                {
                    lblMesageSchedule.Text = "";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorSchedule", "$('#ContentPlaceHolder1_cboSchedule').removeClass('has-error');", true);
                }
                else
                {
                    ind = false;
                    lblMesageSchedule.Text = "Debe seleccionar un horario";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorSchedule", "$('#ContentPlaceHolder1_cboSchedule').addClass('has-error');", true);
                }
            }
            catch (Exception)
            {
                ind = false;
                lblMesageSchedule.Text = "Debe seleccionar un horario";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorSchedule", "$('#ContentPlaceHolder1_cboSchedule').addClass('has-error');", true);
            }
            return ind;
        }

        /** 
          * Method to block the fields
       */
        protected void blockControls()
        {
            clearControls();
            txtCode.Visible = false;
            lblCode.Visible = false;
            txtPrice.Enabled = false;
            cboHours.Enabled = false;
            cboCourse.Enabled = false;
            cboPeriod.Enabled = false;
            cboProgram.Enabled = false;
            cboRoom.Enabled = false;
            cboSchedule.Enabled = false;
            cboTeacher.Enabled = false;
            chkEspecial.Enabled = false;
            btnNew.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
        } //End blockControls()

        /**
         * Method to unlock the form fields
        */
        protected void unlockControls()
        {
            clearControls();
            txtPrice.Enabled = true;
            cboHours.Enabled = true;
            cboCourse.Enabled = true;
            if (oUser.oProgram.code == 1)
            {
                cboPeriod.Enabled = true;
                cboProgram.Enabled = true;
            }
            else
            {
                cboProgramValue();
            }
            cboRoom.Enabled = true;
            cboSchedule.Enabled = true;
            cboTeacher.Enabled = true;
            btnNew.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
        } //End unlockControls()

        /**
         * Method to clear the form field
        */
        protected void clearControls()
        {
            cboProgram.SelectedValue = "0";
            cboPeriod.SelectedValue = "0";
            cboHours.Items.Clear();
            cboCourse.Items.Clear();
            cboRoom.Items.Clear();
            cboSchedule.Items.Clear();
            cboTeacher.Items.Clear();
            chkEspecial.Checked = false;
            txtPrice.Text = "";
            txtCode.Text = "";
            lblMessage.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorName", "$('#ContentPlaceHolder1_cboPeriod').removeClass('has-error');", true);
            lblMessagePeriod.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorName", "$('#ContentPlaceHolder1_cboCourse').removeClass('has-error');", true);
            lblMessageCourse.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorName", "$('#ContentPlaceHolder1_cboSchedule').removeClass('has-error');", true);
            lblMesageSchedule.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorName", "$('#ContentPlaceHolder1_cboTeacher').removeClass('has-error');", true);
            lblMessageTeacher.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorName", "$('#ContentPlaceHolder1_cboProgram').removeClass('has-error');", true);
            lblMessageProgram.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorName", "$('#ContentPlaceHolder1_txtPrice').removeClass('has-error');", true);
            lblMessagePrice.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorName", "$('#ContentPlaceHolder1_cboRoom').removeClass('has-error');", true);
            lblMessageRoom.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorName", "$('#ContentPlaceHolder1_cboHours').removeClass('has-error');", true);
            lblMessageHours.Text = "";
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            
        }//End clearControls()


    }
}