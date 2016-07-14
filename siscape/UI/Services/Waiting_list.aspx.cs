using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI.Services
{

    public partial class Waiting_list : System.Web.UI.Page
    {
        DataTable oDataTable = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getProgram();
                fillSchedule();
                FillGv();
            }
        }

        private bool validateData()
        {
            Boolean ind = true;

            try
            {
                Convert.ToInt32(txtId.Text);
                lblMessageId.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorId", "$('#ContentPlaceHolder1_txtId').removeClass('has-error');", true);
            }
            catch (Exception ex)
            {
                ind = false;
                lblMessageId.Text = "*";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "addHasErrorId", "$('#ContentPlaceHolder1_txtId').addClass('has-error');", true);
            }

            if (txtName.Text.Trim() == "")
            {
                ind = false;
                lblMessageName.Text = "*";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "addHasErrorName", "$('#ContentPlaceHolder1_txtName').addClass('has-error');", true);
            }
            else
            {
                lblMessageName.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorName", "$('#ContentPlaceHolder1_txtName').removeClass('has-error');", true);
            }

            if (txtLastName.Text.Trim() == "")
            {
                ind = false;
                lblMessageLastName.Text = "*";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "addHasErrorLastName", "$('#ContentPlaceHolder1_txtLastName').addClass('has-error');", true);
            }
            else
            {
                lblMessageLastName.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorLastName", "$('#ContentPlaceHolder1_txtLastName').removeClass('has-error');", true);
            }

            try
            {
                Convert.ToInt32(txtHomePhone.Text);
                lblMessageHomePhone.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorHomePhone", "$('#ContentPlaceHolder1_txtHomePhone').removeClass('has-error');", true);
            }
            catch (Exception ex)
            {
                ind = false;
                lblMessageHomePhone.Text = "*";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "addHasErrorHomePhone", "$('#ContentPlaceHolder1_txtHomePhone').addClass('has-error');", true);
            }

            try
            {
                Convert.ToInt32(txtCellPhone.Text);
                lblMessageCellPhone.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorCellPhone", "$('#ContentPlaceHolder1_txtCellPhone').removeClass('has-error');", true);
            }
            catch (Exception ex)
            {
                ind = false;
                lblMessageCellPhone.Text = "*";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "addHasErrorCellPhone", "$('#ContentPlaceHolder1_txtCellPhone').addClass('has-error');", true);
            }

            if (txtEmail.Text.Trim() == "" || !txtEmail.Text.Contains("@"))
            {
                ind = false;
                lblMessageEmail.Text = "*";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "addHasErrorEmail", "$('#ContentPlaceHolder1_txtEmail').addClass('has-error');", true);
            }
            else
            {
                lblMessageEmail.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorEmail", "$('#ContentPlaceHolder1_txtEmail').removeClass('has-error');", true);
            }

            return ind;
        }

        public void btnCancel_Click(object sender, ImageClickEventArgs e)
        {

        }

        private void fillSchedule()
        {
            ListItem oItemS = new ListItem("---- Seleccione ----", "0");
            cboSchedule.Items.Add(oItemS);
            ListItem oItem = new ListItem("Mañana", "Mañana");
            cboSchedule.Items.Add(oItem);
            ListItem oItem1 = new ListItem("Tarde", "Tarde");
            cboSchedule.Items.Add(oItem1);
            ListItem oItem2 = new ListItem("Noche", "Noche");
            cboSchedule.Items.Add(oItem2);

        }



        private void cleanData()
        {
            cboProgram.SelectedIndex = 0;
            cboCourse.SelectedIndex = 0;
            cboSchedule.SelectedIndex = 0;
            foreach (ListItem itemActual in chkDays.Items)
            {
                itemActual.Selected = false;
            }
        }

        private bool validateDataAdd()
        {
            //lblMsjCourse.Text = "";
            bool flag = true;
            String days = "";
            foreach (ListItem itemActual in chkDays.Items)
            {
                if (itemActual.Selected)
                {
                    days = days + itemActual.Value;
                }
            }
            if (days == "")
            {
                flag = false;
            }
            //
            if (cboCourse.SelectedValue.ToString() == "0")
            {
                flag = false;
            }
            if (cboProgram.SelectedValue.ToString() == "0")
            {
                flag = false;
            }

            List<Entities.Course> list = new List<Entities.Course>();
            if (Session["listCourse"] != null)
            {
                list = (List<Entities.Course>)Session["listCourse"];

                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].id == Convert.ToInt32(cboCourse.SelectedValue.ToString()))
                    {
                        //lblMsjCourse.Text = "Este curso ya se ingresó";
                        flag = false;
                        break;
                    }
                }
            }

            return flag;
        }


        protected void FillGv()
        {
            List<Entities.Course> list = new List<Entities.Course>();
            if (Session["listCourse"] != null)
            {
                list = (List<Entities.Course>)Session["listCourse"];
            }
            DataColumn code = oDataTable.Columns.Add("code", typeof(int));
            DataColumn des = oDataTable.Columns.Add("des", typeof(string));
            DataColumn prog = oDataTable.Columns.Add("prog", typeof(string));
            DataColumn sch = oDataTable.Columns.Add("sch", typeof(string));
            DataColumn day = oDataTable.Columns.Add("day", typeof(string));

            DataRow oDataRow;
            for (int i = 0; i < list.Count; i++)
            {
                oDataRow = oDataTable.NewRow();
                oDataRow["code"] = list[i].id;
                oDataRow["des"] = list[i].description;
                oDataRow["prog"] = list[i].oProgram.name;
                oDataRow["sch"] = list[i].schedule;
                oDataRow["day"] = list[i].days;
                oDataTable.Rows.Add(oDataRow);
            }
            grvCourse.DataSource = oDataTable;
            grvCourse.DataBind();
        }

        protected void grvCourse_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int code = Convert.ToInt32(grvCourse.Rows[e.RowIndex].Cells[0].Text);
            List<Entities.Course> list = new List<Entities.Course>();
            if (Session["listCourse"] != null)
            {
                list = (List<Entities.Course>)Session["listCourse"];

                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].id == code)
                    {
                        list.RemoveAt(i);
                        break;
                    }
                }
                Session["listCourse"] = list;
                FillGv();
            }
        }


        protected void chkDays_SelectedIndexChanged(object sender, EventArgs e)
        {
            String days = "";
            String codDays = "";
            foreach (ListItem itemActual in chkDays.Items)
            {
                if (itemActual.Selected)
                {
                    days = days + itemActual.Value;
                    codDays = codDays + itemActual.Value;
                    days = days + ",";
                }
            }
            txtDays.Text = days.TrimEnd(',');
            //txtDays.Text = txtDays.Text + chkDays.SelectedValue;
        }

        /*/////////////////// */

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

        protected void cboProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            int cod = Convert.ToInt32(cboProgram.SelectedValue);
            cboCourse.Items.Clear();
            getCourseProgram(cod);
        }

        public void getCourseProgram(Int32 cod)
        {
            List<Entities.Course> listSubject = new List<Entities.Course>();
            listSubject = BLL.CourseBLL.getInstance().getAllActivedCourseProgram(cod);
            ListItem oItemS = new ListItem("---- Seleccione ----", "0");
            cboCourse.Items.Add(oItemS);
            foreach (Entities.Course olistSubject in listSubject)
            {
                ListItem oItem = new ListItem(olistSubject.description, olistSubject.id.ToString());
                cboCourse.Items.Add(oItem);
            }
        }

        protected void btnAdd_Click1(object sender, ImageClickEventArgs e)
        {
            if (validateDataAdd())
            {

                Int32 code = Convert.ToInt32(cboCourse.SelectedValue.ToString());
                String des = cboCourse.SelectedItem.Text;
                String prog = cboProgram.SelectedItem.Text;
                String sch = cboSchedule.SelectedItem.Text;
                String day = txtDays.Text;
                List<Entities.Course> list = new List<Entities.Course>();
                Entities.Course course = new Entities.Course();
                course.description = des;
                course.id = code;
                course.oProgram = new Entities.Program();
                course.schedule = cboSchedule.SelectedItem.Text;
                course.oProgram.name = prog;
                course.schedule = sch;
                course.days = day;
                if (Session["listCourse"] != null)
                {
                    list = (List<Entities.Course>)Session["listCourse"];
                }
                list.Add(course);
                Session["listCourse"] = list;
                FillGv();
                cleanData();
            }
        }

        protected void btn_Cancel(object sender, EventArgs e)
        {
            Response.Redirect("../index.aspx");
        }

        protected void btnSave(object sender, EventArgs e)
        {
            if (validateData())
            {

                Entities.WaitingList oWaitingList = new Entities.WaitingList();
                oWaitingList.code = BLL.WaitingListBLL.getInstance().getNextCodeWaitingList();
                oWaitingList.id = txtId.Text;
                oWaitingList.name = txtName.Text;
                oWaitingList.lastName = txtLastName.Text;
                oWaitingList.homePhone = txtHomePhone.Text;
                oWaitingList.cellPhone = txtCellPhone.Text;
                oWaitingList.email = txtEmail.Text;
                oWaitingList.period = 0;

                List<Tentative_Schedule> listTentative_Schedule = new List<Tentative_Schedule>();
                List<Course> listCourse = new List<Course>();
                listCourse = (List<Entities.Course>)Session["listCourse"];

                BLL.WaitingListBLL.getInstance().insertWaitingList(listCourse, oWaitingList);
                clearControls();
                Session.RemoveAll();
                Response.Redirect("Waiting_list.aspx");
            }

        }
        protected void clearControls()
        {
            txtCode.Text = "";
            txtId.Text = "";
            lblMessageId.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorId", "$('#ContentPlaceHolder1_txtId').removeClass('has-error');", true);
            txtName.Text = "";
            lblMessageName.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorName", "$('#ContentPlaceHolder1_txtName').removeClass('has-error');", true);
            txtLastName.Text = "";
            lblMessageLastName.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorLastName", "$('#ContentPlaceHolder1_txtLastName').removeClass('has-error');", true);
            txtHomePhone.Text = "";
            lblMessageHomePhone.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorHomePhone", "$('#ContentPlaceHolder1_txtHomePhone').removeClass('has-error');", true);
            txtCellPhone.Text = "";
            lblMessageCellPhone.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorCellPhone", "$('#ContentPlaceHolder1_txtCellPhone').removeClass('has-error');", true);
            txtEmail.Text = "";
            lblMessageEmail.Text = "";
            foreach (ListItem itemActual in chkDays.Items)
            {
                itemActual.Selected = false;
            }
            cboProgram.Items.Clear();
            getProgram();
            cboSchedule.SelectedIndex = 0;
        }

    }
}