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
    public class CourseBLL
    {

        private static CourseBLL instance = null;

        public static CourseBLL getInstance()
        {
            if (instance == null)
            {
                instance = new CourseBLL();
            }
            return instance;
        }

        /**
         * Method to check if the course exists in the database
        */
        public Boolean exists(Int32 pCode)
        {
            Boolean exists;
            String oSql = "SP_EXISTCOURSE";
            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", pCode);
                oCommand.Parameters[0].Direction = ParameterDirection.Input;
                Int32 toExist = Convert.ToInt32(DAO.getInstance().executeQueryScalar(oCommand));
                if (toExist == 0)
                {
                    exists = false;
                }
                else
                {
                    exists = true;
                }
                return exists;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        } //End exists 

        /**
         * Method to get the next code
        */
        public Int32 getNextCode()
        {
            String next;
            String oSql = "SP_GETNEXTCODECOURSE";
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
                return Convert.ToInt32(next);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        } //End getNextCode()

        /**
         * Method to insert a new period type in the database
        */
        public Int32 insert(Course oCourse)
        {
            String oSql = "SP_INSERTCOURSE";
            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", oCourse.id);
                oCommand.Parameters[0].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@DESCRIPTION", oCourse.description);
                oCommand.Parameters[1].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@STATE", oCourse.state);
                oCommand.Parameters[2].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@PROGRAM_ID", oCourse.oProgram.code);
                oCommand.Parameters[3].Direction = ParameterDirection.Input;
                return DAO.getInstance().executeSQL(oCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        } //End insert()

        /**
         * Method to update the data of the period type in the database
        */
        public Int32 modify(Course oSubject)
        {
            String oSql = "SP_MODIFYCOURSE";
            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", oSubject.id);
                oCommand.Parameters[0].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@DESCRIPTION", oSubject.description);
                oCommand.Parameters[1].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@STATE", oSubject.state);
                oCommand.Parameters[2].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@PROGRAM_ID", oSubject.oProgram.code);
                oCommand.Parameters[3].Direction = ParameterDirection.Input;
                return DAO.getInstance().executeSQL(oCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        } //End modify()

        /**
         * Method to deletethe period type in the database
        */
        public Int32 delete(Int32 pCode)
        {
            String oSql = "SP_DELETECOURSE";
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

        /**
         * Method to get all perid type
        */
        public List<Course> getAll()
        {
            String sql = "SP_GETALLCOURSE";
            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                List<Course> listSubject = new List<Course>();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    Course oSubject = new Course();
                    Program oProgram = new Program(); 
                    oSubject.id = Convert.ToInt32(oDataRow[0].ToString());
                    oSubject.description = oDataRow[1].ToString();
                    oSubject.state = Convert.ToInt16(oDataRow[2].ToString());
                    oProgram.code = Convert.ToInt32(oDataRow[3].ToString());
                    oSubject.oProgram = oProgram;
                    listSubject.Add(oSubject);
                }
                return listSubject;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        } //End getAll()

        public List<Course> getAllActived()
        {
            String sql = "SP_GETALLACTIVECOURSE";
            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                List<Course> listSubject = new List<Course>();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    Course oSubject = new Course();
                    Program oProgram = new Program();
                    oSubject.id = Convert.ToInt32(oDataRow[0].ToString());
                    oSubject.description = oDataRow[1].ToString();
                    oSubject.state = Convert.ToInt16(oDataRow[2].ToString());
                    oProgram.code = Convert.ToInt32(oDataRow[3].ToString());
                    oSubject.oProgram = oProgram;
                    listSubject.Add(oSubject);
                }
                return listSubject;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        } //End getAll()

        //This method is called at offer academic to fill uo the cmbCourse by program
        public List<Course> getAllActivedProgram(Int32 program_id)
        {
            String sql = "SP_GETALLACTIVECOURSEPROGRAM";

            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@PROGRAM_ID", program_id);
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                List<Course> listSubject = new List<Course>();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    Course oSubject = new Course();
                    Program oProgram = new Program();
                    oSubject.id = Convert.ToInt32(oDataRow[0].ToString());
                    oSubject.description = oDataRow[1].ToString();
                    oSubject.state = Convert.ToInt16(oDataRow[2].ToString());
                    oProgram.code = Convert.ToInt32(oDataRow[3].ToString());
                    oSubject.oProgram = oProgram;
                    listSubject.Add(oSubject);
                }
                return listSubject;
            }
            catch (Exception)
            {
                throw;
            }
            finally { }
        }


        /**
         * Method to get a period type by user id
        */
        public Course getCourse(Int32 pCode)
        {
            String sql = "SP_GETCOURSE";
            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@CODE", pCode);
                oCommand.Parameters[0].Direction = ParameterDirection.Input;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                Course oSubject = new Course();
                Program oProgram = new Program();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    oSubject.id = Convert.ToInt32(oDataRow[0].ToString());
                    oSubject.description = oDataRow[1].ToString();
                    oSubject.state = Convert.ToInt16(oDataRow[2].ToString());
                    oProgram.code = Convert.ToInt32(oDataRow[3].ToString());
                    oSubject.oProgram = oProgram;
                }
                return oSubject;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        } //End getCourse()


    }
}