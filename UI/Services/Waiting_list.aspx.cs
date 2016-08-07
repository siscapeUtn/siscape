﻿using Entities;
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
            
        }

    }
}