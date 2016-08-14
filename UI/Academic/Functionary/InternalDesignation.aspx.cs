﻿using BLL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI.Academic
{
    public partial class InternalDesignation : System.Web.UI.Page
    {
        static Int32 internalDesignation_id = -1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                blockControls();
            }
            loadData();
        }

        protected void btnNew_Click(object sender, ImageClickEventArgs e)
        {
            unlockControls();
            txtCode.Text = InternalDesignationBLL.getInstance().getNextCode().ToString();
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            Int32 records = -1;
            if (validateData())
            {
                Entities.InternalDesignation oInternalDesignation = new Entities.InternalDesignation();
                oInternalDesignation.code = Convert.ToInt32(txtCode.Text);
                oInternalDesignation.description = txtDescription.Text;
                oInternalDesignation.baseSalary = Convert.ToDouble(txtSalary.Text.Trim());
                oInternalDesignation.annuity = Convert.ToDouble(txtAnnuality.Text);
                oInternalDesignation.state = Convert.ToInt16(cboState.SelectedValue);

                if ( InternalDesignationBLL.getInstance().exists(oInternalDesignation.code))
                {
                    records = InternalDesignationBLL.getInstance().modify(oInternalDesignation);
                }
                else
                {
                    records = InternalDesignationBLL.getInstance().insert(oInternalDesignation);
                }

                blockControls();
                loadData();

                if (records > 0)
                {
                    lblMessage.Text = "Datos almacenados correctamente.";
                }
            }
        }

        protected void btnCancel_Click(object sender, ImageClickEventArgs e)
        {
            blockControls();
        }

        protected void btnReturn_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("../AcademicGroups/gFunctionary.aspx");
        }

        protected void gvInternalDesignation_RowEditing(object sender, GridViewEditEventArgs e)
        {
            unlockControls();
            Int32 code = Convert.ToInt32(gvInternalDesignation.Rows[e.NewEditIndex].Cells[0].Text);
            Entities.InternalDesignation oInternalDesignation = InternalDesignationBLL.getInstance().getInternalDesignation(code);
            txtCode.Text = oInternalDesignation.code.ToString();
            txtDescription.Text = oInternalDesignation.description;
            txtSalary.Text = oInternalDesignation.baseSalary.ToString();
            txtAnnuality.Text = oInternalDesignation.annuity.ToString();
            cboState.SelectedValue = oInternalDesignation.state.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "$('html, body').animate({ scrollTop: $('body').offset().top });", true);
        }

        protected void gvInternalDesignation_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            String internalDesignationDescription = gvInternalDesignation.Rows[e.RowIndex].Cells[1].Text;
            internalDesignation_id = Convert.ToInt32(gvInternalDesignation.Rows[e.RowIndex].Cells[0].Text);
            lblInternalDesignationDescription.Text = internalDesignationDescription;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirmMessage", "$('#confirmMessage').modal();", true);
            confirmModal.Update();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Int32 records = InternalDesignationBLL.getInstance().delete(internalDesignation_id);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirmMessage", "$('#confirmMessage').modal('toggle');", true);

            if (records > 0)
            {
                lblMessage.Text = "Nombramiento interno eliminado correctamente.";
            }
            loadData();
        }

        protected void loadData()
        {
            gvInternalDesignation.DataSource = InternalDesignationBLL.getInstance().getAll();
            gvInternalDesignation.DataBind();
        }

        protected Boolean validateData()
        {
            Boolean ind = true;

            if (txtDescription.Text.Trim() == "")
            {
                ind = false;
                lblMessageDescription.Text = "Debe digitar una descripción correcta.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "addHasErrorDescription", "$('#ContentPlaceHolder1_txtDescription').addClass('has-error');", true);
            }
            else
            {
                lblMessageDescription.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorDescription", "$('#ContentPlaceHolder1_txtDescription').removeClass('has-error');", true);
            }

            try
            {
                Convert.ToDouble(txtSalary.Text);
                lblMessageSalary.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorSalary", "$('#ContentPlaceHolder1_txtSalary').removeClass('has-error');", true);
            }catch (Exception ex){
                ind = false;
                lblMessageSalary.Text = "Debe digitar un salario correcto.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "addHasErrorSalary", "$('#ContentPlaceHolder1_txtSalary').addClass('has-error');", true);
            }

            try
            {
                Convert.ToDecimal(txtAnnuality.Text);
                lblMessageAnnuality.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorAnnuality", "$('#ContentPlaceHolder1_txtAnnuality').removeClass('has-error');", true);
            }
            catch (Exception ex)
            {
                ind = false;
                lblMessageAnnuality.Text = "Debe digitar una anualidad correcta.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "addHasErrorAnnuality", "$('#ContentPlaceHolder1_txtAnnuality').addClass('has-error');", true);
            }

            return true;
        }

        protected void blockControls()
        {
            txtCode.Enabled = false;
            txtDescription.Enabled = false;
            txtSalary.Enabled = false;
            txtAnnuality.Enabled = false;
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
            txtSalary.Enabled = true;
            txtAnnuality.Enabled = true;
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
            lblMessageDescription.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorDescription", "$('#ContentPlaceHolder1_txtDescription').removeClass('has-error');", true);
            txtSalary.Text = "";
            lblMessageSalary.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorSalary", "$('#ContentPlaceHolder1_txtSalary').removeClass('has-error');", true);
            txtAnnuality.Text = "";
            lblMessageAnnuality.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "removeHasErrorAnnuality", "$('#ContentPlaceHolder1_txtAnnuality').removeClass('has-error');", true);
            cboState.SelectedValue = "1";
            lblMessage.Text = "";
        }

        protected void gvInternalDesignation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvInternalDesignation.PageIndex = e.NewPageIndex;
            loadData();
        }
    }
}