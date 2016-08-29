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
        public static String offer_id;
        public static String description;

        DataTable oDataTable = new DataTable();
        public bool service { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                offer_id = Request.Form["idOffer"];
                description = Request.Form["nameCourse"];
                lblCourse.Text = "Curso de "+ description;
            }
        }

        protected void showOService()
        {
            try { 
            this.service = Convert.ToBoolean(Session["Service"].ToString());
            }
            catch
            {
                this.service = false;
            }
           if (this.service == false)
            {
                Response.Redirect("../../index.aspx");
            }
        }
        protected void btn_Cancel(object sender, EventArgs e)
        {
            Response.Redirect("../../index.aspx");
        }

        protected void btnSave(object sender, EventArgs e)
        {
            if (validateData())
            {
                Int32 periodo;
                if (Session["period"] == null)
                {
                    periodo = BLL.PeriodBLL.getInstance().getLasPeriod();
                }
                else
                {
                    periodo = Convert.ToInt32(Session["period"]);
                }
                
                Entities.WaitingList oWaitingList = new Entities.WaitingList();
                oWaitingList.code = BLL.WaitingListBLL.getInstance().getNextCodeWaitingList();
                oWaitingList.id = txtId.Text;
                oWaitingList.name = txtName.Text;
                oWaitingList.lastName = txtLastName.Text;
                oWaitingList.homePhone = txtHomePhone.Text;
                oWaitingList.cellPhone = txtCellPhone.Text;
                oWaitingList.email = txtEmail.Text;
                oWaitingList.academicOffer = Convert.ToInt32(offer_id);
                //oWaitingList.period = periodo;
                //List<Tentative_Schedule> listTentative_Schedule = new List<Tentative_Schedule>();
                //List<Course> listCourse = new List<Course>();
                //listCourse = (List<Entities.Course>)Session["listCourse"];

                BLL.WaitingListBLL.getInstance().insertWaitingList(oWaitingList);
                clearControls();
                Response.Redirect("../../index.aspx");
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
            catch (Exception)
            {
                ind = false;
                lblMessageId.Text = "Debe digitar una identificación";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "addHasErrorId", "$('#ContentPlaceHolder1_txtId').addClass('has-error');", true);
            }

            if (txtName.Text.Trim() == "")
            {
                ind = false;
                lblMessageName.Text = "Debe digitar un nombre";
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
                lblMessageLastName.Text = "Debe digitar un apellido";
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
            catch (Exception)
            {
                ind = false;
                lblMessageHomePhone.Text = "Debe digitar un teléfono de recidencia";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "addHasErrorHomePhone", "$('#ContentPlaceHolder1_txtHomePhone').addClass('has-error');", true);
            }

            try
            {
                Convert.ToInt32(txtCellPhone.Text);
                lblMessageCellPhone.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorCellPhone", "$('#ContentPlaceHolder1_txtCellPhone').removeClass('has-error');", true);
            }
            catch (Exception)
            {
                ind = false;
                lblMessageCellPhone.Text = "Debe digitar un teléfono celular";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "addHasErrorCellPhone", "$('#ContentPlaceHolder1_txtCellPhone').addClass('has-error');", true);
            }

            if (txtEmail.Text.Trim() == "" || !txtEmail.Text.Contains("@"))
            {
                ind = false;
                lblMessageEmail.Text = "Debe digitar un correo válido";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "addHasErrorEmail", "$('#ContentPlaceHolder1_txtEmail').addClass('has-error');", true);
            }
            else
            {
                lblMessageEmail.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorEmail", "$('#ContentPlaceHolder1_txtEmail').removeClass('has-error');", true);
            }

            return ind;
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
            
        }

    }
}