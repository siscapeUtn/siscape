﻿using BLL;
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
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                List<Entities.Course> listCourse = CourseBLL.getInstance().getAll();
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
                title.Add("\n\n Reporte de Cursos\n\n");
                pdfDoc.Add(title);
                
                PdfPTable oPTable = new PdfPTable(5);
                oPTable.TotalWidth = 100;
                oPTable.SpacingBefore = 20f;
                oPTable.SpacingAfter = 30f;
                oPTable.AddCell("Descripción");
                oPTable.AddCell("Estado");

                if (listCourse.Count > 0)
                {
                    foreach (Entities.Course pCourse in listCourse)
                    {
                        oPTable.AddCell(pCourse.description);
                        oPTable.AddCell((pCourse.state == 1 ? "Activo" : "Inactivo"));
                    }
                }
                else
                {
                    PdfPCell cell = new PdfPCell(new text::Phrase("No existen cursos registrados."));
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
                Response.AddHeader("Content-Disposition", "attachment; filename=Cursos.pdf");
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
        }//End clearControls()
    } //End periodType
    }
