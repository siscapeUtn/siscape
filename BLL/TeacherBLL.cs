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
    public class TeacherBLL
    {

        private static TeacherBLL instace = null;

        public static TeacherBLL getInstance()
        {
            if (instace == null)
            {
                instace = new TeacherBLL();
            }
            return instace;
        }

        //
        public Boolean exists(Int32 pCode)
        {
            Boolean exists;
            String oSql = "SP_EXISTTEACHER";
            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", pCode);
                oCommand.Parameters[0].Direction = ParameterDirection.Input;
                Int32 recordExists = Convert.ToInt32(DAO.getInstance().executeQueryScalar(oCommand));
                if (recordExists == 0)
                {
                    exists = false;
                }
                else
                {
                    exists = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
            return exists;
        }//  exists End

        public Int32 getNextCode()
        {

            String next;
            String oSql = "SP_GETNEXTCODETEACHER";

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
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
            return Convert.ToInt32(next);
        } // getNextCode End

        //Insert
        public Int32 insert(Teacher oTeacher)
        {

            String oSql = "SP_INSERTTEACHER";
            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@TEACHER_ID", oTeacher.code);
                oCommand.Parameters[0].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@ID", oTeacher.id);
                oCommand.Parameters[1].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@NAME", oTeacher.name);
                oCommand.Parameters[2].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@SURNAME", oTeacher.lastName);
                oCommand.Parameters[3].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@PHONE", oTeacher.homePhone);
                oCommand.Parameters[4].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@CELPHONE", oTeacher.cellPhone);
                oCommand.Parameters[5].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@MAIL", oTeacher.email);
                oCommand.Parameters[6].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@IDPOSITION", oTeacher.Position.code);
                oCommand.Parameters[7].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@STATE", oTeacher.state);
                oCommand.Parameters[8].Direction = ParameterDirection.Input;

                return DAO.getInstance().executeSQL(oCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        } //Insert end

        //Modify
        public Int32 modify(Teacher oTeacher)
        {

            String oSql = "SP_MODIFYTEACHER";

            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@TEACHER_ID", oTeacher.code);
                oCommand.Parameters[0].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@ID", oTeacher.id);
                oCommand.Parameters[1].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@NAME", oTeacher.name);
                oCommand.Parameters[2].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@SURNAME", oTeacher.lastName);
                oCommand.Parameters[3].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@PHONE", oTeacher.homePhone);
                oCommand.Parameters[4].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@CELPHONE", oTeacher.cellPhone);
                oCommand.Parameters[5].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@MAIL", oTeacher.email);
                oCommand.Parameters[6].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@IDPOSITION", oTeacher.Position.code);
                oCommand.Parameters[7].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@STATE", oTeacher.state);
                oCommand.Parameters[8].Direction = ParameterDirection.Input;
                return DAO.getInstance().executeSQL(oCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }//end Modify

        //delete
        public Int32 delete(Int32 pCode)
        {

            String oSql = "SP_DELETETEACHER";

            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@TEACHER_ID", pCode);
                oCommand.Parameters[0].Direction = ParameterDirection.Input;
                return DAO.getInstance().executeSQL(oCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }//delete end

        //get all the teachers
        public List<Teacher> getAll()
        {
            String sql = "SP_GETALLTEACHER";

            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
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
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }//end getAll

        //obtiene Teacher activos
        public List<Teacher> getAllActive()
        {
            String sql = "SP_GETALLTEACHERACTIVE";

            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
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
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }//getActive

        //get one especific teacher
        public Teacher getTeacher(Int32 pCode)
        {
            String sql = "SP_GETTEACHER";

            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", pCode);
                oCommand.Parameters[0].Direction = ParameterDirection.Input;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                Teacher oTeacher = new Teacher();
                InternalDesignation oInternalDesignation = new InternalDesignation(); 
                
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
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
                }
                return oTeacher;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }


    }
}