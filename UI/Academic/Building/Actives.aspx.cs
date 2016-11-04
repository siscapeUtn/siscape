using BLL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI.Academic.Building
{
    public partial class Actives : System.Web.UI.Page
    {
        static UserSystem oUser = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                blockControls();
                //loadClassroomType();
                loadUser();
                loadPrograms();
                getRoombySchedule();
            }
            //loadData();
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
            unlockControls();
            //txtCode.Text = ClassRoomBLL.getInstance().getNextCode().ToString();
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
            Response.Redirect("../AcademicGroups/gBuilding.aspx");
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {

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

        public void getRoombySchedule()
        {
            List<Entities.ClassRoom> listClassRoom = new List<Entities.ClassRoom>();
            listClassRoom = BLL.ClassRoomBLL.getInstance().getAllActive();
            ListItem oItemS = new ListItem("---- Seleccione ----", "0");
            cboClassroom.Items.Add(oItemS);
            foreach (Entities.ClassRoom olistClassRoom in listClassRoom)
            {
                ListItem oItem = new ListItem(olistClassRoom.num_room + " (" + olistClassRoom.oLocation.oHeadquarters.description + " " + olistClassRoom.oLocation.building + " " + olistClassRoom.oLocation.module + ") ", olistClassRoom.code.ToString());
                cboClassroom.Items.Add(oItem);
            }
        }

        protected void cboProgramValue()
        {
            if (oUser.oProgram.code != 1)
            {
                cboprogram.SelectedValue = oUser.oProgram.code.ToString();
            }
        }
        protected void unlockControls()
        {
            clearControls();
            txtCode.Enabled = true;
            txtDescription.Enabled = true;
            txtcodeAlphaNumeric.Enabled = true;
            cboClassroom.Enabled = true;
            if (oUser.oProgram.code == 1)
            {
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
        }

        protected void blockControls()
        {
            txtCode.Enabled = false;
            cboprogram.Enabled = false;
            txtDescription.Enabled = false;
            txtcodeAlphaNumeric.Enabled = false;
            cboClassroom.Enabled = false;
            cboState.Enabled = false;
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
            cboState.SelectedValue = "1";
            lblMessage.Text = "";
            cboClassroom.SelectedValue = "0";
            lblMessagecodeAlphaNumeric.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorcodeAlphaNumeric", "$('#ContentPlaceHolder1_txtcodeAlphaNumeric').removeClass('has-error');", true);
            lblMessageDescription.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorDescription", "$('#ContentPlaceHolder1_txtDescription').removeClass('has-error');", true);
            lblMesageClassroom.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "removeHasErrorClassroom", "$('#ContentPlaceHolder1_cboClassroom').removeClass('has-error');", true);
            
        }
    }
}