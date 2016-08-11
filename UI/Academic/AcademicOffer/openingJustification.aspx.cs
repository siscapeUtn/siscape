using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
    }
}