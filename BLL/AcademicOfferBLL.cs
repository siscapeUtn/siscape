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
    public class AcademicOfferBLL
    {

        private static AcademicOfferBLL instace = null;

        public static AcademicOfferBLL getInstance()
        {
            if (instace == null)
            {
                instace = new AcademicOfferBLL();
            }
            return instace;
        }

        public Int32 getNextCode()
        {
            String next;
            String oSql = "SP_GETNEXTCODEACADEMIC_OFFER";
            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                next = DAO.getInstance().executeQueryScalar(oCommand);
                if (next.Equals(""))
                {
                    next = "1";
                }
                else
                {
                    next = Convert.ToString(Convert.ToInt32(next) + 1);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally { }
            return Convert.ToInt32(next);
        } // getNextCode End

        public Boolean exists(Int32 pCode)
        {
            Boolean existe;
            String oSql = "SP_EXISTACADEMIC_OFFER";
            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", pCode);
                Int32 existencia = Convert.ToInt32(DAO.getInstance().executeQueryScalar(oCommand));
                if (existencia == 0)
                {
                    existe = false;
                }
                else
                {
                    existe = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally { }
            return existe;
        }// end get next code

        //insert the academic offer

        public Int32 insert(Entities.AcademicOffer oAcademicOffer)
        {
            String oSql = "SP_INSERTACADEMIC_OFFER";
            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", oAcademicOffer.code);
                oCommand.Parameters.AddWithValue("@TEACHER_ID", oAcademicOffer.oteacher.code);
                oCommand.Parameters.AddWithValue("@PERIOD_ID", oAcademicOffer.oPeriod.code);
                oCommand.Parameters.AddWithValue("@PROGRAM_ID", oAcademicOffer.oProgram.code);
                oCommand.Parameters.AddWithValue("@COURSE_ID", oAcademicOffer.oCourse.id);
                oCommand.Parameters.AddWithValue("@PRICE", oAcademicOffer.price);
                oCommand.Parameters.AddWithValue("@CLASSROOM_ID", oAcademicOffer.oClassRoom.code);
                oCommand.Parameters.AddWithValue("@SCHEDULE_ID", oAcademicOffer.oSchedule.code);
                oCommand.Parameters.AddWithValue("@HOURS", oAcademicOffer.hours);

                return DAO.getInstance().executeSQL(oCommand);
            }
            catch (Exception)
            {
                throw;
            }
            finally { }
        }

        public void modify(Entities.AcademicOffer oAcademicOffer)
        {
            throw new NotImplementedException();
        }

        /**
       * Method to deletethe Academic offer in the database
      */
        public Int32 delete(Int32 pCode)
        {
            String oSql = "SP_DELETEAcademicOffer";
            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", pCode);
                oCommand.Parameters[0].Direction = ParameterDirection.Input;
                return DAO.getInstance().executeSQL(oCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        } //End delete()


        //to select all academics offer 
        public List<Entities.AcademicOffer> getGridView()
        {
            String sql = "SP_SELECTALLGRIDVIEW";

            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
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

        //to select all academics offer by program
        public List<Entities.AcademicOffer> getGridViewProgram(int code)
        {
            String sql = "SP_SELECTPROGRAMGRIDVIEW";

            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", code);
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

    }
}