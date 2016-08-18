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

        public List<Teacher> reportTeacher()
        {
            String sql = "SP_REPORT_TEACHER";
            SqlCommand oCommand = new SqlCommand(sql);
            oCommand.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                List<Teacher> listTeacher = new List<Teacher>();

                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    Teacher oTeacher = new Teacher();
                    InternalDesignation oInternalDesignation = new InternalDesignation();
                    oTeacher.code = Convert.ToInt32(oDataRow[0].ToString());
                    oTeacher.id = oDataRow[1].ToString();
                    oTeacher.name = oDataRow[2].ToString();
                    oTeacher.lastName = oDataRow[3].ToString();
                    oTeacher.homePhone = oDataRow[4].ToString();
                    oTeacher.cellPhone = oDataRow[5].ToString();
                    oTeacher.email = oDataRow[6].ToString();
                    oInternalDesignation.code = Convert.ToInt32(oDataRow[7].ToString());
                    oInternalDesignation.description = oDataRow[8].ToString();
                    oTeacher.state = Convert.ToInt16(oDataRow[9].ToString());
                    oTeacher.Position = oInternalDesignation;
                    listTeacher.Add(oTeacher);
                }

                return listTeacher;
            }
            catch (Exception)
            {
                throw;
            }
            finally { }
        }


        public List<ClassRoom> reportClassRoom(Int32 pPeriod_id)
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

        public List<ExternalDesignation> reportExternalDesignation(Int32 pPeriod_id)
        {
            String sql = "SP_REPORT_WAITING_LIST";
            SqlCommand oCommand = new SqlCommand(sql);
            oCommand.Parameters.AddWithValue("@PERIOD_ID", pPeriod_id);
            oCommand.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                List<ExternalDesignation> listInternalDesignation = new List<ExternalDesignation>();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    ExternalDesignation oExternalDesignation = new ExternalDesignation();
                    oExternalDesignation.code = Convert.ToInt32(oDataRow[0].ToString());
                    oExternalDesignation.oTeacher = new Teacher();
                    oExternalDesignation.oTeacher.name = oDataRow[1].ToString();
                    oExternalDesignation.oTeacher.id = oDataRow[2].ToString();
                    oExternalDesignation.position = oDataRow[3].ToString();
                    oExternalDesignation.location = oDataRow[4].ToString();
                    oExternalDesignation.hours = Convert.ToInt32(oDataRow[5].ToString());
                    oExternalDesignation.initial_day = Convert.ToDateTime(oDataRow[6].ToString());
                    oExternalDesignation.final_day = Convert.ToDateTime(oDataRow[7].ToString());

                    listInternalDesignation.Add(oExternalDesignation);
                }
                return listInternalDesignation;
            }
            catch (Exception)
            {
                throw;
            }
            finally { }
        }

        public List<ExternalDesignation> reportExternalDesignation(Int32 pPeriod_id, Int32 contact)
        {
            String sql = "SP_REPORT_WAITING_LISTBYCONTACT";
            SqlCommand oCommand = new SqlCommand(sql);
            oCommand.Parameters.AddWithValue("@PERIOD_ID", pPeriod_id);
            oCommand.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                List<ExternalDesignation> listInternalDesignation = new List<ExternalDesignation>();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    ExternalDesignation oExternalDesignation = new ExternalDesignation();
                    oExternalDesignation.code = Convert.ToInt32(oDataRow[0].ToString());
                    oExternalDesignation.oTeacher = new Teacher();
                    oExternalDesignation.oTeacher.name = oDataRow[1].ToString();
                    oExternalDesignation.oTeacher.id = oDataRow[2].ToString();
                    oExternalDesignation.position = oDataRow[3].ToString();
                    oExternalDesignation.location = oDataRow[4].ToString();
                    oExternalDesignation.hours = Convert.ToInt32(oDataRow[5].ToString());
                    oExternalDesignation.initial_day = Convert.ToDateTime(oDataRow[6].ToString());
                    oExternalDesignation.final_day = Convert.ToDateTime(oDataRow[7].ToString());

                    listInternalDesignation.Add(oExternalDesignation);
                }
                return listInternalDesignation;
            }
            catch (Exception)
            {
                throw;
            }
            finally { }
        }
    }
}