using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BLL
{
    public class ReportBLL
    {

        private static ReportBLL instace = null;

        public static ReportBLL getInstance()
        {
            if (instace == null)
            {
                instace = new ReportBLL();
            }
            return instace;
        }

        public List<AcademicOffer> reportTeacher(Int32 pPeriod_id)
        {
            String sql = "SP_REPORT_TEACHER";
            SqlCommand oCommand = new SqlCommand(sql);
            oCommand.Parameters.AddWithValue("@PERIOD_ID", pPeriod_id);
            oCommand.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                List<AcademicOffer> listAcademicOffer = new List<AcademicOffer>();

                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    AcademicOffer oAcademicOffer = new AcademicOffer();
                    Teacher oTeacher = new Teacher();
                    Course oCourse = new Course();
                    Schedule oSchedule = new Schedule();

                    oTeacher.name = oDataRow[0].ToString();
                    oTeacher.lastName = oDataRow[1].ToString();
                    oCourse.description = oDataRow[2].ToString();
                    oSchedule.name = oDataRow[3].ToString();
                    oSchedule.startTime = DateTime.Parse(oDataRow[4].ToString());
                    oSchedule.endTime = DateTime.Parse(oDataRow[5].ToString());
                    oAcademicOffer.hours = Convert.ToInt32(oDataRow[6].ToString());
                    oTeacher.state = Convert.ToInt16(oDataRow[7].ToString());
                    oAcademicOffer.oteacher = oTeacher;
                    oAcademicOffer.oCourse = oCourse;
                    oAcademicOffer.oSchedule = oSchedule;
                    listAcademicOffer.Add(oAcademicOffer);
                }

                return listAcademicOffer;
            }
            catch (Exception)
            {
                throw;
            }
            finally { }
        }


        public List<ClassRoom> reportClassRoom(Int32 pPeriod_id) //Listo
        {
            String sql = "SP_REPORT_CLASSROOM";

            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.Parameters.AddWithValue("@PERIOD_ID", pPeriod_id);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                List<ClassRoom> listClassRoom = new List<ClassRoom>();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    ClassRoom oClassRoom = new ClassRoom();
                    Program oProgram = new Program();
                    Location oLocation = new Location();
                    Headquarters oHeadquarters = new Headquarters();
                    ClassRoomsType oClassRoomsType = new ClassRoomsType();
                    oClassRoom.code = Convert.ToInt32(oDataRow[0].ToString());
                    oClassRoom.num_room = oDataRow[1].ToString();
                    oClassRoom.size = Convert.ToInt32(oDataRow[2].ToString());
                    oProgram.code = Convert.ToInt32(oDataRow[3].ToString());
                    oProgram.name = oDataRow[4].ToString();
                    oClassRoomsType.code = Convert.ToInt32(oDataRow[5].ToString());
                    oClassRoomsType.description = oDataRow[6].ToString();
                    oLocation.code = Convert.ToInt16(oDataRow[7].ToString());
                    oHeadquarters.code = Convert.ToInt16(oDataRow[8].ToString());
                    oHeadquarters.description = oDataRow[9].ToString();
                    oLocation.building = oDataRow[10].ToString();
                    oLocation.module = oDataRow[11].ToString();
                    oLocation.oHeadquarters = oHeadquarters;
                    oClassRoom.state = Convert.ToInt16(oDataRow[12].ToString());
                    oClassRoom.oProgram = oProgram;
                    oClassRoom.oClassRoomsType = oClassRoomsType;
                    oClassRoom.oLocation = oLocation;
                    listClassRoom.Add(oClassRoom);
                }
                return listClassRoom;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }

        public List<Entities.AcademicOffer> reportOffer(Int32 pPeriod_id)
        {
            String sql = "SP_REPORT_ACADEMIC_OFFER";

            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.Parameters.AddWithValue("@PERIOD_ID", pPeriod_id);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                List<AcademicOffer> listAcademicOffer = new List<AcademicOffer>();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    AcademicOffer oAcademic = new AcademicOffer();
                    Course oCourse = new Course();
                    Teacher oTeacher = new Teacher();
                    Schedule oSchedule = new Schedule();
                    ClassRoom oClassRoom = new ClassRoom();

                    oAcademic.code = Convert.ToInt32(oDataRow[0].ToString());
                    oCourse.description = oDataRow[1].ToString();
                    oTeacher.name = oDataRow[2].ToString();
                    oTeacher.lastName = oDataRow[3].ToString();
                    oSchedule.name = oDataRow[4].ToString();
                    oSchedule.typeSchedule = oDataRow[5].ToString();
                    oSchedule.startTime = DateTime.Parse(oDataRow[9].ToString());
                    oSchedule.endTime = DateTime.Parse(oDataRow[10].ToString());
                    oClassRoom.num_room = oDataRow[6].ToString();
                    oAcademic.price = Convert.ToDecimal(oDataRow[7].ToString());
                    oAcademic.oCourse = oCourse;
                    oAcademic.oteacher = oTeacher;
                    oAcademic.oSchedule = oSchedule;
                    oAcademic.oClassRoom = oClassRoom;
                    listAcademicOffer.Add(oAcademic);
                }
                return listAcademicOffer;
            }
            catch (Exception)
            {
                throw;
            }
            finally { }
        }

        public List<Entities.WaitingList> reportWaitingList(Int32 pPeriod_id, Int32 pIsContact)
        {
            String sql = "SP_REPORT_WAITING_LIST";

            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.Parameters.AddWithValue("@PERIOD_ID", pPeriod_id);
                oCommand.Parameters.AddWithValue("@IS_CONTACT", pIsContact);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                List<WaitingList> listWaitingList = new List<WaitingList>();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    WaitingList oWaitingList = new WaitingList();

                    oWaitingList.name = oDataRow[0].ToString();
                    oWaitingList.lastName = oDataRow[1].ToString();
                    oWaitingList.homePhone = oDataRow[2].ToString();
                    oWaitingList.cellPhone = oDataRow[3].ToString();
                    oWaitingList.email = oDataRow[4].ToString();
                    oWaitingList.course_name = oDataRow[5].ToString();
                    oWaitingList.day = oDataRow[6].ToString();
                    listWaitingList.Add(oWaitingList);
                }
                return listWaitingList;
            }
            catch (Exception)
            {
                throw;
            }
            finally { }
        }

    }
}