using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DAL;
using Entities;

namespace BLL
{
    public class ProgramBLL
    {
        private static ProgramBLL instace = null;

        public static ProgramBLL getInstance()
        {
            if (instace == null)
            {
                instace = new ProgramBLL();
            }
            return instace;
        }//End getInstance()

        public Int32 getNextCode()
        {
            String next; //To contain the next code
            String oSql = "SP_GETNEXTCODEPROGRAM"; //Stored procedure name
            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                next = DAO.getInstance().executeQueryScalar(oCommand);//To execute query
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
        } // getNextCode End

        //Method to check if the program exists in the database
        public Boolean exists(Int32 pCode)
        {
            Boolean exist;
            String oSql = "SP_EXISTPROGRAM";//Stored procedure name
            try
            {

                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", pCode);
                Int32 toExist = Convert.ToInt32(DAO.getInstance().executeQueryScalar(oCommand)); //To execute query
                if (toExist == 0)
                {
                    exist = false;
                }
                else
                {
                    exist = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
            return exist;
        }//End exists

        //Method to insert a new program in the database
        public Int32 insert(Program pProgram)
        {
            String oSql = "SP_INSERTPROGRAM"; //Stored procedure name
            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", pProgram.code);
                oCommand.Parameters[0].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@NAME", pProgram.name);
                oCommand.Parameters[1].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@STATE", pProgram.state);
                oCommand.Parameters[2].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@UNIT", pProgram.unit);
                oCommand.Parameters[3].Direction = ParameterDirection.Input;
                return DAO.getInstance().executeSQL(oCommand); //To execute insert
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }//End insert()

        //Method to update a program in the database
        public Int32 modify(Program pProgram)
        {
            String oSql = "SP_MODIFYPROGRAM";//Stored procedure name
            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", pProgram.code);
                oCommand.Parameters.AddWithValue("@NAME", pProgram.name);
                oCommand.Parameters.AddWithValue("@STATE", pProgram.state);
                oCommand.Parameters.AddWithValue("@UNIT", pProgram.unit);
                return DAO.getInstance().executeSQL(oCommand);////To execute update
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }//End modify()

        //Method to delete a program in the database
        public Int32 delete(Int32 pCode)
        {
            String oSql = "SP_DELETEPROGRAM";//Stored procedure name
            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", pCode);
                return DAO.getInstance().executeSQL(oCommand);////To execute update
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }//End delete()

        //Method to get all programs
        public List<Program> getAll()
        {
            String sql = "SP_GETALLPROGRAM";//Stored procedure name
            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);//To execute query
                List<Program> listProgram = new List<Program>();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    Program oProgram = new Program();
                    oProgram.code = Convert.ToInt32(oDataRow[0].ToString());
                    oProgram.name = oDataRow[1].ToString();
                    oProgram.state = Convert.ToInt16(oDataRow[2].ToString());
                    oProgram.unit = Convert.ToInt64(oDataRow[3].ToString());
                    listProgram.Add(oProgram);
                }
                return listProgram;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }//End getAll()

        //Method to get a program by id
        public Program getProgram(Int32 pCode)
        {
            String sql = "SP_GETPROGRAM";//Stored procedure name
            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ID", pCode);
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);//To execute query
                Program oProgram = new Program();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    oProgram.code = Convert.ToInt32(oDataRow[0].ToString());
                    oProgram.name = oDataRow[1].ToString();
                    oProgram.state = Convert.ToInt16(oDataRow[2].ToString());
                    oProgram.unit = Convert.ToInt64(oDataRow[3].ToString());
                }
                return oProgram;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }//End getProgram()

        //Method to get all actived programs
        public List<Program> getAllActived()
        {
            String sql = "SP_GETALLPROGRAMACTIVED";//Stored procedure name
            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);//To execute query
                List<Program> listProgram = new List<Program>();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    Program oProgram = new Program();
                    oProgram.code = Convert.ToInt32(oDataRow[0].ToString());
                    oProgram.name = oDataRow[1].ToString();
                    oProgram.state = Convert.ToInt16(oDataRow[2].ToString());
                    oProgram.unit = Convert.ToInt64(oDataRow[3].ToString());
                    listProgram.Add(oProgram);
                }
                return listProgram;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }//End getAllActived()

        //Method to get all disactived programs
        public List<Program> getAllDesactived()
        {
            String sql = "SP_GETALLPROGRAMDESACTIVE";//Stored procedure name
            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);//To execute query
                List<Program> listProgram = new List<Program>();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    Program oProgram = new Program();
                    oProgram.code = Convert.ToInt32(oDataRow[0].ToString());
                    oProgram.name = oDataRow[1].ToString();
                    oProgram.state = Convert.ToInt16(oDataRow[2].ToString());
                    oProgram.unit = Convert.ToInt64(oDataRow[3].ToString());
                    listProgram.Add(oProgram);
                }
                return listProgram;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }//End getAllDesactived()

    }//End ProgramBLL
}