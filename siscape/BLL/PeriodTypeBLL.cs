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
    public class PeriodTypeBLL
    {
        private static PeriodTypeBLL instance = null;

        public static PeriodTypeBLL getInstance()
        {
            if (instance == null)
            {
                instance = new PeriodTypeBLL();
            }
            return instance;
        }

        /**
         * Method to check if the periodType exists in the database
        */
        public Boolean exists(Int32 pCode)
        {
            Boolean exists;
            String oSql = "SP_EXISTSPERIODTYPE";
            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@CODE", pCode);
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
            String oSql = "SP_GETNEXTCODEPERIODTYPE";
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
        public Int32 insert(PeriodType oPeriodType)
        {
            String oSql = "SP_INSERTPERIODTYPE";
            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@CODE", oPeriodType.code);
                oCommand.Parameters[0].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@DESCRIPTION", oPeriodType.description);
                oCommand.Parameters[1].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@STATE", oPeriodType.state);
                oCommand.Parameters[2].Direction = ParameterDirection.Input;
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
        public Int32 modify(PeriodType oPeriodType)
        {
            String oSql = "SP_MODIFYPERIODTYPE";
            try
            {
                SqlCommand oCommand = new SqlCommand(oSql);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@CODE", oPeriodType.code);
                oCommand.Parameters[0].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@DESCRIPTION", oPeriodType.description);
                oCommand.Parameters[1].Direction = ParameterDirection.Input;
                oCommand.Parameters.AddWithValue("@STATE", oPeriodType.state);
                oCommand.Parameters[2].Direction = ParameterDirection.Input;
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
            String oSql = "SP_DELETEPERIODTYPE";
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
        public List<PeriodType> getAll()
        {
            String sql = "SP_GETALLPERIODTYPE";
            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                List<PeriodType> listPeriodType = new List<PeriodType>();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    PeriodType oPeriodType = new PeriodType();
                    oPeriodType.code = Convert.ToInt32(oDataRow[0].ToString());
                    oPeriodType.description = oDataRow[1].ToString();
                    oPeriodType.state = Convert.ToInt16(oDataRow[2].ToString());
                    listPeriodType.Add(oPeriodType);
                }
                return listPeriodType;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        } //End getAll()

        public List<PeriodType> getAllActived()
        {
            String sql = "SP_GETALLACTIVEPERIODTYPE";
            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                List<PeriodType> listPeriodType = new List<PeriodType>();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    PeriodType oPeriodType = new PeriodType();
                    oPeriodType.code = Convert.ToInt32(oDataRow[0].ToString());
                    oPeriodType.description = oDataRow[1].ToString();
                    oPeriodType.state = Convert.ToInt16(oDataRow[2].ToString());
                    listPeriodType.Add(oPeriodType);
                }
                return listPeriodType;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        } //End getAll()

        /**
         * Method to get a period type by user id
        */
        public PeriodType getPeriod(Int32 pCode)
        {
            String sql = "SP_GETPERIODTYPE";
            try
            {
                SqlCommand oCommand = new SqlCommand(sql);
                oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@CODE", pCode);
                oCommand.Parameters[0].Direction = ParameterDirection.Input;
                DataTable oDataTable = DAO.getInstance().executeQuery(oCommand);
                PeriodType oPeriodType = new PeriodType();
                foreach (DataRow oDataRow in oDataTable.Rows)
                {
                    oPeriodType.code = Convert.ToInt32(oDataRow[0].ToString());
                    oPeriodType.description = oDataRow[1].ToString();
                    oPeriodType.state = Convert.ToInt16(oDataRow[2].ToString());
                }
                return oPeriodType;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        } //End getPeriod()
    }
}