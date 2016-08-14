using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI.Academic.AcademicOffer
{
    public partial class Course : System.Web.UI.Page
    {
        public bool offerAcademic { get; set; }
        static Int32 Course_id = -1;
        protected void Page_Load(object sender, EventArgs e)
        {
           if (!IsPostBack)
            {
                blockControls();
            }
            loadData();
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
            txtCode.Text = CourseBLL.getInstance().getNextCode().ToString();
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            Int32 records = 0;
            if (validateData())
            {
                Entities.Course oSubject = new Entities.Course();
                oSubject.id = Convert.ToInt32(txtCode.Text);
                oSubject.description = txtName.Text;
                oSubject.state = Convert.ToInt16(cboState.SelectedValue);
                oSubject.oProgram.code = 1;
                    if (CourseBLL.getInstance().exists(oSubject.id))
                    {
                        records = CourseBLL.getInstance().modify(oSubject);
                    }
                    else
                    {
                        records = CourseBLL.getInstance().insert(oSubject);
                    }
                  
                blockControls();
                loadData();

                if (records > 0)
                {
                    lblMessage.Text = "Datos almacenados correactamente";
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

        protected void gvCourse_RowEditing(object sender, GridViewEditEventArgs e)
        {
              unlockControls();
              Int32 code = Convert.ToInt32(gvCourse.Rows[e.NewEditIndex].Cells[0].Text);
              Entities.Course oSubject = CourseBLL.getInstance().getCourse(code);
              txtCode.Text = oSubject.id.ToString();
              txtName.Text = oSubject.description.ToString();
              try
              {
                  cboState.SelectedValue = oSubject.state.ToString();
              }
              catch (Exception)
              {
                  cboState.SelectedValue = "1";
              }
              ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "$('html, body').animate({ scrollTop: $('body').offset().top });", true);
        }

        protected void gvCoursee_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            String CourseDescription = gvCourse.Rows[e.RowIndex].Cells[1].Text;
            Course_id = Convert.ToInt32(gvCourse.Rows[e.RowIndex].Cells[0].Text);
            lblCourseDescription.Text = CourseDescription;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirmMessage", "$('#confirmMessage').modal();", true);
            confirmModal.Update();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Int32 records = CourseBLL.getInstance().delete(Course_id);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirmMessage", "$('#confirmMessage').modal('toggle');", true);

            if (records > 0)
            {
                lblMessage.Text = "Curso eliminado correctamente.";
            }
            loadData();
        }

        protected Boolean validateData()
        {
           Boolean ind = true;

              if (txtName.Text.Trim() == "")
             {
                 ind = false;
                 lblNameMessage.Text = "Debe digitar una descripción correcta.";
                 ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addHasErrorName", "$('#ContentPlaceHolder1_txtName').addClass('has-error');", true);
             }
             else
             {
                 lblNameMessage.Text = "";
                 ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorName", "$('#ContentPlaceHolder1_txtName').removeClass('has-error');", true);
             }

            return ind;
        }

        /**
         * Method to get period types registered in the database and to load in a GridView
        */ 
        protected void loadData()
        {
           gvCourse.DataSource = CourseBLL.getInstance().getAll();
           gvCourse.DataBind();
        } //End loadData()

        /** 
         * Method to block the fields
        */
        protected void blockControls()
        {
            clearControls();
            txtName.Enabled = false;
            cboState.Enabled = false;
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
            txtName.Enabled = true;
            cboState.Enabled = true;
            btnNew.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
        } //End unlockControls()

        /**
         * Method to clear the form fields 
        */
        protected void clearControls()
        {
            txtCode.Text = "";
            txtName.Text = "";
            cboState.SelectedValue = "1";
            lblMessage.Text = "";
            lblNameMessage.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorName", "$('#ContentPlaceHolder1_txtName').removeClass('has-error');", true);
        }

        protected void gvCourse_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCourse.PageIndex = e.NewPageIndex;
            loadData();
        }//End clearControls()
    } //End periodType
    }
